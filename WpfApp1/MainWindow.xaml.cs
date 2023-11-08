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
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
 
    public partial class MainWindow : Window
    {
        int idWorker = 0;
        db database = new db();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Login(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;
            

            database.openCon();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();

            string sqlQuery = $"select position,Password,Login from Auth where Password ='{ password}' and Login = '{login}'";

            SqlCommand command = new SqlCommand(sqlQuery,database.getConnection());

            adapter.SelectCommand = command;
           
            adapter.Fill(dt);// заносим данные в перемнную

            var role = command.ExecuteScalar();// обявляем переменную для проверки данных в бд

            if(dt.Rows.Count == 1)
            {
                if(role != null&&role.ToString()=="manager")
                {
                  MessageBox.Show("Вы успешно вошли в систему","Сообщение", MessageBoxButton.OK,MessageBoxImage.Information);
                  ManagerWindow Mw = new ManagerWindow();
                  this.Hide();
                  Mw.ShowDialog();
                    this.Show();
                }
                else if(role != null&&role.ToString()== "engineer")
                {
                    WorkerWindow workerWindow = new WorkerWindow();
                    MessageBox.Show("Вы успешно вошли в систему", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Hide();
                    workerWindow.ShowDialog();
                    this.Show();
                }
                else
                    MessageBox.Show("Аккаунт не существует или у вас нет доступа", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
                MessageBox.Show("Аккаунт не существует или у вас нет доступа", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
