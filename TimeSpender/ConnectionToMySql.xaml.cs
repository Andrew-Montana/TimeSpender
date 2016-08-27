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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.IO;
namespace TimeSpender
{
    /// <summary>
    /// Interaction logic for ConnectionToMySql.xaml
    /// </summary>
    public partial class ConnectionToMySql
    {
        public ConnectionToMySql()
        {
            InitializeComponent();
            PutSettingsInTB();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (server_textbox.Text != "")
            {
              /*  if (File.Exists("settings.xml") == true)
                    File.Delete("settings.xml");
                StreamWriter sw = new StreamWriter("settings.xml");
                sw.WriteLine(server_textbox.Text);
                sw.WriteLine(username_textbox.Text);
                sw.WriteLine(password_textbox.Text);
                sw.Close(); */
                using (StreamWriter sw = new StreamWriter("settings.xml", false, System.Text.Encoding.UTF8))
                {
                sw.WriteLine(server_textbox.Text);
                sw.WriteLine(port_textbox.Text);
                sw.WriteLine(username_textbox.Text);
                sw.WriteLine(password_textbox.Text);
                }
                this.Close();
               // System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
               // Application.Current.Shutdown();
            }
           
        }

        private void PutSettingsInTB()
        {
            if (File.Exists("settings.xml") == true)
             {
                 using (StreamReader rd = new StreamReader("settings.xml", System.Text.Encoding.UTF8))
                 {
                     string line;
                     string[] lines = { "", "", "", "" };
                     int i = 0;
                     while ((line = rd.ReadLine()) != null)
                     {
                        lines[i] = line;
                         i++;
                     }
                         server_textbox.Text = lines[0];
                         port_textbox.Text = lines[1];
                         username_textbox.Text = lines[2];
                         password_textbox.Text = lines[3];
                 }
            }
        }
    }
}
