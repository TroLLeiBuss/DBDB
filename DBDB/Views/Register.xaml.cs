using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using DBDB;
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using SD = System.Data;

namespace DBDB
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : System.Windows.Controls.Page
    { 

        public Register()
        {
            InitializeComponent();
        }

        public MySqlConnection mycon;
        public MySqlCommand mycom;
        public string connect = "server=localhost;username=mysql;password=mysql; database=point p31;charset=utf8;";
        public SD.DataSet ds;

        private void BtnSignin_Click(object sender, EventArgs e)
        {
            try
            {
                string CommandText = "SELECT users, password FROM loginuser " +
                "WHERE users='" + TxbLogin.Text + "' AND password='" + TxbPassword.Password + "'";
                string Connect = "server=localhost;username=mysql;password=mysql; database=point p31;charset=utf8;";

                MySqlConnection myConnection = new MySqlConnection(Connect);
                MySqlCommand myCommand = new MySqlCommand(CommandText, myConnection);

                myConnection.Open(); //Устанавливаем соединение с базой данных.

                MySqlDataReader rd = myCommand.ExecuteReader();

                if (rd.Read())
                {
                    MessageBox.Show("Добро пожаловать - "+ TxbLogin.Text + "!"); // получаем сообщение "null - null"
                    this.NavigationService.Content = null;
                    rd.Close();
                    myConnection.Close(); //Обязательно закрываем соединение!
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                    rd.Close();
                    myConnection.Close();
                }
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void RegIn_Click(object sender, EventArgs e)
        {
            RegOut.Navigate(new Open());
        }
    }
}

//regIn.Visibility = Visibility.Collapsed;
//MessageBox.Show(check_Login + " - " + check_Password); // получаем сообщение "null - null"
