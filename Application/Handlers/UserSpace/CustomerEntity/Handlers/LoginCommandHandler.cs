using Application.Handlers.UserSpace.CustomerEntity.Commands;
using Application.Handlers.UserSpace.CustomerEntity.Responses;
using Domain;
using Domain.Entities.UserSpace;
using Domain.Interfaces.Repositories.UserSpace;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Handlers.UserSpace.CustomerEntity.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private TimeSpan _secondsLimit = TimeSpan.FromSeconds(30);  //Сек.
        private TimeSpan _codeLifetime = TimeSpan.FromMinutes(5);   //Мин.
        private const int _countSendLimit = 5; //Ко-во доступных попыток отправки, перед блокировкой на _unblockingTime мин.
        private TimeSpan _unblockingTime = TimeSpan.FromMinutes(3); //Кол-во мин. на которое блокируется пользователь, если сделал _countSendLimit попыток

        private readonly IPhoneVerificationCodeRepository _codeRepository;

        private readonly ILogger<LoginCommandHandler> _logger;

        public LoginCommandHandler(IPhoneVerificationCodeRepository codeRepository, ILogger<LoginCommandHandler> logger)
        {
            _codeRepository = codeRepository;
            _logger = logger;
        }

        

        public async Task<LoginResponse> SendSMSCode(PhoneVerificationCode phoneVerificationCode, CancellationToken cancellationToken, bool isNewEntity = false)
        {
            phoneVerificationCode.AllCountSendMessage += 1;
            phoneVerificationCode.NumberOfMessagesSentBeforeLoggingIn += 1;
            phoneVerificationCode.CountSendMessage += 1; //Кол-во отправленных сообщений на номер одного и того же кода

            phoneVerificationCode.UnblockingTimeSeconds = TimeHelper.GetSecondsSince1980() + (long)_secondsLimit.TotalSeconds; //Ставим минимальную блокировку на частоту отправки

            //Здесь вышло время жизни кода 
            if (phoneVerificationCode.CodeLifetimeSeconds <= TimeHelper.GetSecondsSince1980())
            {
                
                phoneVerificationCode.Code = GetRandomCode(); // Генерируем 6-значный код  
                phoneVerificationCode.CodeLifetimeSeconds = TimeHelper.GetSecondsSince1980() + (long)_codeLifetime.TotalSeconds; //Обновляем время жизни кода
                phoneVerificationCode.CountSendMessage = 1; //Сбрасываем попытки 
               
                _logger.LogWarning($"\n Вышло время жизни кода, генерируем новый 6-значный код {phoneVerificationCode.Code} \n");
                 
            }

            if (isNewEntity) {

                _logger.LogWarning($"Новая запись");
                await _codeRepository.AddAsync(phoneVerificationCode, cancellationToken);
            }
            else
            {

                _logger.LogWarning($"Обновление");

                await _codeRepository.UpdateAsync(phoneVerificationCode, cancellationToken);
            }
           

            try
            {    //SMS-сервис  
                _logger.LogWarning($"\n\n Отправка SMS на {phoneVerificationCode.PhoneNumber}: Ваш код подтверждения: {phoneVerificationCode.Code} \n\n",
                     $"{phoneVerificationCode.PhoneNumber}",
                    $"{phoneVerificationCode.CodeLifetimeSeconds}",
                    $"{phoneVerificationCode.CountSendMessage}",
                    $"{phoneVerificationCode.UnblockingTimeSeconds}",
                    $"{phoneVerificationCode.AllCountSendMessage}",
                    $"{phoneVerificationCode.NumberOfAttempts}"
                    );
                 
                 
                return new LoginResponse(phoneVerificationCode.UnblockingTimeSeconds, phoneVerificationCode.PhoneNumber) { Success = true, MessageInformation = "Код в SMS успешно отправлен на наш телефон."};

            }
            catch (Exception ex)
            {
                return new LoginResponse(phoneVerificationCode.UnblockingTimeSeconds, phoneVerificationCode.PhoneNumber) { Success = false, Error = ex.Message };
            }
      
        }

        private Random _random = new Random();

        private string GetRandomCode() => _random.Next(100000, 1000000).ToString();

       

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        { 

            PhoneVerificationCode? phoneVerificationCode = await _codeRepository.GetByPhoneNumberAsync(request.PhoneNumber);

            if (phoneVerificationCode == null) {

                _logger.LogWarning($"Запись не найдена");

                var newEntity = new PhoneVerificationCode
                {
                    Id = Guid.NewGuid(),
                    PhoneNumber = request.PhoneNumber,
                    Code = GetRandomCode(), // Генерируем 6-значный код
                    CodeLifetimeSeconds = TimeHelper.GetSecondsSince1980() + (long)_codeLifetime.TotalSeconds, //Время жизни этого кода, по истечению генерируем новый.  
                    NumberOfAttempts = 1, //Кол-во попыток отправить
                };


                return await SendSMSCode(newEntity, cancellationToken, true);
 
            }

            _logger.LogWarning($"Запись найдена");

            phoneVerificationCode.NumberOfAttempts += 1; //Увеличиваем на один, общее кол-во попыток отправить

            _logger.LogWarning($"TimeSpan {phoneVerificationCode.UnblockingTimeSeconds > TimeHelper.GetSecondsSince1980()}");

            //Если время UnblockingTimeSeconds больше текущего, значит действует блокировка.
            if (phoneVerificationCode.UnblockingTimeSeconds > TimeHelper.GetSecondsSince1980())
            { 

                await _codeRepository.UpdateAsync(phoneVerificationCode, cancellationToken); //Обновляем так как NumberOfAttempts изменился
                 

                if (phoneVerificationCode.CountSendMessage < _countSendLimit)
                {
                    var message = $"Следующая попытка будет доступна через: {TimeHelper.GetFormattedRemainingTime(phoneVerificationCode.UnblockingTimeSeconds)}";

                    _logger.LogWarning(message);

                    return new LoginResponse(phoneVerificationCode.UnblockingTimeSeconds, phoneVerificationCode.PhoneNumber) { Success = false, Error = message};
                }
                else
                {                
                    var message = $"Вы слишком много сделали попыток, попробуйте через: {TimeHelper.GetFormattedRemainingTime(phoneVerificationCode.UnblockingTimeSeconds)}";

                    _logger.LogWarning(message);

                    return new LoginResponse(phoneVerificationCode.UnblockingTimeSeconds, phoneVerificationCode.PhoneNumber) { Success = false, Error = message,  };
                }
            }

            //Если кол-во оправленных сообщений достигло лимита на отправку, мы ставим блокировку на не большое время
            if (phoneVerificationCode.CountSendMessage == _countSendLimit)
            {
                phoneVerificationCode.UnblockingTimeSeconds = TimeHelper.GetSecondsSince1980() + (long)_unblockingTime.TotalSeconds; //Ставим блокировку
                 
                phoneVerificationCode.CountSendMessage += 1; //Мы увеличиваем, что-бы пройти эту проверку

                await _codeRepository.UpdateAsync(phoneVerificationCode, cancellationToken);

                var message = $"Вы слишком много сделали попыток, попробуйте через: {TimeHelper.GetFormattedRemainingTime(phoneVerificationCode.UnblockingTimeSeconds)}";

                return new LoginResponse(phoneVerificationCode.UnblockingTimeSeconds, phoneVerificationCode.PhoneNumber) { Success = false, Error = message};

            }

            //Если кол-во отправленных больше чем, установлено лимитом, то значит было ограничение, но оно уже снято!
            if (phoneVerificationCode.CountSendMessage > _countSendLimit)
            {

                phoneVerificationCode.CodeLifetimeSeconds = long.MinValue; //Здесь мы точно говорим, что время жизни кода истекло, далее он будет сброшен
                return await SendSMSCode(phoneVerificationCode, cancellationToken);

            }

  
            return await SendSMSCode(phoneVerificationCode, cancellationToken);

        }
    }
}
