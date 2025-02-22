using Application.Common.Interfaces;
using Application.Handlers.UserSpace.CustomerEntity.Commands;
using Application.Handlers.UserSpace.CustomerEntity.DTOs;
using Application.Handlers.UserSpace.CustomerEntity.Responses;
using Domain;
using Domain.Entities.ProductSpace;
using Domain.Entities.UserSpace;
using Domain.Entities.UserSpace.UserTypes;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.ProductSpace;
using Domain.Interfaces.Repositories.UserSpace;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserSpace.CustomerEntity.Handlers
{
    public class VerifyPhoneCodeCommandHandler : IRequestHandler<VerifyPhoneCodeCommand, VerifyPhoneCodeResponse>
    {
        private readonly IPhoneVerificationCodeRepository _codeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IWishListRepository _wishListRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VerifyPhoneCodeCommandHandler> _logger;

        

        public VerifyPhoneCodeCommandHandler(
            IPhoneVerificationCodeRepository codeRepository,
            IUserRepository userRepository,
            IWishListRepository wishListRepository,
            IUnitOfWork unitOfWork,
            ITokenService tokenService,
            ILogger<VerifyPhoneCodeCommandHandler> logger)
        {
            _codeRepository = codeRepository;
            _userRepository = userRepository;
            _wishListRepository = wishListRepository;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task<VerifyPhoneCodeResponse> Handle(VerifyPhoneCodeCommand request, CancellationToken cancellationToken)
        {
            PhoneVerificationCode? phoneVerificationCode = await _codeRepository.GetByPhoneNumberAsync(request.PhoneNumber);
            if (phoneVerificationCode == null)
            {
                _logger.LogWarning($"\n\n Код не найден. Запросите новый код. ({request.PhoneNumber}) \n\n");

                return new VerifyPhoneCodeResponse { Success = false, Error = "Код не найден. Запросите новый код." };
            }

            if (phoneVerificationCode.CodeLifetimeSeconds < TimeHelper.GetSecondsSince1980())
            {
                _logger.LogInformation($"\n\n Код истёк. Запросите новый код. ({request.PhoneNumber} | {phoneVerificationCode.Code}) \n\n");
                return new VerifyPhoneCodeResponse { Success = false, Error = "Код истёк. Запросите новый код." };
            }

            if (phoneVerificationCode.Code != request.Code)
            {
                _logger.LogInformation($"\n\n Неверный код. ({request.PhoneNumber} | {phoneVerificationCode.Code}) \n\n");

                return new VerifyPhoneCodeResponse { Success = false, Error = "Неверный код." };
            }
             

            //Так как мы прошли авторизацию, то нужно обновить счетчик. Этот счетчик контролирует возможные попытки работы скрипта, который не входит в систему, а только пытается получать коды в sms 
            //После вывода из системы и попыток войти, ему будет снова доступна возможность отправлять коды, но мы поставим защиту на кол-во входов-выходов и будем блокировать на больший срок.  
            phoneVerificationCode.NumberOfMessagesSentBeforeLoggingIn = 0;

            await _codeRepository.UpdateAsync(phoneVerificationCode);


            // Поиск пользователя по номеру телефона
            var user = await _userRepository.GetByPhoneAsync(request.PhoneNumber);

            if (user == null)
            {
                // Если пользователь не найден, выполняем авто-регистрацию
             
                using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
                try
                {
                    var wishList = new WishList
                    {
                        Id = Guid.NewGuid()
                    };

                    await _wishListRepository.AddAsync(wishList, cancellationToken); 

                    var userId = Guid.NewGuid();

                    user = new User
                    {
                        Id = userId,
                        UserType = UserType.Customer,
                        PhoneNumber = request.PhoneNumber,
                        CreatedByUserId = userId,
                        WishListId = wishList.Id
                    }; 

                    await _userRepository.AddAsync(user, cancellationToken); 
           
                    await transaction.CommitAsync(cancellationToken);

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }  
            }
             

            // Генерация JWT-токена
            var token = _tokenService.CreateToken(user);

            return new VerifyPhoneCodeResponse
            {
                Success = true,
                Token = token,
                MessageInformation = "Вы успешно выполнили вход.",
                CustomerDto = new CustomerDto
                {
                    Id = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Patronymic = user.Patronymic,
                    DateOfBirth = user.DateOfBirth,
                    Email = user.Email, 
                }
            };
        }
    }
}
