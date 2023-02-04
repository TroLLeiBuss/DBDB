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
using SD = System.Data;
using MySql.Data.MySqlClient;

namespace DBDB
{
    /// <summary>
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : System.Windows.Window
    {
        public EditPage()
        {
            InitializeComponent();
            hobi.ItemsSource = new Person[]
        {
            new Person { Name = "Пение" },
            new Person { Name = "Баскетбол" },
            new Person { Name = "Рисование" },
            new Person { Name = "Шахматы" },
            new Person { Name = "Математика" },
            new Person { Name = "Волонтёрство" },
            new Person { Name = "Рукоделие" },
        };
            hobi.SelectedIndex = 0;  

            club.ItemsSource = new Person[]
        {
            new Person {Name = "Вокальное пение"},
            new Person { Name = "Баскетбол" },
            new Person {Name = "Юный художник"},
            new Person { Name = "Шахматы" },
            new Person { Name = "Юный математик" },
            new Person { Name = "Юный друг полиции" },
            new Person { Name = "Умелые ручки" },
        };
            club.SelectedIndex = 0;   
        }

        public MySqlConnection mycon;
        public MySqlCommand mycom;
        public string connect = "server=localhost;username=mysql;password=mysql; database=point p31;charset=utf8;";
        public SD.DataSet ds;

        private void btDob_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dateValue = DateTime.Parse(birthday.Text);
                var sqlDateFormat = dateValue.ToString("yyyy.MM.dd");
                mycon = new MySqlConnection(connect);
                mycom = new MySqlCommand();
                mycom.Connection = mycon;

                int hobbie = hobi.SelectedIndex;
                hobbie += 1;
                int clubb = club.SelectedIndex;
                clubb += 1;

                mycon.Open();
                mycom.CommandText = "INSERT INTO student (username, id_hobbie, id_club, birthday) " +
                    "VALUES ('" + name.Text + "', '" + hobbie + "', '" + clubb + "', " +
                    "@birthday)";
                mycom.Parameters.AddWithValue("@birthday", sqlDateFormat);

                mycom.ExecuteNonQuery();
                mycon.Close();

                MessageBox.Show("Данные успешно добавлены!");

                ((MainWindow)Application.Current.MainWindow).vivod();

                this.Close();
            }
            catch
            {
                MessageBox.Show("Данные не добавлены!");
            }

        }
        
    }
    public class Person
    {
        public string Name { get; set; } = "";
        public string Company { get; set; } = "";
        public override string ToString() => $"{Name}";
    }
}