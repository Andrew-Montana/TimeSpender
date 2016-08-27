using System;
using System.Collections.Generic;
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

using MahApps.Metro.Controls;
using System.IO;
using System.Windows.Threading;
using MySql.Data.MySqlClient;

namespace TimeSpender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        DataBaseQueryes db_query;
        Color color;
        private bool isStop;
        private bool isStart;
        private DispatcherTimer timer;
        private TimeSpan counter;
        private bool isRecorded = false;
        DateTime dt;
        TimeSpan tp;
        private bool isConnected = false;

        public MainWindow()
        {
            InitializeComponent();
            color = (Color)ColorConverter.ConvertFromString("#FF008080"); // main color
            isStop = false;
            isStart = false;
            CheckSettings();
           isConnected = db_query.CreateDataBaseClassic(isConnected);
            // timer
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Interval = TimeSpan.FromSeconds(1);
            if (isConnected == true)
            {
                main_combobox.Items.Clear();
                results_combobox.Items.Clear();
                db_query.FillThemeInComboBox(main_combobox);
                db_query.FillThemeInComboBox(results_combobox);
            }
            Application.Current.Exit += new ExitEventHandler(AppExitAutoSave_Event);
           
        }

        private void AppExitAutoSave_Event(object sender, ExitEventArgs e)
        {
            if (isRecorded == true)
            {
                timer.Stop();
                db_query.SendTimeToDB(main_combobox, counter);
                counter = TimeSpan.FromSeconds(0);
                isRecorded = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter += TimeSpan.FromSeconds(1);
            label_time.Content = string.Format(counter.Hours.ToString() + ":" + counter.Minutes.ToString() + ":" + counter.Seconds.ToString());
            if (counter.TotalSeconds >= 86399)
            {
                db_query.SendTimeToDB(main_combobox, counter);
                counter = TimeSpan.FromSeconds(0);

            }
        }


        // ## GALKA image. events
        private void main_image_galka_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isConnected == true)
            {
                if (main_textbox.Text != "")
                {
                    main_image_galka.Margin = new Thickness(291, 66, 0, 0);
                    string theme = main_textbox.Text;
                    db_query.CreateThemeTable(theme);
                    main_combobox.Items.Clear();
                    results_combobox.Items.Clear();
                    db_query.FillThemeInComboBox(main_combobox);
                    db_query.FillThemeInComboBox(results_combobox);
                    main_textbox.Text = "";
                }
            }
        }

        private void main_image_galka_MouseLeave(object sender, MouseEventArgs e)
        {
            main_image_galka.Margin = new Thickness(291, 64, 0, 0);
        }

        // ## main Start. events
        private void main_start_MouseEnter_1(object sender, MouseEventArgs e)
        {
            if (isStart == false)
                main_start.Foreground = new SolidColorBrush(color);
            else
                if (isStart == true)
                    main_start.Foreground = Brushes.LimeGreen;
        }

        private void main_start_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isStart == false)
            main_start.Foreground = Brushes.White;
            else
                if (isStart == true)
                    main_start.Foreground = Brushes.LimeGreen;
        }

        private void main_start_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isConnected == true)
            {
                if (isStart == false && main_combobox.Text != "")
                {
                    isStop = false;
                    isStart = true;
                    main_startGif.Visibility = Visibility.Visible;
                    main_start.Foreground = Brushes.LimeGreen;
                    main_stop.Foreground = Brushes.White;

                    // database query. checking database timespender and table info.
                    db_query.CreateDataBase(this.timer);
                    isRecorded = true;
                }
                else if (isRecorded == true)
                    isStop = false;
                else
                    MessageBox.Show("Choose your theme!");
            }
        }

        // ## main Stop. events
        private void main_stop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isConnected == true)
            {
                if (isStop == false && isRecorded == true)
                {
                    isStop = true;
                    isStart = false;
                    main_startGif.Visibility = Visibility.Hidden;
                    main_start.Foreground = Brushes.White;
                    main_stop.Foreground = Brushes.Red;
                    timer.Stop();
                    db_query.SendTimeToDB(main_combobox, counter);
                    counter = TimeSpan.FromSeconds(0);
                    isRecorded = false;
                }
            }
        }

        private void main_stop_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isStop == false)
            main_stop.Foreground = new SolidColorBrush(color);
        }

        private void main_stop_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isStop == false)
            main_stop.Foreground = Brushes.White;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {   // Go to 2-d window
            ConnectionToMySql window = new ConnectionToMySql();
            window.ShowDialog();
        }


        // ############ SETTINGS values
        public void CheckSettings()
        {
            if (File.Exists("settings.xml") == true)
            {
                using (StreamReader rd = new StreamReader("settings.xml", System.Text.Encoding.UTF8))
                {
                    string line;
                    string[] lines = { "", "", "", ""};
                    int i = 0;
                    while ((line = rd.ReadLine()) != null)
                    {
                        lines[i] = line;
                        i++;
                    }
                    string server = lines[0];
                    string port = lines[1];
                    string username = lines[2];
                    string password = lines[3];
                    db_query = new DataBaseQueryes(server, port, username, password);
                    main_start.IsEnabled = true; 
                    main_stop.IsEnabled = true;
                    label_gohere.Visibility = Visibility.Hidden;
                }
            }
            else { main_start.IsEnabled = false; main_stop.IsEnabled = false; label_gohere.Visibility = Visibility.Visible; }
        }

        private void MetroWindow_Activated_1(object sender, EventArgs e)
        {
            CheckSettings();
        }

        private void results_button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected == true)
            {
                if (results_combobox.Text != "")
                {
                    bool isNull = false;
                    isNull = db_query.IsInfoNull(isNull);
                    if (isNull == false)
                    {
                     //   MessageBox.Show(db_query.OutputTotalHours(results_combobox, dt, tp));
                        label_TotalHours.Content = db_query.OutputTotalHours(results_combobox, dt, tp);
                    }
                }
            }
        }
    
    }
}
