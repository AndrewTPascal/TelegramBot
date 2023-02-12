using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Models
{
    /// <summary>
    /// Хранение данных о сессии пользователя, содержит код выбранного языка
    /// </summary>
    public class Session
    {
        public string LanguageCode { get; set; }
    }
}
