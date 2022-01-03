using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ubiety.Dns.Core;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            button2.Enabled = true;
           
        }

        private void DBSelect(string command)
        {
            dataGridView1.Rows.Clear();
            string connetStr = "server=127.0.0.1;port=3306;username=root;database=iot_final_db;SslMode=none;allow zero datetime=true";//database是資料庫名字
            MySqlConnection conn = new MySqlConnection(connetStr);
            // 連線到資料庫
       
            conn.Open();

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            MySqlDataAdapter da = new MySqlDataAdapter(command, conn);
            da.Fill(dt);
          
            int count = 0;
            foreach (DataRow dr in dt.Rows)
            {
                DataGridViewRowCollection rows = dataGridView1.Rows;
                rows.Add(new Object[] { dr["order_id"], dr["tableNum"], dr["order_state"], dr["orderTime"], dr["checkTime"] });

                count++;
            }

            conn.Close();
        }

        private void unfinishedOrders_btn_Click(object sender, EventArgs e)
        {
            string command = "SELECT * FROM orders where order_state = 0";
            DBSelect(command);//列出未完成訂單
        }

        private void finishedOrders_btn_Click(object sender, EventArgs e)
        {
            string command = "SELECT * FROM orders where order_state = 1";
            DBSelect(command);//列出已完成訂單
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//將資料傳回資料庫
        {
            DBUpdate();
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();//產生Form2的物件，才可以使用它所提供的Method
            this.Visible = false;//將Form1隱藏。由於在Form1的程式碼內使用this，所以this為Form1的物件本身
            form2.Visible = true;//顯示第二個視窗
        }
        private void DBUpdate()//修改完成
        {
           
            string connetStr = "server=127.0.0.1;port=3306;username=root;database=iot_final_db;SslMode=none;allow zero datetime=true";//database是資料庫名字
            MySqlConnection conn = new MySqlConnection(connetStr);
            // 連線到資料庫

            conn.Open();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string command = "UPDATE `orders` SET `tableNum`='"+ row.Cells["tableNum"].Value + "',`order_state`='"+ row.Cells["order_state"].Value + "',`orderTime`='"+ row.Cells["orderTime"].Value + "',`checkTime`='"+ row.Cells["checkTime"].Value + "' WHERE `order_id`=" + row.Cells["order_id"].Value;

                try
                {
                    MySqlCommand cmd = new MySqlCommand(command, conn);
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {

                }
                   

            }

            conn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
