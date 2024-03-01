using System;
using System.Diagnostics;

namespace Os
{
    using System;
    using System.Diagnostics;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите команду для выполнения:");
            string command = Console.ReadLine();

            // Подверженное OS injection
            string result = ExecuteCommand(command);

            // Вывод результата
            Console.WriteLine("Результат выполнения команды:");
            Console.WriteLine(result);
        }

        static string ExecuteCommand(string command)
        {
            // Создаем новый процесс
            Process process = new Process();
            // Устанавливаем имя исполняемого файла (команды)
            process.StartInfo.FileName = "cmd.exe";
            // Разрешаем использование оболочки операционной системы
            process.StartInfo.UseShellExecute = false;
            // Запускаем процесс с командой пользователя
            process.StartInfo.Arguments = "/c " + command;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            // Обработка вывода команды
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            // Формируем и возвращаем результат выполнения команды
            return output + "\n" + error;
        }
    }


}
