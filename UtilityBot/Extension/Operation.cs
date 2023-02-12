
namespace UtilityBot.Extension
{
    public static class Operation
    {
        /// <summary>
        /// Перебор строки, подсчет суммы числел в строке, при ошибке возвратим null
        /// </summary>
        public static int? Sum(string text) 
        {
            string[] number = text.Split(' '); // Разделение на коллекцию 
            int total = 0;

            foreach (string i in number)
            {
                int value;
                bool iscorrect = int.TryParse(i, out value);
                if (!iscorrect) return null;
                else total += value;
            }

            return total;
        }
    }
}
