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
using SD=System.Data;
using MySql.Data.MySqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using DBDB.Clubs;
using System;
using MySqlX.XDevAPI.Relational;
using System.Data.SqlClient;
using System.Data;

namespace DBDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public static int trans;

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Register());
        }

        public MySqlConnection mycon;
        public MySqlCommand mycom;
        public string connect = "server=localhost;username=mysql;password=mysql; database=point p31;charset=utf8;";
        public SD.DataSet ds;

        private void btCon_Click(object sender, RoutedEventArgs e)
        {
            new EditPage().ShowDialog();
        }

        private void btZapr_Click(object sender, RoutedEventArgs e)
        {
            vivod();
        }

        public void vivod()
        {
            try
            {
                string script = "Select id_student, username, hobbie.hobbie, club.club, birthday " +
                    "from student, hobbie, club WHERE student.id_hobbie = hobbie.id_hobbie and student.id_club = club.id_club " +
                    "ORDER BY id_student";
                mycon = new MySqlConnection(connect);
                mycon.Open();
                MySqlDataAdapter ms_data = new MySqlDataAdapter(script, connect);
                SD.DataTable table = new SD.DataTable();
                ms_data.Fill(table);
                dvg.ItemsSource = table.DefaultView;
                mycon.Close();
            }
            catch
            {
                MessageBox.Show("Connection Lost");
            }
        }

        private void btRender_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < dvg.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = dvg.Columns[j].Header;
            }
            for (int i = 0; i < dvg.Columns.Count; i++)
            {
                for (int j = 0; j < dvg.Items.Count; j++)
                {
                    TextBlock b = dvg.Columns[i].GetCellContent(dvg.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }

        private void dvg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonClub_Click(object sender, RoutedEventArgs e)
        {
            Frame2.Navigate(new login());

            dvg.ItemsSource = null;
        }

        private void LoginBt_Click(object sender, RoutedEventArgs e)
        {
            Frame2.Navigate(new Hobbie());

            dvg.ItemsSource = null;
        }

        private void butLog_Click(object sender, RoutedEventArgs e)
        {
            Frame2.Content = null;

            dvg.ItemsSource = null;
        }

        private void btUdal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int udal = dvg.SelectedIndex;
                udal += 1;

                mycon = new MySqlConnection(connect);
                mycom = new MySqlCommand();
                mycom.Connection = mycon;

                mycon.Open();
                mycom.CommandText = "DELETE FROM student WHERE id_student = '" + udal + "';" +
                    "UPDATE student SET id_student = id_student -1 WHERE id_student >'" + udal + "';" +
                    "ALTER TABLE student AUTO_INCREMENT = 1;";
                mycom.ExecuteNonQuery();
                mycon.Close();
                MessageBox.Show("Вы удалили запись!", Title = "Успешно");
                vivod();
            }
            catch
            {
                MessageBox.Show("Ошибка!", Title="Ошибка");
            }

        }
    }
}

