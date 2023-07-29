using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram;
using System.Threading.Tasks;

namespace Контроль_задач
{
    public interface ITelegramSave
    {
        void SaveFromTG(TelegramBotClient client);

        void DeleteFromTG(TelegramBotClient client);

    }
}
