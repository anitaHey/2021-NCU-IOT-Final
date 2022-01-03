
namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.unfinishedOrders_btn = new System.Windows.Forms.Button();
            this.finishedOrders_btn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.order_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.orderTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(919, 265);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "編輯";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // unfinishedOrders_btn
            // 
            this.unfinishedOrders_btn.Location = new System.Drawing.Point(919, 98);
            this.unfinishedOrders_btn.Name = "unfinishedOrders_btn";
            this.unfinishedOrders_btn.Size = new System.Drawing.Size(143, 55);
            this.unfinishedOrders_btn.TabIndex = 2;
            this.unfinishedOrders_btn.Text = "未完成訂單";
            this.unfinishedOrders_btn.UseVisualStyleBackColor = true;
            this.unfinishedOrders_btn.Click += new System.EventHandler(this.unfinishedOrders_btn_Click);
            // 
            // finishedOrders_btn
            // 
            this.finishedOrders_btn.Location = new System.Drawing.Point(919, 183);
            this.finishedOrders_btn.Name = "finishedOrders_btn";
            this.finishedOrders_btn.Size = new System.Drawing.Size(143, 55);
            this.finishedOrders_btn.TabIndex = 3;
            this.finishedOrders_btn.Text = "已完成訂單";
            this.finishedOrders_btn.UseVisualStyleBackColor = true;
            this.finishedOrders_btn.Click += new System.EventHandler(this.finishedOrders_btn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.order_id,
            this.tableNum,
            this.order_state,
            this.orderTime,
            this.checkTime});
            this.dataGridView1.Location = new System.Drawing.Point(35, 53);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 32;
            this.dataGridView1.Size = new System.Drawing.Size(848, 560);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // order_id
            // 
            this.order_id.HeaderText = "order_id";
            this.order_id.MinimumWidth = 8;
            this.order_id.Name = "order_id";
            this.order_id.ReadOnly = true;
            this.order_id.Width = 150;
            // 
            // tableNum
            // 
            this.tableNum.HeaderText = "tableNum";
            this.tableNum.MinimumWidth = 8;
            this.tableNum.Name = "tableNum";
            this.tableNum.Width = 150;
            // 
            // order_state
            // 
            this.order_state.HeaderText = "order_state";
            this.order_state.MinimumWidth = 8;
            this.order_state.Name = "order_state";
            this.order_state.Width = 150;
            // 
            // orderTime
            // 
            this.orderTime.HeaderText = "orderTime";
            this.orderTime.MinimumWidth = 8;
            this.orderTime.Name = "orderTime";
            this.orderTime.Width = 150;
            // 
            // checkTime
            // 
            this.checkTime.HeaderText = "checkTime";
            this.checkTime.MinimumWidth = 8;
            this.checkTime.Name = "checkTime";
            this.checkTime.Width = 150;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(919, 335);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 55);
            this.button2.TabIndex = 5;
            this.button2.Text = "完成";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button3.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.button3.Location = new System.Drawing.Point(919, 448);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 54);
            this.button3.TabIndex = 6;
            this.button3.Text = "服務系統";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 651);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.finishedOrders_btn);
            this.Controls.Add(this.unfinishedOrders_btn);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "訂餐系統";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button unfinishedOrders_btn;
        private System.Windows.Forms.Button finishedOrders_btn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn tableNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_state;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkTime;
    }
}

