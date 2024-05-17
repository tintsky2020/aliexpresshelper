namespace aliexpress
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
            components = new System.ComponentModel.Container();
            dataGridView1 = new DataGridView();
            index = new DataGridViewTextBoxColumn();
            address = new DataGridViewTextBoxColumn();
            highestPrice = new DataGridViewTextBoxColumn();
            originalPrice = new DataGridViewTextBoxColumn();
            delivery = new DataGridViewTextBoxColumn();
            discountRate = new DataGridViewTextBoxColumn();
            deliverydate = new DataGridViewTextBoxColumn();
            richTextBox1 = new RichTextBox();
            button1 = new Button();
            textBox1 = new TextBox();
            richTextBox2 = new RichTextBox();
            button2 = new Button();
            htmlcheck = new CheckBox();
            timer1 = new System.Windows.Forms.Timer(components);
            button3 = new Button();
            button4 = new Button();
            availQuantity = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { index, address, highestPrice, originalPrice, delivery, discountRate, deliverydate, availQuantity });
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(959, 318);
            dataGridView1.TabIndex = 0;
            // 
            // index
            // 
            index.HeaderText = "index";
            index.Name = "index";
            // 
            // address
            // 
            address.HeaderText = "address";
            address.Name = "address";
            // 
            // highestPrice
            // 
            highestPrice.HeaderText = "highestPrice";
            highestPrice.Name = "highestPrice";
            // 
            // originalPrice
            // 
            originalPrice.HeaderText = "originalPrice";
            originalPrice.Name = "originalPrice";
            // 
            // delivery
            // 
            delivery.HeaderText = "delivery";
            delivery.Name = "delivery";
            // 
            // discountRate
            // 
            discountRate.HeaderText = "discountRate";
            discountRate.Name = "discountRate";
            discountRate.Width = 80;
            // 
            // deliverydate
            // 
            deliverydate.HeaderText = "deliverydate";
            deliverydate.Name = "deliverydate";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(648, 336);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(323, 259);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "https://ko.aliexpress.com/item/1005006440054713.html";
            // 
            // button1
            // 
            button1.Location = new Point(720, 601);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "ali button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(648, 601);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(67, 23);
            textBox1.TabIndex = 3;
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(12, 336);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(630, 288);
            richTextBox2.TabIndex = 4;
            richTextBox2.Text = "//\"C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe\" --remote-debugging-port=9222";
            // 
            // button2
            // 
            button2.Location = new Point(801, 601);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 5;
            button2.Text = "nav bttn2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // htmlcheck
            // 
            htmlcheck.AutoSize = true;
            htmlcheck.Location = new Point(887, 603);
            htmlcheck.Name = "htmlcheck";
            htmlcheck.Size = new Size(51, 19);
            htmlcheck.TabIndex = 6;
            htmlcheck.Text = "html";
            htmlcheck.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // button3
            // 
            button3.Location = new Point(567, 601);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 7;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(486, 603);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 8;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // availQuantity
            // 
            availQuantity.HeaderText = "availQuantity";
            availQuantity.Name = "availQuantity";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(977, 634);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(htmlcheck);
            Controls.Add(button2);
            Controls.Add(richTextBox2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(richTextBox1);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private RichTextBox richTextBox1;
        private Button button1;
        private TextBox textBox1;
        private RichTextBox richTextBox2;
        private Button button2;
        private DataGridViewTextBoxColumn index;
        private DataGridViewTextBoxColumn address;
        private DataGridViewTextBoxColumn highestPrice;
        private DataGridViewTextBoxColumn originalPrice;
        private DataGridViewTextBoxColumn delivery;
        private DataGridViewTextBoxColumn discountRate;
        private DataGridViewTextBoxColumn deliverydate;
        private CheckBox htmlcheck;
        private System.Windows.Forms.Timer timer1;
        private Button button3;
        private Button button4;
        private DataGridViewTextBoxColumn availQuantity;
    }
}
