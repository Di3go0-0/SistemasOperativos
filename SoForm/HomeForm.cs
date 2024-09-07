using System.Reflection;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using SoForm.Panels;
using SoForm.Models;
using SoForm.Algorithms;

namespace SoForm
{
    public partial class HomeForm : Form
    {
        private List<ProcessModel> processModels = new List<ProcessModel>();

        public HomeForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(HomeForm_Load);
        }

        private void HomeForm_Load(object sender, EventArgs e)
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
            switch (formType)
            {
                case FormType.FIFO:
                    FIFO fifo = new FIFO();
                    fifo.RunProcess();
                    fifo.CalcularTiempos();
                    processModels = fifo.Procesos;
                    break;
                case FormType.SJF:
                    SJF sjf = new SJF();
                    sjf.RunProcess();
                    sjf.CalcularTiempos();
                    processModels = sjf.Procesos;
                    break;
                case FormType.Prioridad:
                    Prioridad prioridad = new Prioridad();
                    prioridad.RunProcess();
                    prioridad.CalcularTiempos();
                    processModels = prioridad.Procesos;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(formType), formType, null);
            }

            panel2.Controls.Clear();
            Times window = new Times(processModels);

            // Configurar el formulario para que se comporte como un control
            window.TopLevel = false;
            window.FormBorderStyle = FormBorderStyle.None;
            window.Dock = DockStyle.Fill;

            // Agregar el formulario al panel2
            panel2.Controls.Add(window);
            panel2.Tag = window;

            // Mostrar el formulario
            window.Show();
        }
        private void ShowForm(FormType formType)
        {
            try
            {
                // Limpiar el panel de controles existentes
                panel1.Controls.Clear();

                // Verificar si ya hay un formulario ListProcess en el panel
                var existingForm = panel1.Controls.OfType<ListProcess>().FirstOrDefault();
                if (existingForm == null)
                {
                    Form window = CreateForm(formType);

                    // Configurar el formulario para que se comporte como un control
                    window.TopLevel = false;
                    window.FormBorderStyle = FormBorderStyle.None;
                    window.Dock = DockStyle.Fill;

                    // Agregar el formulario al panel1
                    panel1.Controls.Add(window);
                    panel1.Tag = window;

                    // Mostrar el formulario
                    window.Show();
                }
                else
                {
                    MessageBox.Show("El formulario ListProcess ya está en el panel.");
                }
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

        private enum FormType
        {
            FIFO,
            SJF,
            Prioridad,
            ListProcess
        }
    }
}