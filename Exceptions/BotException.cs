using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace BotTransfer.Exceptions
{
    public static class BotException
    {
        public static async Task respException(ITelegramBotClient botClient , Exception exception , CancellationToken cancellationToken)
        {
            Console.WriteLine("Возникла ошибка!" + exception.Message);
        }
    }
}
