using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using System.Configuration;
using System.Data.SqlClient;
using BotTransfer.Connection;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotTransfer.WorkKeyboard
{
    internal class BotKeyboard
    {
        public static async Task respKeyboard(ITelegramBotClient botClient, Message message, CallbackQuery callbackQuery)
        {
            if (message == null)
            {
                await botClient.SendTextMessageAsync(callbackQuery.From.Id, callbackQuery.Data.ToString());

                string connectionString = "Data Source=10.55.31.55;Initial Catalog=Myst;Persist Security Info=True;User ID=rom;Password=*New_123#";
                SqlConnection con = new SqlConnection(connectionString);

                bool answer = false;
                answer = SingletonDB.respBit(Convert.ToInt32(callbackQuery.From.Id));

                con.Open();

                if (answer == false)
                {
                    string queryInsert = $"INSERT INTO TestBot(ChatId) VALUES({callbackQuery.From.Id})";
                    SqlCommand sqlCommand = new SqlCommand(queryInsert, con);
                    sqlCommand.ExecuteNonQuery();
                }
                con.Close();
                return;
            }

            if (message.Text == "/button")
            {
                ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
                {
                        new KeyboardButton[]{"Новый перевод"}
                });
                await botClient.SendTextMessageAsync(message.Chat, text: "...", replyMarkup: keyboard);
                return;
            }
        }
    }
}
