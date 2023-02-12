using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    /// <summary>
    /// Контроллер текстовых сообщений
    /// </summary>
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            // Старт бота, выводим кнопки 
            if (message.Text == "/start")
            {
                var buttons = new List<InlineKeyboardButton[]>();
                buttons.Add(new[]
                {
                        InlineKeyboardButton.WithCallbackData($"Длина сообщения" , $"MessageLength"),
                        InlineKeyboardButton.WithCallbackData($"Сумма чисел" , $"Sum"),
                    });
                // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                $"<b>  Наш бот может вычислить длину вашего сообщения или сумму целых чисел.</b> {Environment.NewLine}",
                    cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                return;
            }

            string selectOption = _memoryStorage.GetSession(message.Chat.Id).SelectOption;   // Получаем опцию из сессии пользователя
            
            // В зависимости от выбранной опции выводим длину сообщения либо сумму чисел
            switch (selectOption)
            {
                case "MessageLength":
                    await _telegramClient.SendTextMessageAsync
                (message.Chat.Id, $"Длина сообщения: {message.Text.Length} знаков", cancellationToken: ct);
                    break;

                case "Sum":
                    int? total = Extension.Operation.Sum(message.Text);

                    if (total == null) await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                "Введены некоректные данные!", cancellationToken: ct);
                    else
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                    $"Сумма чисел равна {total}", cancellationToken: ct);

                    break;
                default:
                    return;
            }

        }
    }
}
