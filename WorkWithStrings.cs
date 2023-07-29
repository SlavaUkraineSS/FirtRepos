using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Контроль_задач
{
    public class MakeNewNotes : MainWindow
    {

        public string id { get; private set; }         
        public string ent { get; private set; }
        private string label;


        public NoteButtons noteButtons;
        
        public MakeNewNotes(List<StackPanel> panels , string LabelOfButton, string NewEnter, string id, SqlConnection connection)
        {
            this.id = id;
            this.panels = panels;

            ent = NewEnter;
            label = LabelOfButton;
                        
            noteButtons = new NoteButtons(panels, LabelOfButton);

            noteButtons.newEnt.Click += (sender, e) =>
            {
                EditEntery edit = new EditEntery(ent, label);
                
                edit.Editor = (change) =>
                {
                    ent = change;
                    new SqlCommand($"UPDATE Notes SET Text = N'{ent}' WHERE id LIKE '{id}'", connection).ExecuteNonQuery();
                };
            };
                       
        }
                

        public void DeleteAll()
        {
            
            panels[0].Children.Remove(noteButtons.newEnt);
            panels[1].Children.Remove(noteButtons.doneBut);
            panels[2].Children.Remove(noteButtons.remBut);

        }







    }




}
