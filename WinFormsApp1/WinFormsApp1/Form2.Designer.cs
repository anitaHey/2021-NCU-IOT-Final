
namespace WinFormsApp1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.finishedServices_btn = new System.Windows.Forms.Button();
            this.unfinishedServices_btn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.service_details_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.table_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.service_details_state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // finishedServices_btn
            // 
            this.finishedServices_btn.Location = new System.Drawing.Point(744, 130);
            this.finishedServices_btn.Name = "finishedServices_btn";
            this.finishedServices_btn.Size = new System.Drawing.Size(143, 55);
            this.finishedServices_btn.TabIndex = 6;
            this.finishedServices_btn.Text = "已完成服務";
            this.finishedServices_btn.UseVisualStyleBackColor = true;
            this.finishedServices_btn.Click += new System.EventHandler(this.finishedServices_btn_Click);
            // 
            // unfinishedServices_btn
            // 
            this.unfinishedServices_btn.Location = new System.Drawing.Point(744, 45);
            this.unfinishedServices_btn.Name = "unfinishedServices_btn";
            this.unfinishedServices_btn.Size = new System.Drawing.Size(143, 55);
            this.unfinishedServices_btn.TabIndex = 5;
            this.unfinishedServices_btn.Text = "未完成服務";
            this.unfinishedServices_btn.UseVisualStyleBackColor = true;
            this.unfinishedServices_btn.Click += new System.EventHandler(this.unfinishedServices_btn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(744, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 55);
            this.button1.TabIndex = 4;
            this.button1.Text = "編輯";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button3.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.button3.Location = new System.Drawing.Point(744, 361);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 54);
            this.button3.TabIndex = 7;
            this.button3.Text = "訂餐系統";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(744, 289);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 55);
            this.button2.TabIndex = 8;
            this.button2.Text = "完成";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.service_details_id,
            this.table_id,
            this.service_details_state});
            this.dataGridView1.Location = new System.Drawing.Point(39, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 32;
            this.dataGridView1.Size = new System.Drawing.Size(665, 489);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // service_details_id
            // 
            this.service_details_id.HeaderText = "service_details_id";
            this.service_details_id.MinimumWidth = 8;
            this.service_details_id.Name = "service_details_id";
            this.service_details_id.ReadOnly = true;
            this.service_details_id.Width = 200;
            // 
            // table_id
            // 
            this.table_id.HeaderText = "table_id";
            this.table_id.MinimumWidth = 8;
            this.table_id.Name = "table_id";
            this.table_id.Width = 150;
            // 
            // service_details_state
            // 
            this.service_details_state.HeaderText = "service_details_state";
            this.service_details_state.MinimumWidth = 8;
            this.service_details_state.Name = "service_details_state";
            this.service_details_state.Width = 200;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 562);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.finishedServices_btn);
            this.Controls.Add(this.unfinishedServices_btn);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "服務系統";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button finishedServices_btn;
        private System.Windows.Forms.Button unfinishedServices_btn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn service_details_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn table_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn service_details_state;


    }
}