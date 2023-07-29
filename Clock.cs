using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Контроль_задач
{
    static class Clock
    {
                
        public static void Start(TextBlock time)
        {
         DispatcherTimer timer = new DispatcherTimer();
         timer.Interval = TimeSpan.FromSeconds(1);
         timer.Tick += (sender, e) => time.Text = DateTime.Now.ToString("HH:mm:ss");
         timer.Start();
         
        }

        public static void Start(TextBox time)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, e) => time.Text = DateTime.Now.ToString("HH:mm:ss");
            timer.Start();

        }
              

    }
}
