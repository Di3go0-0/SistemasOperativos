using SoForm.Models;
using SoForm.Repository;
using SoForm.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace SoForm.Panels
{
    public partial class ListProcess : Form
    {
        private ProcessRepositoy _processRepository;
        private List<ProcessDbModel> _processList;


        public ListProcess()
        {
            InitializeComponent();
            _processRepository = new ProcessRepositoy(new AppDbContext()); // Asegúrate de que AppDbContext esté bien configurado
            _processList = new List<ProcessDbModel>();
            LoadData();
            dataGridView1.AllowUserToAddRows = true;
        }

        private async void LoadData()
        {
            try
            {
                _processList = await _processRepository.GetAll();
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

                // Agregar columnas
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Id", Name = "ID" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Proceso", HeaderText = "Proceso", Name = "Proceso" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Rafaga", HeaderText = "Rafaga", Name = "Rafaga" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Llegada", HeaderText = "Llegada", Name = "Llegada" });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Prioridad", HeaderText = "Prioridad", Name = "Prioridad" });

                // Asignar los datos
                dataGridView1.DataSource = new BindingList<ProcessDbModel>(_processList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}");
            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        // Método para validar las filas y marcar las modificadas
        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Marcar la fila como modificada si el usuario cambió algún valor
            if (dataGridView1.IsCurrentRowDirty)
            {
                dataGridView1.Rows[e.RowIndex].Tag = "modified"; // Etiquetamos la fila como modificada
            }
        }
        private async void btnGuardar_Click(object sender, EventArgs e)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsNewRow) continue; // Saltar las filas que no contienen datos
                        if (row.Tag != null && row.Tag.ToString() == "saved") continue; // Saltar las filas ya guardadas

                        var idCell = row.Cells["ID"].Value;
                        var proceso = row.Cells["Proceso"].Value?.ToString();
                        var rafaga = Convert.ToInt32(row.Cells["Rafaga"].Value);
                        var llegada = Convert.ToInt32(row.Cells["Llegada"].Value);
                        var prioridad = Convert.ToInt32(row.Cells["Prioridad"].Value);

                        if (string.IsNullOrWhiteSpace(proceso))
                        {
                            MessageBox.Show("El nombre del proceso no puede estar vacío.");
                            continue;
                        }

                        var processDbModel = new ProcessDbModel
                        {
                            Proceso = proceso,
                            Rafaga = rafaga,
                            Llegada = llegada,
                            Prioridad = prioridad
                        };

                        // Si el ID es nulo o vacío, es un nuevo proceso (inserción)
                        if (idCell == null || string.IsNullOrWhiteSpace(idCell.ToString()))
                        {
                            await _processRepository.Create(processDbModel);
                        }
                        else
                        {
                            // Si el ID existe, se trata de una actualización
                            processDbModel.Id = Convert.ToInt32(idCell); // Asegúrate de asignar el ID al modelo
                            await _processRepository.Update(processDbModel);
                        }

                        row.Tag = "saved"; // Marcar la fila como guardada
                    }

                    LoadData(); // Recargar los datos
                    MessageBox.Show("Datos guardados correctamente.");
                }
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var idsToDelete = new List<int>();

                // Recopilar todos los IDs de las filas seleccionadas
                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                {
                    var id = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                    idsToDelete.Add(id);
                }

                try
                {
                    // Eliminar cada proceso por su ID
                    foreach (var id in idsToDelete)
                    {
                        await _processRepository.Delete(id);
                    }

                    LoadData(); // Recargar los datos
                    MessageBox.Show("Procesos eliminados correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar los procesos: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Seleccione al menos un proceso para eliminar.");
            }
        }


        private void Nuevo_Click(object sender, EventArgs e)
        {
            // Crear un nuevo objeto del tipo ProcessDbModel con valores por defecto
            var nuevoProceso = new ProcessDbModel
            {
                Proceso = "New",
                Rafaga = 0,
                Llegada = 0,
                Prioridad = 0
            };

            // Agregar el nuevo proceso a la lista en memoria
            _processList.Add(nuevoProceso);

            // Actualizar el DataGridView con la lista modificada
            dataGridView1.DataSource = new BindingList<ProcessDbModel>(_processList);

            // Enfocar la fila nueva en el DataGridView para que el usuario pueda empezar a editar
            int newRowIndex = dataGridView1.Rows.Count - 1;
            dataGridView1.CurrentCell = dataGridView1.Rows[newRowIndex].Cells[1]; // Mover el foco a la primera celda editable (Proceso)

            // Habilitar la edición de la fila
            dataGridView1.BeginEdit(true);
        }





    }
}
