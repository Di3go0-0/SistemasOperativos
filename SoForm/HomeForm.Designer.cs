namespace SoForm
{
    partial class HomeForm
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
            panel1 = new Panel();
            FIFOButton = new Button();
            DatosButton = new Button();
            SJF = new Button();
            Prioridad = new Button();
            panel2 = new Panel();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(897, 623);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint;
            // 
            // FIFOButton
            // 
            FIFOButton.BackColor = Color.Gray;
            FIFOButton.Location = new Point(1082, 77);
            FIFOButton.Name = "FIFOButton";
            FIFOButton.Size = new Size(220, 59);
            FIFOButton.TabIndex = 3;
            FIFOButton.Text = "FIFO";
            FIFOButton.UseVisualStyleBackColor = false;
            FIFOButton.Click += button1_Click;
            // 
            // DatosButton
            // 
            DatosButton.BackColor = Color.Gray;
            DatosButton.ForeColor = SystemColors.ActiveCaptionText;
            DatosButton.Location = new Point(1082, 12);
            DatosButton.Name = "DatosButton";
            DatosButton.Size = new Size(220, 59);
            DatosButton.TabIndex = 5;
            DatosButton.Text = "Cargar Datos";
            DatosButton.UseVisualStyleBackColor = false;
            DatosButton.Click += DatosButton_Click;
            // 
            // SJF
            // 
            SJF.BackColor = Color.Gray;
            SJF.Location = new Point(1082, 142);
            SJF.Name = "SJF";
            SJF.Size = new Size(220, 59);
            SJF.TabIndex = 6;
            SJF.Text = "SJF";
            SJF.UseVisualStyleBackColor = false;
            SJF.Click += SJF_Click;
            // 
            // Prioridad
            // 
            Prioridad.BackColor = Color.Gray;
            Prioridad.Location = new Point(1082, 207);
            Prioridad.Name = "Prioridad";
            Prioridad.Size = new Size(220, 59);
            Prioridad.TabIndex = 7;
            Prioridad.Text = "Prioridad";
            Prioridad.UseVisualStyleBackColor = false;
            Prioridad.Click += Prioridad_Click;
            // 
            // panel2
            // 
            panel2.Location = new Point(963, 273);
            panel2.Name = "panel2";
            panel2.Size = new Size(429, 362);
            panel2.TabIndex = 8;
            panel2.Paint += panel2_Paint;
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkOliveGreen;
            ClientSize = new Size(1404, 673);
            Controls.Add(panel2);
            Controls.Add(Prioridad);
            Controls.Add(SJF);
            Controls.Add(DatosButton);
            Controls.Add(FIFOButton);
            Controls.Add(panel1);
            Name = "HomeForm";
            Text = "Form1";
            Load += HomeForm_Load_1;
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Button FIFOButton;
        private Button DatosButton;
        private Button SJF;
        private Button Prioridad;
        private Panel panel2;
    }
}