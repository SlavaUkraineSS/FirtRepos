using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using System.Threading;
using System.Windows.Threading;

namespace Контроль_задач
{
    public class MyTelegramSave //: ITelegramSave
    {

        public TelegramBotClient botClient = null;

        public delegate void MyTelegramSaveHandler(string LableOfButton, string Enter);
        public MyTelegramSaveHandler saveFromTG { get; set; }
        public Dispatcher mainThread = null;

        public MyTelegramSave(string token, Dispatcher mainThread)
        {
            this.mainThread = mainThread;
            botClient = new TelegramBotClient(token);

            botClient.StartReceiving(Update, Error);
        }

        private async Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            
        }

        public async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var message = update.Message;

            if (message.Text != null)
            {

                if (message.Text.ToLower().Contains("заметка:"))
                {
                    mainThread.Invoke(() =>
                    {
                        saveFromTG("lable", update.Message.Text.Replace("заметка:", ""));
                    });
                }

            }

        }

        //public void SaveFromTG(TelegramBotClient client)
        //{

        //}

        public void DeleteFromTG(TelegramBotClient client)
        {

        }

    }
}
