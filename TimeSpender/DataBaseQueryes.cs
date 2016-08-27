using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Windows.Threading;

using MahApps.Metro.Controls;
using System.Windows.Controls;
using System.Threading;
namespace TimeSpender
{
    class DataBaseQueryes
    {
      public string server { get; private set; }
      public string port { get; private set; }
      public string username { get; private set;}
      public string password { get; private set;}

        // constructor
        public DataBaseQueryes(string server, string port, string username, string password)
        {
            this.server = server;
            this.port = port;
            this.username = username;
            this.password = password;
        }

         public void CreateDataBase(DispatcherTimer timer) {
            try
            {
                string ConnectingString = "server=" + server  + ";port=" + port + ";username=" + username + ";password=" + password + ";";
                string query = "CREATE DATABASE IF NOT EXISTS timespender;"; // creating database
                MySqlConnection connect = new MySqlConnection(ConnectingString);
                connect.Open();
                MySqlCommand command = new MySqlCommand(query, connect);
                command.ExecuteNonQuery();
                MySqlDataReader sqlreader = command.ExecuteReader();
                connect.Close();
                CreateTable();
                timer.Start();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); timer.Stop(); }
        }

         public void CreateTable()
         {
             try
             {
                 string ConnectingString = "server=" + server + ";port=" + port + ";username=" + username + ";password=" + password + ";";
                 string create_table = "CREATE TABLE IF NOT EXISTS timespender.info(id int NOT NULL AUTO_INCREMENT,theme varchar(30),date timestamp,PRIMARY KEY(id));";
                 MySqlConnection connect = new MySqlConnection(ConnectingString);
                 connect.Open();
                 MySqlCommand command = new MySqlCommand(create_table, connect);
                 command.ExecuteNonQuery();
                 connect.Close();
             }
             catch (Exception ex) { MessageBox.Show(ex.Message); }
         }

         public void SendTimeToDB(ComboBox combobox1, TimeSpan counter)
         {
             try
             {
                 string connectingString = "server=" + server + ";port=" + port + ";username=" + username + ";password=" + password + ";";
                 string query = "INSERT INTO timespender.info(theme, date) values('" + combobox1.Text + "','" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + " " + counter.Hours.ToString() + ":" + counter.Minutes.ToString() + ":" + counter.Seconds.ToString() +"');";
                 MySqlConnection connect = new MySqlConnection(connectingString);
                 connect.Open();
                 MySqlCommand command = new MySqlCommand(query,connect);
                 command.ExecuteNonQuery();
                 connect.Close();
             }
             catch (Exception ex) { MessageBox.Show(ex.Message); }
         }

         public void CreateThemeTable(string theme)
         {
             try
             {
                 string connectingString = "server=" + server + ";port=" + port + ";username=" + username + ";password=" + password + ";";
                 string query = "CREATE TABLE IF NOT EXISTS timespender.themes(id int NOT NULL AUTO_INCREMENT PRIMARY KEY,theme varchar(30));";
                 MySqlConnection connect = new MySqlConnection(connectingString);
                 connect.Open();
                 MySqlCommand command = new MySqlCommand(query, connect);
                 command.ExecuteNonQuery();
                 connect.Close();

                 // Add new view
                 query = "INSERT INTO timespender.themes(theme) values('" + theme + "');";
                 connect.Open();
                 command = new MySqlCommand(query, connect);
                 //   MySqlDataReader 
                 command.ExecuteNonQuery();
                 connect.Close();
             }
             catch (Exception ex) { MessageBox.Show(ex.Message); }
         }

         public void FillThemeInComboBox(ComboBox combobox1)
         {
             bool isNull = false;
            isNull = IsThemesNull(isNull);
            if (isNull == false)
            {
                try
                {
                    string connectingString = "server=" + server + ";port=" + port + ";username=" + username + ";password=" + password + ";";
                    string query = "SELECT * FROM timespender.themes;";
                    MySqlConnection connect = new MySqlConnection(connectingString);
                    connect.Open();
                    MySqlCommand command = new MySqlCommand(query, connect);
                    command.ExecuteNonQuery();
                    MySqlDataReader sqlreader = command.ExecuteReader();
                    while (sqlreader.Read())
                    {
                        string myitem = sqlreader.GetString("theme");
                        combobox1.Items.Add(myitem);
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
         }

         public string OutputTotalHours(ComboBox combobox2, DateTime dt, TimeSpan tp)
         {
                 string connectingString = "server=" + server + ";port=" + port + ";username=" + username + ";password=" + password + ";";
                 //  string query = "SELECT date FROM timespender.info WHERE theme='" + combobox1.Text + "';";
                 string query = string.Format("SELECT date FROM timespender.info WHERE theme='{0}';", combobox2.Text);
                 MySqlConnection connect = new MySqlConnection(connectingString);
                 connect.Open();
                 MySqlCommand command = new MySqlCommand(query, connect);
                 command.ExecuteNonQuery();
                 MySqlDataReader sqlreader = command.ExecuteReader();

                 DateTime myDate2 = new DateTime(1970, 1, 9, 0, 0, 00);
                 TimeSpan tpResult = TimeSpan.FromSeconds(0);
                 while (sqlreader.Read())
                 {
                     string line = sqlreader.GetString("date").ToString();
                     dt = Convert.ToDateTime(line);
                     tp = dt - myDate2;
                     tpResult += TimeSpan.FromSeconds(tp.Seconds);
                     tpResult += TimeSpan.FromMinutes(tp.Minutes);
                     tpResult += TimeSpan.FromHours(tp.Hours);
                    // Console.WriteLine("tp - " + tp.Hours.ToString() + tp.Minutes.ToString() + tp.Seconds.ToString());
                    // Console.WriteLine("dt - " + dt.ToLongDateString());
                    // Console.WriteLine("line - " + line + "\n ##############");

                     // 3 - дописать += к tpresults чтоб в итоге сделать TotalHours и сверить. Если не понравится то самому написать алгоритм
                     // %60 % 60 и ок для часов.
                     // 4 - проверить не пустые ли страницы перед results output
                 }
                 return string.Format("{0:0.00}",tpResult.TotalHours);

         }

         public bool IsThemesNull(bool isNull)
         {
             string connectingString = string.Format("server={0};port={1};username={2};password={3};", server, port, username, password);
             string query = "CREATE TABLE IF NOT EXISTS timespender.themes(id int NOT NULL AUTO_INCREMENT PRIMARY KEY,theme varchar(30));";
             MySqlConnection connect = new MySqlConnection(connectingString);
             connect.Open();
             MySqlCommand command = new MySqlCommand(query, connect);
             command.ExecuteNonQuery();
             connect.Close();
             query = "SELECT theme FROM timespender.themes;";
             string line = "";
             connect.Open();
             command = new MySqlCommand(query, connect);
             MySqlDataReader sqlReader = command.ExecuteReader();
             while (sqlReader.Read())
             {
                 line = sqlReader.GetString("theme").ToString();
             }
             if (line == "")
                 return isNull = true;
             else
                 return isNull = false;
         }

         public bool IsInfoNull(bool isNull)
         {
             string connectingString = string.Format("server={0};port={1};username={2};password={3};", server, port, username, password);
             string query = "CREATE TABLE IF NOT EXISTS timespender.info(id int NOT NULL AUTO_INCREMENT,theme varchar(30),date timestamp,PRIMARY KEY(id));";
             MySqlConnection connect = new MySqlConnection(connectingString);
             connect.Open();
             MySqlCommand command = new MySqlCommand(query, connect);
             command.ExecuteNonQuery();
             connect.Close();
             query = "SELECT theme FROM timespender.info;";
             string line = "";
             connect.Open();
             command = new MySqlCommand(query, connect);
             MySqlDataReader sqlReader = command.ExecuteReader();
             while (sqlReader.Read())
             {
                 line = sqlReader.GetString("theme").ToString();
             }
             if (line == "")
                 return isNull = true;
             else
                 return isNull = false;
         }

         public bool CreateDataBaseClassic(bool isConnected)
         {
             try
             {
                 string ConnectingString = "server=" + server + ";port=" + port + ";username=" + username + ";password=" + password + ";";
                 string query = "CREATE DATABASE IF NOT EXISTS timespender;"; // creating database
                 MySqlConnection connect = new MySqlConnection(ConnectingString);
                 connect.Open();
                 MySqlCommand command = new MySqlCommand(query, connect);
                 command.ExecuteNonQuery();
                 MySqlDataReader sqlreader = command.ExecuteReader();
                 connect.Close();
                 CreateTable();
                 return isConnected = true;

             }
             catch (Exception ex) { MessageBox.Show(ex.Message); return isConnected = false; }
         }


    }
}
