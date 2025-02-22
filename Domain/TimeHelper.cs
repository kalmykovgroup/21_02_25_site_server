using System.Globalization;

namespace Domain
{
    public class TimeHelper
    {
        private static readonly DateTime Epoch1980 = new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Возвращает строку с оставшимся временем в удобном формате (например, "1 час 30 минут" или "1 hour 30 minutes")
        /// </summary>
        public static string GetFormattedRemainingTime(long secondsSince1980)
        {
            long currentSeconds = GetSecondsSince1980();
            long remainingSeconds = secondsSince1980 - currentSeconds;

            if (remainingSeconds <= 0)
            {
                return GetLocalizedTimeString(0, 0, 0);
            }

            TimeSpan timeSpan = TimeSpan.FromSeconds(remainingSeconds);
            return GetLocalizedTimeString((int)timeSpan.Hours, (int)timeSpan.Minutes, (int)timeSpan.Seconds);
        }

        /// <summary>
        /// Получает текущие секунды с 1980 года
        /// </summary>
        public static long GetSecondsSince1980()
        {
            return (long)(DateTime.UtcNow - Epoch1980).TotalSeconds;
        }

        /// <summary>
        /// Форматирует время в зависимости от локали
        /// </summary>
        private static string GetLocalizedTimeString(int hours, int minutes, int seconds)
        {
            string culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            switch (culture)
            {
                case "ru":
                    return $"{(hours > 0 ? $"{hours} ч " : "")}{(minutes > 0 ? $"{minutes} мин " : "")}{seconds} сек".Trim();
                case "en":
                default:
                    return $"{(hours > 0 ? $"{hours} h " : "")}{(minutes > 0 ? $"{minutes} min " : "")}{seconds} sec".Trim();
            }
        }

    }
}
