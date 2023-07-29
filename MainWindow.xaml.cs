using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Контроль_задач
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        public SqlConnection sqlConnection = null;
        protected List<MakeNewNotes> notes = new List<MakeNewNotes>();
        protected List<StackPanel> panels = new List<StackPanel>();

        public MyTelegramSave telegramSave = null;

 
        public MainWindow()
        {
            InitializeComponent();
            Clock.Start(clock);
           

            Closing += (sender, e) => Application.Current.Shutdown();
            Loaded += (sender, e) => { panels.Add(EntriesInPanel); panels.Add(DonePanel); panels.Add(EntSetingsPanel); };
            Loaded += MainWindow_Loaded;
           
            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Task connectionTask = SqlConectionMethood(); 
            LoadNotes(connectionTask);

            telegramSave = new MyTelegramSave("6368646450:AAGBCksctBOXkG26Ha-of_o3D6EE7oue__E", this.Dispatcher);
            telegramSave.saveFromTG = NewEntery;


           
        }

       
        private async Task SqlConectionMethood()
        {
           await Task.Run(() =>
            {

                try
                {                    
                    sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyNotesDB"].ConnectionString);
                    sqlConnection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "SqlConectionMethood");
                }
            });
        }

        private async void LoadNotes(Task con)
        {
            await Task.WhenAll(con);
                        
            await Task.Run(() =>
            {
                SqlDataReader reader = new SqlCommand("SELECT Label, Text, id FROM Notes", sqlConnection).ExecuteReader();


                try
                {
                    while (reader.Read())
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            notes.Add(new MakeNewNotes(panels, reader.GetString(0), reader.GetString(1), reader.GetString(2), sqlConnection));
                        });
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "LoadNotes");
                }
                finally
                {
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();

                    }
                }
            });
            
        }

        private void New_Entry_Button(object sender, RoutedEventArgs e)
        {            
          NewEnter();
          InvalidInput();
          NewText.Text = null;
          SetLabel.Text = null;
        }

                                              
        private void NewEntery(string LableOfButton, string Enter)
        {
            string id = Guid.NewGuid().ToString().Replace("-", "");
            
            notes.Add(new MakeNewNotes(panels, LableOfButton, Enter, id, sqlConnection));
            
            try
            {
                
                new SqlCommand(
                    $"INSERT INTO Notes ([CreatedAt], [Label], [Text], [id]) VALUES('{DateTime.Now.ToString("yyyy-MM-dd hh:m:ss")}', N'{LableOfButton}', N'{Enter}', '{id}')",
                    sqlConnection).ExecuteNonQuery();
            } 
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message + "New Entery"); 
            }

        }


        private string date = $" \t дата: {DateTime.Now.ToString("yyyy-MM-dd")}";
        private void NewEnter()
        {

            if (string.IsNullOrWhiteSpace(NewText.Text) == false)
            {

                if (string.IsNullOrWhiteSpace(SetLabel.Text) == false && SetLabel.Text.Length < 35)
                {
                    NewEntery($"{SetLabel.Text}{date}", NewText.Text);
                }

                else { NewEntery($"Новая заметка {notes.Count}{date}", NewText.Text); }

            }
        }

        private void InvalidInput()
        {
            if (string.IsNullOrWhiteSpace(NewText.Text) == true)
            {
                NewEntButton.ToolTip = "Отсутствует текст";
                NewEntButton.Background = Brushes.Red;
            }

            else if (string.IsNullOrWhiteSpace(NewText.Text) == false)
            {

                NewEntButton.ToolTip = null;
                NewEntButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(221, 221, 221));

            }



        }



        public delegate void DeleteNote();
        public event DeleteNote OnDelete;

        private void Delete_Selected_Button(object sender, RoutedEventArgs e)
        {
           
            notes.Where(x => x.noteButtons.NeedBeDeleted()).ToList().ForEach(x =>    
            {    
                OnDelete += x.DeleteAll;    
                new SqlCommand($"DELETE FROM Notes WHERE id LIKE '{x.id}'", sqlConnection).ExecuteNonQuery();    
            });                          
          
            if (OnDelete != null)
            {
                OnDelete();
                OnDelete = null;
            }

            notes.RemoveAll(x => x.noteButtons.NeedBeDeleted());
        }




    }

}
