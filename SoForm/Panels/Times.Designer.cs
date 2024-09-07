namespace SoForm.Panels
{
    partial class Times
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
            dataGridView1 = new DataGridView();
            Proceso = new DataGridViewTextBoxColumn();
            Espera = new DataGridViewTextBoxColumn();
            Sistema = new DataGridViewTextBoxColumn();
            mySqlDataAdapter1 = new MySqlConnector.MySqlDataAdapter();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Proceso, Espera, Sistema });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(428, 272);
            dataGridView1.TabIndex = 0;
            // 
            // Proceso
            // 
            Proceso.HeaderText = "Proceso";
            Proceso.MinimumWidth = 6;
            Proceso.Name = "Proceso";
            Proceso.ReadOnly = true;
            Proceso.Width = 125;
            // 
            // Espera
            // 
            Espera.HeaderText = "T.Espera";
            Espera.MinimumWidth = 6;
            Espera.Name = "Espera";
            Espera.ReadOnly = true;
            Espera.Width = 125;
            // 
            // Sistema
            // 
            Sistema.HeaderText = "T.Sistema";
            Sistema.MinimumWidth = 6;
            Sistema.Name = "Sistema";
            Sistema.ReadOnly = true;
            Sistema.Width = 125;
            // 
            // mySqlDataAdapter1
            // 
            mySqlDataAdapter1.DeleteCommand = null;
            mySqlDataAdapter1.InsertCommand = null;
            mySqlDataAdapter1.SelectCommand = null;
            mySqlDataAdapter1.UpdateBatchSize = 0;
            mySqlDataAdapter1.UpdateCommand = null;
            // 
            // Times
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(428, 272);
            Controls.Add(dataGridView1);
            Name = "Times";
            Text = "Times";
            Load += Times_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Proceso;
        private DataGridViewTextBoxColumn Espera;
        private DataGridViewTextBoxColumn Sistema;
        private MySqlConnector.MySqlDataAdapter mySqlDataAdapter1;
    }
}