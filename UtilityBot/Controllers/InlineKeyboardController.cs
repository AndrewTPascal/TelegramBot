using Telegram.Bot;
using Telegram.Bot.Types;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    /// <summary>
    /// Контроллер нажатий на кнопки управления
    /// </summary>
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).SelectOption = callbackQuery.Data;

            // Генерим информационное сообщение
            switch (callbackQuery.Data)
            {
                case "MessageLength":
                    await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                "Введите текст", cancellationToken: ct);
                    break;

                case "Sum":
                    await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                "Введите числа через пробел", cancellationToken: ct);
                    break;
            }

        }


    }
}
