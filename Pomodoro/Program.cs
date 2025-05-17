using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Reflection;
using Microsoft.Toolkit.Uwp.Notifications;

class Program
{

    static async Task sendTelegramMessage(string msg)
    {
        string botToken = Environment.GetEnvironmentVariable("BOT_TOKEN");
        string chatId = Environment.GetEnvironmentVariable("CHAT_ID");
        string url = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={msg}";

        HttpClient client = new HttpClient();
        HttpResponseMessage res = await client.GetAsync(url);

        if (res.IsSuccessStatusCode)
        {
            Console.WriteLine("Message sent successfully at " + DateTime.Now.ToString("hh:mm:ss tt"));
        }
        else
        {
            Console.WriteLine("Failed to send message. Check your chatID and bot token");
        }
    }
    static async Task Main(string[] args)
    {
        string workMessage = "Ok back to work, hurry up!";
        string breakMessage = "You can take 5 minute break now!";
        string longBreakMessage = "Well done! Your deserved 30 minute break here!";

        while (true)
        {
            for (int i = 0; i <4; i++)
            {
                await sendTelegramMessage(workMessage);
                await Task.Delay(TimeSpan.FromMinutes(25));

                await sendTelegramMessage(breakMessage);
                await Task.Delay(TimeSpan.FromMinutes(5));
            }
            await sendTelegramMessage(longBreakMessage);
            await Task.Delay(TimeSpan.FromMinutes(30));
        }
    }
}