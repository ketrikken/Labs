namespace WindowsFormsApplication1
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Pointer_X = new System.Windows.Forms.TextBox();
            this.Pointer_Y = new System.Windows.Forms.TextBox();
            this.ans_line = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(962, 295);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 326);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 76);
            this.button1.TabIndex = 1;
            this.button1.Text = "Поехали";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(239, 332);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Введите че хотите, но только от А - Д";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(242, 355);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(111, 20);
            this.textBox1.TabIndex = 3;
            // 
            // Pointer_X
            // 
            this.Pointer_X.Location = new System.Drawing.Point(391, 400);
            this.Pointer_X.Name = "Pointer_X";
            this.Pointer_X.Size = new System.Drawing.Size(36, 20);
            this.Pointer_X.TabIndex = 4;
            // 
            // Pointer_Y
            // 
            this.Pointer_Y.Location = new System.Drawing.Point(473, 400);
            this.Pointer_Y.Name = "Pointer_Y";
            this.Pointer_Y.Size = new System.Drawing.Size(34, 20);
            this.Pointer_Y.TabIndex = 5;
            // 
            // ans_line
            // 
            this.ans_line.AutoSize = true;
            this.ans_line.Location = new System.Drawing.Point(410, 380);
            this.ans_line.Name = "ans_line";
            this.ans_line.Size = new System.Drawing.Size(72, 13);
            this.ans_line.TabIndex = 6;
            this.ans_line.Text = "Координаты ";
            this.ans_line.Click += new System.EventHandler(this.label2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(546, 355);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(111, 20);
            this.textBox2.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(509, 332);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Введите че хотите, но только от А - D";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 444);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.ans_line);
            this.Controls.Add(this.Pointer_Y);
            this.Controls.Add(this.Pointer_X);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox Pointer_X;
        private System.Windows.Forms.TextBox Pointer_Y;
        private System.Windows.Forms.Label ans_line;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
    }
}

