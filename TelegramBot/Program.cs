using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBot.Configuration;
using TelegramBot.Controllers;
using TelegramBot.Services;

namespace TelegramBot
{
    class Program
    {
        public static async Task Main()
        {
            // Выбор кодировки в консоли, юникод
            Console.OutputEncoding = Encoding.Unicode;
            

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");

        }

        /// <summary>
        /// Подключение контроллеров и конфигурации
        /// </summary>
        /// <param name="services"></param>
        static void ConfigureServices(IServiceCollection services)
        {
            //id = 6035435830:AAHK-m94k11kC3Vf9taH8heQkLi-ndQ0oQM

            services.AddSingleton<IFileHandler, AudioFileHandler>();

            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());

            services.AddSingleton<IStorage, MemoryStorage>();

            // Подключаем контроллеры сообщений и кнопок
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<VoiceMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(appSettings.BotToken));
            services.AddHostedService<Bot>();
        }

        /// <summary>
        /// Задаем основную конфигупрацию: каталог загрузки голосовых сообщений, токен, имя файла, 
        /// входной и выходной формат аудио, битрейт (бит/сек)
        /// </summary>
        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                DownloadsFolder = "D:\\Разработка\\skillfactory\\task_11\\TelegramBot\\Downloads",
                BotToken = "6035435830:AAHK-m94k11kC3Vf9taH8heQkLi-ndQ0oQM",
                AudioFileName = "audio",
                InputAudioFormat = "ogg",
                OutputAudioFormat = "wav",
                InputAudioBitrate = 50000,
            };
        }



    }
}
