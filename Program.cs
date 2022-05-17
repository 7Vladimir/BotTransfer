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
        static void Main(string[] args)
        {
            MyBot bot = new MyBot();
            bot.Start();
        }
    }
}