using UtilityBot.Models;

namespace UtilityBot.Services
{

    /// <summary>
    /// Интерфейс для хранения пользовательских сессий
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Получение сессии пользователя по идентификатору
        /// </summary>
        Session GetSession(long chatId);
    }

}
