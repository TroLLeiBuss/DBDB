using MySql.Data.MySqlClient;
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
using DBDB;
using Microsoft.Office.Interop.Excel;
using SD = System.Data;
using System.Runtime.Remoting.Contexts;

namespace DBDB
{
    /// <summary>
    /// Логика взаимодействия для Open.xaml
    /// </summary>
    public partial class Open : System.Windows.Controls.Page
    {
        public Open()
        {
            InitializeComponent();
        }

        public MySqlConnection mycon;
        public MySqlCommand mycom;
        public string connect = "server=localhost;username=mysql;password=mysql; database=point p31;charset=utf8;";
        public SD.DataSet ds;

        private void BtnReg_Click(object sender, EventArgs e)
        {
            try
            {
                mycom = new MySqlCommand();
                mycon = new MySqlConnection(connect);
                mycon.Open();
                mycom.Connection = mycon;

                mycom.CommandText = "SELECT users FROM loginuser WHERE users='" + TxReg.Text + "'";

                MySqlDataReader rd = mycom.ExecuteReader();

                if (rd.Read())
                {
                    MessageBox.Show("Пользователь " + TxReg.Text + " уже зарегистрирован!");
                    return;
                }
                rd.Close();

                mycom.CommandText = "INSERT INTO loginuser (users, password) VALUES ('" + TxReg.Text + "', " + "'" + CopyPass.Password + "')";

                if (TxReg.Text == "" && CopyPass.Password == "")
                {
                    MessageBox.Show("Не удалось зарегистрироваться! Проверьте логин или пароль.");
                    return;
                }

                mycom.ExecuteNonQuery();
                mycon.Close();

                MessageBox.Show("Аккаунт " + TxReg.Text + " зарегистрирован!", Title="Успешно");
                this.NavigationService.Content = null;
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void RegIn_Click(object sender, EventArgs e)
        {
            this.NavigationService.Content = null;
        }
    }
}
