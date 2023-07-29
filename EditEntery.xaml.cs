using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// <summary>
    /// Логика взаимодействия для EditEntery.xaml
    /// </summary>
    


    public partial class EditEntery
    {

        private string NewChange = null;

        public delegate void EditorAttribute(string editStr);
        public EditorAttribute Editor { get; set; }
     
        public EditEntery(string Enteries, string Label)
        {
            InitializeComponent();
            Title = Label;
            EditEnteryTextBox.Text = Enteries;
            NewChange = Enteries;
            Title = Label;
        }

        private void Save_Changes(object sender, RoutedEventArgs e)
        {
            NewChange = EditEnteryTextBox.Text;
            Editor(NewChange);
        }
      
    }
}
