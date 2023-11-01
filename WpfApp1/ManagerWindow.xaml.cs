﻿using System;
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
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace WpfApp1
{
    
    public partial class ManagerWindow : Window
    {
        db database = new db();
        public ManagerWindow()
        {
            InitializeComponent();
            
        }
        private void Loaddata()
        {
            string query = "SELECT * FROM Applications";
            SqlDataAdapter adapter = new SqlDataAdapter(query,database.getConnection());
           
            DataSet ds = new DataSet();
            adapter.Fill(ds,"Applications");

            Colums.ItemsSource = ds.Tables["Applications"].DefaultView;
        }
        private void Show_Data(object sender, RoutedEventArgs e)
        {
            Loaddata();
        }

        private void Colums_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Status") 
            {
                var comboBoxColumn = new DataGridComboBoxColumn
                {
                    Header = "Status",
                    SelectedValueBinding = new Binding("Status"),
                    ItemsSource = new List<string> { "complete", "in process", "confirmation" }
                };
                e.Column = comboBoxColumn;
            }
        }

        private void SaveData_Click(object sender, RoutedEventArgs e)
        {
            database.openCon();
            DataTable dt = ((DataView)Colums.ItemsSource).ToTable();
            foreach (DataRow row in dt.Rows) 
            {
                int idApplication = Convert.ToInt32(row["idApplication"]); 
                string status = row["Status"].ToString();
                
                string query = "UPDATE Applications SET Status = @Status  WHERE idApplication = @idApplication";
                SqlCommand command = new SqlCommand(query, database.getConnection());
                command.Parameters.AddWithValue("@idApplication", idApplication);
                command.Parameters.AddWithValue("@Status", status);
               

                command.ExecuteNonQuery();
            }
            foreach( DataRow row in dt.Rows)
            {
                int idApplication = Convert.ToInt32(row["idApplication"]);
                int id = Convert.ToInt32(row["id"]);
                int idWorker = Convert.ToInt32 (row["idWorker"]);
                string TypeDestruction = row["TypeDestruction"].ToString();
                string ProblemDescription = row["ProblemDescription"].ToString();
                string Date = row["Date"].ToString();

                string query = "UPDATE Applications SET id = @id ,idWorker = @idWorker,TypeDestruction=@TypeDestruction,ProblemDescription=@ProblemDescription, Date=@Date  WHERE idApplication = @idApplication";
                SqlCommand command = new SqlCommand(query, database.getConnection());

                command.Parameters.AddWithValue("@idApplication", idApplication);
                command.Parameters.AddWithValue("@idWorker", idWorker);
                command.Parameters.AddWithValue("@TypeDestruction", TypeDestruction);
                command.Parameters.AddWithValue("@ProblemDescription", ProblemDescription);
                command.Parameters.AddWithValue("@Date", Date);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            
        }
    }
}