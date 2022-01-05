using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
using IOTFinalServer.Model;
using System.Collections.Generic;

namespace IOTFinalServer.ViewModel
{
    class DatabaseManager
    {
        String connString = "server=127.0.0.1;port=3306;username=root;database=iot_final_db;charset=utf8;";
        MySqlConnection conn = new MySqlConnection();

        public List<ServiceData> loadService()
        {
            conn.ConnectionString = connString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            string sql = @"SELECT * FROM `service_details` WHERE 1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            List<ServiceData> list = new List<ServiceData>();

            while (reader.Read())
            {
                ServiceData tem = new ServiceData();
                tem.id = Int32.Parse(reader.GetString("service_details_id"));
                tem.table_id = Int32.Parse(reader.GetString("table_id"));
                tem.state = Int32.Parse(reader.GetString("service_details_state"));

                list.Add(tem);
            }

            conn.Close();

            return list;
        }

        public ObservableCollection<PointData> loadPoint()
        {
            conn.ConnectionString = connString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            string sql = @"SELECT * FROM `iot_table` WHERE 1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            ObservableCollection<PointData> list = new ObservableCollection<PointData>();

            while (reader.Read())
            {
                PointData tem = new PointData();
                tem.pointName = reader.GetString("name");
                tem.X = Double.Parse(reader.GetString("x"));
                tem.Y = Double.Parse(reader.GetString("y"));
                tem.number = int.Parse(reader.GetString("number"));

                list.Add(tem);
            }

            conn.Close();

            return list;
        }

        public void addPoint(PointData point)
        {
            conn.ConnectionString = connString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            string sql = @"INSERT INTO `iot_table`(`name`, `x`, `y`, `number`, `state`) values ('" +
                point.pointName.ToString() + "'," + point.X.ToString() + "," + point.Y.ToString() + ", 2, 0)";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                int index = cmd.ExecuteNonQuery();

                if (index > 0)
                    MessageBox.Show("Add success!", "Success");
                else
                    MessageBox.Show("Error or duplicate content, try again.", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                MessageBox.Show("Error or duplicate content, try again.", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            conn.Close();
        }
    }
}
