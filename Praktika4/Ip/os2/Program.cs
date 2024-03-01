using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace os2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите команду для выполнения:");
            string userInput = Console.ReadLine();

            // Проверяем пользовательский ввод на наличие недопустимых символов
            if (!IsValidInput(userInput))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                return;
            }

            // Защищенное от OS injection
            ExecuteCommandSafely(userInput);
        }

        static void ExecuteCommandSafely(string command)
        {
            // Создаем новый процесс
            Process process = new Process();
            // Устанавливаем имя исполняемого файла (команды)
            process.StartInfo.FileName = "cmd.exe";
            // Запрещаем использование оболочки операционной системы
            process.StartInfo.UseShellExecute = false;
            // Перенаправляем стандартный поток ввода
            process.StartInfo.RedirectStandardInput = true;
            // Перенаправляем стандартный поток вывода
            process.StartInfo.RedirectStandardOutput = true;
            // Перенаправляем стандартный поток ошибок
            process.StartInfo.RedirectStandardError = true;
            // Запускаем процесс
            process.Start();

            // Пишем команду в стандартный поток ввода
            process.StandardInput.WriteLine(command);
            process.StandardInput.Flush();
            process.StandardInput.Close();

            // Получаем вывод из стандартного потока вывода
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            // Выводим результат
            Console.WriteLine("Результат выполнения команды:");
            Console.WriteLine(output);
            Console.WriteLine("Ошибки:");
            Console.WriteLine(error);

            // Ждем завершения процесса
            process.WaitForExit();
            process.Close();
        }

        static bool IsValidInput(string input)
        {
            // Проверяем пользовательский ввод на наличие недопустимых символов
            // В данном примере разрешаем только буквы, цифры, пробелы и знаки препинания
            Regex regex = new Regex(@"^[a-zA-Z0-9\s\p{P}]+$");
            return regex.IsMatch(input);
        }
    }

}
