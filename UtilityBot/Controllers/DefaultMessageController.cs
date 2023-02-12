using Telegram.Bot.Types;
using Telegram.Bot;

namespace UtilityBot.Controllers
{
    /// <summary>
    /// Контроллер сообщений другого формата
    /// </summary>
    public class DefaultMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public DefaultMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Данный тип сообщения не поддерживатеся. Пожалуйста, введите текст!",
                cancellationToken: ct);
        }
     }
}
