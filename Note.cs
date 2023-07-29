using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Контроль_задач
{
    public class NoteButtons
    {

        public Button newEnt { get; private set; }
        public Button doneBut { get; private set; }
        public Button remBut { get; private set; }


        public NoteButtons(List<StackPanel> panels, string label)
        {
            newEnt =  new Button { Content = label };
            doneBut = new Button { Content = " " };
            remBut = new Button { Content = " " };

            panels[0].Children.Add(newEnt);
            panels[1].Children.Add(doneBut);
            panels[2].Children.Add(remBut);


            DoneButton();
            SelectRemoveButton();

        }

        

        private void DoneButton()
        {
            doneBut.Click += (sender, e) =>
            {
                if ((string)doneBut.Content == " ")
                {
                    doneBut.Content = "✔";
                    doneBut.Background = Brushes.DarkGray;
                    newEnt.Background = Brushes.DarkGray;
                }
                else
                {
                    doneBut.Content = " ";
                    doneBut.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                    newEnt.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));

                }

            };

        }

        private void SelectRemoveButton()
        {
            remBut.Click += (sender, e) =>
            {
                if ((string)remBut.Content == " ")
                {
                    remBut.Content = "Delete";

                }
                else
                {
                    remBut.Content = " ";
                };
            };
        }

        public bool NeedBeDeleted()
        {
            if((string)remBut.Content == "Delete")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
