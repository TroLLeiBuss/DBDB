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
using SD = System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace DBDB
{
    /// <summary>
    /// Логика взаимодействия для login.xaml
    /// </summary>
    public partial class login : System.Windows.Controls.Page
    {
        public MainWindow mainWindow;

        public login()
        {
            InitializeComponent();
        }

        public MySqlConnection mycon;
        public MySqlCommand mycom;
        public string connect = "server=localhost;username=mysql;password=mysql; database=point p31;charset=utf8;";
        public SD.DataSet ds;

        private void ButtonTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string script = "Select * from club";
                mycon = new MySqlConnection(connect);
                mycon.Open();
                MySqlDataAdapter ms_data = new MySqlDataAdapter(script, connect);
                SD.DataTable table = new SD.DataTable();
                ms_data.Fill(table);
                cld.ItemsSource = table.DefaultView;
                mycon.Close();
            }
            catch
            {
                MessageBox.Show("Connection Lost");
            }
        }

        private void Ecsport_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < cld.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true;
                sheet1.Columns[j + 1].ColumnWidth = 15;
                myRange.Value2 = cld.Columns[j].Header;
            }
            for (int i = 0; i < cld.Columns.Count; i++)
            {
                for (int j = 0; j < cld.Items.Count; j++)
                {
                    TextBlock b = cld.Columns[i].GetCellContent(cld.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }

        private void butLog_Click(object sender, EventArgs e)
        {
            this.NavigationService.Content = null;
        }

        private void cld_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btDance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int udal = cld.SelectedIndex;
                udal += 1;

                mycon = new MySqlConnection(connect);
                mycom = new MySqlCommand();
                mycom.Connection = mycon;

                mycon.Open();
                mycom.CommandText = "DELETE FROM club WHERE id_club = '" + udal + "';" +
                    "UPDATE club SET id_club = id_club -1 WHERE id_club >'" + udal + "';" +
                    "ALTER TABLE club AUTO_INCREMENT = 1;";
                mycom.ExecuteNonQuery();
                mycon.Close();
                MessageBox.Show("Вы удалили запись!", Title = "Успешно");
            }
            catch
            {
                MessageBox.Show("Ошибка!", Title = "Ошибка");
            }

        }

        private void btRedact_Click(object sender, RoutedEventArgs e)
        {
            new EditPage().ShowDialog();
        }
    }
}
