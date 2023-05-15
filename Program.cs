
using System;
using System.Net;
using System.Text;
using System.Threading;

namespace HttpServerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запуск HTTP-сервера в окремому потоці
            Thread serverThread = new Thread(StartServer);
            serverThread.Start();

            Console.WriteLine("HTTP-сервер запущено. Натисніть Enter для завершення.");
            Console.ReadLine();
        }

        static void StartServer()
        {
            // Створення HTTP-прослуховувача на порту 8080
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();

            while (true)
            {
                try
                {

                    HttpListenerContext context = listener.GetContext();
                    string login = context.Request.QueryString["is-11fiot-21-124"];

                    string surname = "Polishchuk";
                    string firstName = "Victor";
                    int course = 2;
                    string group = "IS-11";
                    string responseText = $"<h1>Your personal data:</h1><p>Surname: {surname}</p><p>First Name: {firstName}</p><p>Course: {course}</p><p>Group: {group}</p>";
                    byte[] responseBytes = Encoding.UTF8.GetBytes(responseText);

                    // Встановлення заголовків відповіді
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "text/html";
                    context.Response.ContentLength64 = responseBytes.Length;

                    // Відправка відповіді
                    context.Response.OutputStream.Write(responseBytes, 0, responseBytes.Length);
                    context.Response.OutputStream.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка при обробці запиту: {ex.Message}");
                }
            }
        }
    }
}