using System;
using System.IO;
using System.Text;
using System.Windows;

namespace WpfProject.ThirdTask
{
    public class ThirdTask
    {
        public void RemoveAllComments(string filename)
        {
            try
            {
                StringBuilder content = new StringBuilder();
                content.Append(File.ReadAllText(filename));
                
                content.Replace("summary", "");

                RemoveComments(content, "/*", "*/");
                RemoveComments(content, "/// <summary>", "/// </summary>");
                RemoveComments(content, "//", Environment.NewLine);

                File.WriteAllText(filename + ".txt", content.ToString());

                MessageBox.Show($"Создан файл {filename}.txt");
            }
            catch (Exception)
            {
                MessageBox.Show("Повторите ввод");
            }
        }

        private void RemoveComments(StringBuilder content, string start, string end)
        {
            string contentStr;
            int startIndex = 0;
            int endIndex = 0;

            //длина концовки;
            //если мы ищем однострочный комментарий значит длина концовки равна нулю
            int endLength = end == Environment.NewLine ? 0 : end.Length;

            while (true)
            {
                contentStr = content.ToString();
                startIndex = contentStr.IndexOf(start, StringComparison.Ordinal);
                endIndex = startIndex != -1 ? contentStr.IndexOf(end, startIndex, StringComparison.Ordinal) : -1;

                if (startIndex != -1 && endIndex != -1)
                {
                    content.Remove(startIndex, endIndex + endLength - startIndex);
                }
                //если нашелся только начальный индекс и мы удаляем однострочный комментарий
                //значит после комментария идет конец файла, в таком случае удаляем все
                //начиная со startIndex
                else if (startIndex != -1 && end == Environment.NewLine)
                {
                    content.Remove(startIndex, content.Length - startIndex);
                }
                //если не найден ни один индекс завершаем поиск
                else
                {
                    break;
                }
            }
        }
    }
}