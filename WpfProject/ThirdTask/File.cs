using System.Text;

namespace WpfProject.ThirdTask
{
    public static class File
    {
        public static string Load(string pathFile)
        {
            StringBuilder content = new StringBuilder();
            content.Append(System.IO.File.ReadAllText(pathFile, Encoding.Unicode));
            return content.ToString();
        }

        public static void Save(string path, string contents)
        {
            System.IO.File.WriteAllText(path + "_RESULT.txt", contents,Encoding.Unicode);
        }
    }
}