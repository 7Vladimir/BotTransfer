using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using BotTransfer.Interface;
using BotTransfer.Exceptions;
using BotTransfer.WorkMessage;
using BotTransfer.Connection;
namespace TestBot
{
    internal class Program
    {
        static extern IntPtr GetConsoleWindow();
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        static void Main(string[] args)
        {
            try
            {

                MyBot bot = new MyBot();
                bot.Start();

                var handle = GetConsoleWindow();
                ShowWindow(handle, SW_HIDE);
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
    }
}