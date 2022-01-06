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

        public List<OrderData> loadOrder()
        {
            conn.ConnectionString = connString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            string sql = @"SELECT * FROM `orders` WHERE 1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            List<OrderData> list = new List<OrderData>();

            while (reader.Read())
            {
                OrderData tem = new OrderData();
                tem.order_id = Int32.Parse(reader.GetString("order_id"));
                tem.table_id = Int32.Parse(reader.GetString("tableNum"));
                tem.state = Int32.Parse(reader.GetString("order_state"));
                tem.orderTime = reader.GetString("orderTime");
                tem.checkTime = reader.GetString("checkTime");

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

            string sql = @"SELECT * FROM `tables` WHERE 1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            ObservableCollection<PointData> list = new ObservableCollection<PointData>();

            while (reader.Read())
            {
                PointData tem = new PointData();
                tem.id = Int32.Parse(reader.GetString("table_id"));
                tem.pointName = reader.GetString("table_name");
                tem.X = Double.Parse(reader.GetString("table_x"));
                tem.Y = Double.Parse(reader.GetString("table_y"));
                tem.number = int.Parse(reader.GetString("customerCount"));

                list.Add(tem);
            }

            conn.Close();

            return list;
        }

        public DishDataList loadMenu()
        {
            conn.ConnectionString = connString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            string sql = @"SELECT * FROM `menu` WHERE 1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader reader = cmd.ExecuteReader();

            ObservableCollection<DishData> list = new ObservableCollection<DishData>();

            while (reader.Read())
            {
                DishData tem = new DishData();
                tem.id = int.Parse(reader.GetString("id"));
                tem.name = reader.GetString("name");
                tem.content = reader.GetString("content");
                tem.price = int.Parse(reader.GetString("price"));

                list.Add(tem);
            }

            conn.Close();
            DishDataList dishDataList = new DishDataList();
            dishDataList.list = list;

            return dishDataList;
        }

        public int addPoint(PointData point)
        {
            int insert_id = -1;
            conn.ConnectionString = connString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            string sql = @"INSERT INTO `tables`(`table_name`, `table_x`, `table_y`, `customerCount`, `state`) values ('" +
                point.pointName.ToString() + "'," + point.X.ToString() + "," + point.Y.ToString() + ", 2, 0)";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                int index = cmd.ExecuteNonQuery();
                insert_id = (int)cmd.LastInsertedId;

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

            return insert_id;
        }

        public int createNewOrder(int table_id)
        {
            int insert_id = -1;
            conn.ConnectionString = connString;
            if (conn.State != ConnectionState.Open)
                conn.Open();
            String timeStamp = DateTime.Now.ToString();
            string sql = @"INSERT INTO `orders`(`tableNum`, `order_state`, `orderTime`, `checkTime`) values ('" +
                table_id + ", 0, " + timeStamp + ", NULL)";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                int index = cmd.ExecuteNonQuery();
                insert_id = (int)cmd.LastInsertedId;
            }
            catch
            {
                Console.WriteLine("error");
            }

            conn.Close();

            return insert_id;
        }
    }
}
