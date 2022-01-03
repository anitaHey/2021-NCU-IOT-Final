using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();//產生Form2的物件，才可以使用它所提供的Method
            this.Visible = false;//將Form1隱藏。由於在Form1的程式碼內使用this，所以this為Form1的物件本身
            form1.Visible = true;//顯示第二個視窗
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
                rows.Add(new Object[] { dr["service_details_id"], dr["table_id"], dr["service_details_state"] });

                count++;
            }

            conn.Close();
        }

        private void DBUpdate()//修改完成
        {

            string connetStr = "server=127.0.0.1;port=3306;username=root;database=iot_final_db;SslMode=none;allow zero datetime=true";//database是資料庫名字
            MySqlConnection conn = new MySqlConnection(connetStr);
            // 連線到資料庫

            conn.Open();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string command = "UPDATE `service_details` SET `table_id`='" + row.Cells["table_id"].Value + "',`service_details_state`='" + row.Cells["service_details_state"].Value + "' WHERE `service_details_id`=" + row.Cells["service_details_id"].Value;

                try
                {
                    MySqlCommand cmd = new MySqlCommand(command, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
            }

            conn.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void unfinishedServices_btn_Click(object sender, EventArgs e)
        {
            string command = "SELECT * FROM service_details where service_details_state = 0";
            DBSelect(command);//列出未完成服務
        }

        private void finishedServices_btn_Click(object sender, EventArgs e)
        {

            string command = "SELECT * FROM service_details where service_details_state = 1";
            DBSelect(command);//列出已完成服務
        }

        private void button2_Click(object sender, EventArgs e)//修改完成
        {
            DBUpdate();
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)//編輯
        {
            button2.Enabled = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

       
    }
}
