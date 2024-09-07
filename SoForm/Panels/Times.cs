using SoForm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoForm.Panels
{
    public partial class Times : Form
    {
        private List<ProcessModel> processModels = new List<ProcessModel>();
        private double tiempoEsperaPromedio = 0;
        private double tiempoSistemaPromedio = 0;
        public Times(List<ProcessModel> processModels)
        {
            InitializeComponent();
            this.processModels = processModels;

        }

        private void Times_Load(object sender, EventArgs e)
        {
            loadData();
        }
        private void times()
        {
            double tiempoEspera = 0;
            double tiempoSistema = 0;
            foreach (var process in processModels)
            {
                tiempoEspera += process.TiempoEspera;
                tiempoSistema += process.TiempoSistema;
            }
            tiempoEsperaPromedio = tiempoEspera / processModels.Count;
            tiempoSistemaPromedio = tiempoSistema / processModels.Count;
        }

        private void loadData()
        {
            try
            {
                // Calcular los tiempos promedio
                times();

                // Crear una BindingList para manejar los datos del DataGridView
                var bindingList = new BindingList<ProcessModel>(processModels);

                // Crear y agregar la fila de promedios a la BindingList
                bindingList.Add(new ProcessModel
                {
                    Proceso = "Promedio",
                    TiempoEspera = tiempoEsperaPromedio,
                    TiempoSistema = tiempoSistemaPromedio
                });

                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                // Agregar columnas al DataGridView
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Proceso", HeaderText = "Proceso", Name = "Proceso" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TiempoEspera", HeaderText = "T.Espera", Name = "T.Espera" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TiempoSistema", HeaderText = "T.Sistema", Name = "T.Sistema" });

                // Asignar la fuente de datos al DataGridView
                dataGridView1.DataSource = bindingList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}");
            }
        }


    }
}