using System.Reflection;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using SoForm.Panels;
using SoForm.Models;
using SoForm.Algorithms;
using SoForm.Helpers;

namespace SoForm
{
    public partial class HomeForm : Form
    {
        private List<ProcessModel> processModels = new List<ProcessModel>();

        public HomeForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(HomeForm_Load);
            this.MaximumSize = this.Size;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Simulador de Algoritmos de Planificación de Procesos";
        }

        private void HomeForm_Load(object? sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Código aquí
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Código aquí
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowForm(FormType.FIFO);
            ShowPanel2(FormType.FIFO);

        }

        private void DatosButton_Click(object sender, EventArgs e)
        {
            ShowForm(FormType.ListProcess);
            panel2.Controls.Clear();
        }

        private void SJF_Click(object sender, EventArgs e)
        {
            ShowForm(FormType.SJF);
            ShowPanel2(FormType.SJF);
        }

        private void Prioridad_Click(object sender, EventArgs e)
        {
            ShowForm(FormType.Prioridad);
            ShowPanel2(FormType.Prioridad);
        }

        private void ShowPanel2(FormType formType)
        {
            processModels = TimeCalculator.CalculateTimes(formType);
            Times window = new Times(processModels);
            PanelManager.ShowFormInPanel(window, panel2);
        }

        private void ShowForm(FormType formType)
        {
            try
            {
                panel1.Controls.Clear();
                Form window = CreateForm(formType);
                PanelManager.ShowFormInPanel(window, panel1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
        }

        private Form CreateForm(FormType formType)
        {
            switch (formType)
            {
                case FormType.FIFO:
                    return new Show(1);
                case FormType.SJF:
                    return new Show(2);
                case FormType.Prioridad:
                    return new Show(3);
                case FormType.ListProcess:
                    return new ListProcess();
                default:
                    throw new ArgumentOutOfRangeException(nameof(formType), formType, null);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void HomeForm_Load_1(object sender, EventArgs e)
        {
        }
    }
}