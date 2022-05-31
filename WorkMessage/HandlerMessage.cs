using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using System.Configuration;
using System.Data.SqlClient;
using BotTransfer.Adapters;
namespace BotTransfer.WorkMessage
{
    internal class HandlerMessage
    {
        public static async Task handlerMessage(ITelegramBotClient botClient, Message message)
        {
            bool flag = true;
            int value = 0;
            if (message.Text.ToLower() == "да")
            {
                await botClient.SendTextMessageAsync(message.Chat, $"Введите сумму перевода");
            }
            else if (message.Text.ToLower() == "нет")
            {
                await botClient.SendTextMessageAsync(message.Chat, "Ваш перевод отменен.\nДля создания нового перевода воспользуйтесь кнопкой новый перевод");
            }

            try
            {
                string money = message.Text.ToLower().Trim(new Char[] { ' ', '*', '.', ',', 'р' });

                if (money == null)
                {
                    money = "...";
                }
                value = Convert.ToInt32(money);
            }
            catch (Exception e)
            {
                flag = false;
            }
            if (flag == true)
            {
                string url = API_GetReference.pull(value);
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TestBotTg"].ConnectionString);
                con.Open();

                string queryUpdate = $"UPDATE TestBotTg SET Money={value} WHERE ChatID={Convert.ToInt32(message.Chat.Id)}";
                SqlCommand cmd = new SqlCommand(queryUpdate, con);
                cmd.ExecuteNonQuery();
                con.Close();

                Console.WriteLine("Ввели число");
                if (value > 0)
                {
                    InlineKeyboardMarkup inKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Получить ссылку" ,  $"{url}"),
                    });
                    await botClient.SendTextMessageAsync(message.Chat, "...", replyMarkup: inKeyboard);
                }
            }
        }
    }
}
