namespace CG_2
{
    partial class Form1
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.X_Box = new System.Windows.Forms.TextBox();
            this.Y_Box = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ans_line = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1132, 459);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(26, 495);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 56);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // X_Box
            // 
            this.X_Box.Location = new System.Drawing.Point(270, 531);
            this.X_Box.Name = "X_Box";
            this.X_Box.Size = new System.Drawing.Size(37, 20);
            this.X_Box.TabIndex = 2;
            // 
            // Y_Box
            // 
            this.Y_Box.Location = new System.Drawing.Point(368, 531);
            this.Y_Box.Name = "Y_Box";
            this.Y_Box.Size = new System.Drawing.Size(34, 20);
            this.Y_Box.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(228, 534);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(336, 534);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y";
            // 
            // ans_line
            // 
            this.ans_line.AutoSize = true;
            this.ans_line.Location = new System.Drawing.Point(242, 497);
            this.ans_line.Name = "ans_line";
            this.ans_line.Size = new System.Drawing.Size(205, 13);
            this.ans_line.TabIndex = 6;
            this.ans_line.Text = "Введите координаты точки в 5 октанте";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(502, 498);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Введите радиус окружности\r\n";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // RBox
            // 
            this.RBox.Location = new System.Drawing.Point(548, 531);
            this.RBox.Name = "RBox";
            this.RBox.Size = new System.Drawing.Size(34, 20);
            this.RBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(377, 475);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(7, 26);
            this.label4.TabIndex = 9;
            this.label4.Text = "\r\n\r\n";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 573);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.RBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ans_line);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Y_Box);
            this.Controls.Add(this.X_Box);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox X_Box;
        private System.Windows.Forms.TextBox Y_Box;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ans_line;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox RBox;
        private System.Windows.Forms.Label label4;
    }
}

