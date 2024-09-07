namespace SoForm.Panels
{
    partial class ListProcess
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
            components = new System.ComponentModel.Container();
            mySqlDataAdapter1 = new MySqlConnector.MySqlDataAdapter();
            appDbContextBindingSource = new BindingSource(components);
            processDbModelBindingSource = new BindingSource(components);
            appDbContextBindingSource1 = new BindingSource(components);
            dataGridView1 = new DataGridView();
            btnGuardar = new Button();
            btnEliminar = new Button();
            Nuevo = new Button();
            ID = new DataGridViewTextBoxColumn();
            Proceso = new DataGridViewTextBoxColumn();
            Rafaga = new DataGridViewTextBoxColumn();
            Llegada = new DataGridViewTextBoxColumn();
            Prioridad = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)appDbContextBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)processDbModelBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)appDbContextBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // mySqlDataAdapter1
            // 
            mySqlDataAdapter1.DeleteCommand = null;
            mySqlDataAdapter1.InsertCommand = null;
            mySqlDataAdapter1.SelectCommand = null;
            mySqlDataAdapter1.UpdateBatchSize = 0;
            mySqlDataAdapter1.UpdateCommand = null;
            // 
            // appDbContextBindingSource
            // 
            appDbContextBindingSource.DataSource = typeof(Data.AppDbContext);
            // 
            // processDbModelBindingSource
            // 
            processDbModelBindingSource.DataSource = typeof(Models.ProcessDbModel);
            // 
            // appDbContextBindingSource1
            // 
            appDbContextBindingSource1.DataSource = typeof(Data.AppDbContext);
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.DarkSeaGreen;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { ID, Proceso, Rafaga, Llegada, Prioridad });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(879, 576);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(701, 165);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(166, 55);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(701, 251);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(166, 59);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar ";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // Nuevo
            // 
            Nuevo.Location = new Point(746, 181);
            Nuevo.Name = "Nuevo";
            Nuevo.Size = new Size(94, 29);
            Nuevo.TabIndex = 3;
            Nuevo.Text = "Nuevo";
            Nuevo.UseVisualStyleBackColor = true;
            Nuevo.Click += Nuevo_Click;
            // 
            // ID
            // 
            ID.HeaderText = "ID";
            ID.MinimumWidth = 6;
            ID.Name = "ID";
            ID.Width = 125;
            // 
            // Proceso
            // 
            Proceso.HeaderText = "Proceso";
            Proceso.MinimumWidth = 6;
            Proceso.Name = "Proceso";
            Proceso.Width = 125;
            // 
            // Rafaga
            // 
            Rafaga.HeaderText = "Rafaga";
            Rafaga.MinimumWidth = 6;
            Rafaga.Name = "Rafaga";
            Rafaga.Width = 125;
            // 
            // Llegada
            // 
            Llegada.HeaderText = "Tiempo de Llegada";
            Llegada.MinimumWidth = 6;
            Llegada.Name = "Llegada";
            Llegada.Width = 125;
            // 
            // Prioridad
            // 
            Prioridad.HeaderText = "Prioridad";
            Prioridad.MinimumWidth = 6;
            Prioridad.Name = "Prioridad";
            Prioridad.Width = 125;
            // 
            // ListProcess
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(879, 576);
            Controls.Add(btnGuardar);
            Controls.Add(Nuevo);
            Controls.Add(btnEliminar);
            Controls.Add(dataGridView1);
            Name = "ListProcess";
            Text = "ListProcess";
            ((System.ComponentModel.ISupportInitialize)appDbContextBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)processDbModelBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)appDbContextBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private MySqlConnector.MySqlDataAdapter mySqlDataAdapter1;
        private BindingSource appDbContextBindingSource;
        private BindingSource processDbModelBindingSource;
        private BindingSource appDbContextBindingSource1;
        private DataGridView dataGridView1;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button Nuevo;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Proceso;
        private DataGridViewTextBoxColumn Rafaga;
        private DataGridViewTextBoxColumn Llegada;
        private DataGridViewTextBoxColumn Prioridad;
    }
}