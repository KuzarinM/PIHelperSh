using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIHelperSh.RandomEx.Extentions
{
    public static class RandomExtention
    {
        private const string letters = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz1234567890 ";

        #region Получение из массива
        /// <summary>
        /// Получение из массива случайного элемента
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rnd"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T NextItem<T>(this Random rnd, List<T> list) => list[rnd.Next(0, list.Count)];

        /// <summary>
        ///  Получение из массива случайного элемента
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rnd"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T NextItem<T>(this Random rnd, T[] array) => array[rnd.Next(0, array.Length)];

        /// <summary>
        /// Получение случайного символа строки
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static char NextItem(this Random rnd, string str) => str[rnd.Next(0, str.Length)];

        #endregion

        #region Перемешивание массива
        /// <summary>
        /// Метод перемешивания массива
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rnd"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> Shuffle<T>(this Random rnd, List<T> list)
        {
            var count = list.Count;
            for (var i = 0; i < count; i++)
            {
                var index = rnd.Next(count);
                var trade = list[index];
                list[index] = list[i];
                list[i] = trade;
            }
            return list;
        }

        /// <summary>
        /// Метод перемешивания массива
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rnd"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T[] Shuffle<T>(this Random rnd, T[] list)
        {
            var count = list.Length;
            for (var i = 0; i < count; i++)
            {
                var index = rnd.Next(count);
                var trade = list[index];
                list[index] = list[i];
                list[i] = trade;
            }
            return list;
        }

        #endregion

        /// <summary>
        /// Формирования случайной строки по паттерну, чем-то схожему с языком регулярных выражений
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static string NextString(this Random rnd, string regex)
        {

            bool inRange = false;
            StringBuilder sb = new();
            StringBuilder alphavite = new();

            for (int i = 0; i < regex.Length; i++)
            {
                if (inRange) // Либо просто пишем символы из строки, либо выбираем из группы. isRande => из группы
                {
                    if (regex[i] == ']') // Выходим из режима группы, проверяем кол-во и добавляем в итоговую строку
                    {
                        inRange = false;
                        int minCount = -1;
                        int maxCount = -1;
                        if (i + 1 < regex.Length) switch (regex[i + 1])
                            {
                                case '*': //0 или боле повторений
                                    minCount = 0;
                                    maxCount = 100;
                                    i++;
                                    break;
                                case '+'://1 или более повторений
                                    minCount = 1;
                                    maxCount = 100;
                                    i++;
                                    break;
                                case '?'://0 или 1 повторение
                                    minCount = 0;
                                    maxCount = 1;
                                    i++;
                                    break;
                            }

                        if (minCount == -1)
                        {
                            StringBuilder number = new();
                            //Собираем число по циферкам
                            for (int j = i + 1; j < regex.Length; j++)
                            {
                                if (char.IsDigit(regex[j])) number.Append(regex[j]);
                                else break;
                                i++;
                            }
                            if (number.Length > 0)//Если какая-то цифра есть
                                minCount = maxCount = int.Parse(number.ToString());
                            else//Иначе будет единица
                                minCount = maxCount = 1;
                        }

                        for (int j = 0; j < rnd.Next(minCount, maxCount + 1); j++)
                        {
                            sb.Append(alphavite[rnd.Next(0, alphavite.Length)]);
                        }
                        alphavite.Clear();
                        continue;
                    }
                    else if (i + 1 < regex.Length)
                    {
                        if (regex[i] == '\\')
                        {
                            alphavite.Append(regex[i + 1]);
                            i++;
                            continue;
                        }
                        if (regex[i + 1] == '-' && i + 2 < regex.Length)
                        {
                            for (char c = regex[i]; c <= regex[i + 2]; c++)
                            {
                                alphavite.Append(c);
                            }
                            i += 2;
                            continue;
                        }
                    }
                    alphavite.Append(regex[i]);
                }
                else
                {
                    if (regex[i] == '[')
                        inRange = true;//Переходим в множество для выбора
                    else if (regex[i] == '\\' && i + 1 < regex.Length)
                        sb.Append(regex[++i]); //После символа экранирования символ проходит сразу
                    else
                        sb.Append(regex[i]);//Иначе закидываем символ
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Получение случайной строки из букв англиского алфавита обоих регистров, цифр и пробела
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="len">Длина строки</param>
        /// <returns></returns>
        public static string NextString(this Random rnd, int len)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                sb.Append(rnd.NextItem(letters));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Получение случайного цвета
        /// </summary>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public static Color NextColor(this Random rnd) => 
            Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

        /// <summary>
        /// Получение случайного цвета из списка уже существующих
        /// </summary>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public static Color NextExistColor(this Random rnd)
        {
            var vars = Enum.GetValues<KnownColor>();
            return Color.FromKnownColor(vars[rnd.Next(vars.Length - 1)]);
        }
    }
}
