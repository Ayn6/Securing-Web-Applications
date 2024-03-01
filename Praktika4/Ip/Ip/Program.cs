using System;
using System.Net;
using System.Net.Sockets;

namespace Ip
{
    class Program
    {
        // Список разрешенных IP-адресов
        static string[] allowedIPs = { "192.168.1.100", "192.168.1.101" };

        static void Main(string[] args)
        {
            // Создание TcpListener для прослушивания подключений
            TcpListener listener = new TcpListener(IPAddress.Any, 8080);
            listener.Start();
            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

                // Проверка разрешенного IP-адреса
                if (IsAllowedIP(clientIP))
                {
                    Console.WriteLine($"Подключение от разрешенного IP-адреса: {clientIP}");
                }
                else
                {
                    Console.WriteLine($"Отклонено подключение от неразрешенного IP-адреса: {clientIP}");
                    client.Close();
                }
            }
        }

        // Метод для проверки, является ли IP-адрес разрешенным
        static bool IsAllowedIP(string ip)
        {
            foreach (string allowedIP in allowedIPs)
            {
                if (allowedIP == ip)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
