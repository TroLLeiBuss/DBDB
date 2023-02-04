using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using SD = System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;


namespace DBDB.Clubs
{
    /// <summary>
    /// Логика взаимодействия для Club.xaml
    /// </summary>
    public partial class Club : System.Windows.Controls.Page
    {
        public Club()
        {
            InitializeComponent();
            ClubFrame.Navigate(new MainWindow());
        }

        public MySqlConnection myconclub;
        public MySqlCommand mycomclub;
        public string connectclub = "server=localhost;username=mysql;password=mysql; database=point p31;charset=utf8;";
        public SD.DataSet dsclub;

        private void BtClub_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string script = "Select id_club, club from club";
                myconclub = new MySqlConnection(connectclub);
                myconclub.Open();
                MySqlDataAdapter ms_data = new MySqlDataAdapter(script, connectclub);
                SD.DataTable table = new SD.DataTable();
                ms_data.Fill(table);
                cld.ItemsSource = table.DefaultView;
                myconclub.Close();
            }
            catch
            {
                MessageBox.Show("Connection Lost");
            }
        }
    }
}
