using OxyPlot;
using SoForm.Algorithms;
using OxyPlot.WindowsForms;


namespace SoForm.Panels
{

    public partial class Show : Form
    {
        private int Option;
        private PlotView plotView;
        private PlotModel plotModel = new PlotModel();

        public Show(int OP)
        {
            Option = OP;
            InitializeComponent();
            InitializePlotView(); // Inicializar el control PlotView
        }

        private void InitializePlotView()
        {
            // Crear e inicializar PlotView
            plotView = new PlotView();
            plotView.Dock = DockStyle.Fill;
            this.Controls.Add(plotView);
        }

        private void load()
        {
            if (Option == 1)
            {
                FIFO fifo = new FIFO();
                this.plotModel = fifo.GeneratePlotModel();
                plotView.Model = this.plotModel; // Asignar el modelo al PlotView
            }else if (Option == 2)
            {
                SJF sjf = new SJF();
                this.plotModel = sjf.GeneratePlotModel();
                plotView.Model = this.plotModel; // Asignar el modelo al PlotView
            }else if (Option == 3)
            {
                Prioridad prioridad = new Prioridad();
                this.plotModel = prioridad.GeneratePlotModel();
                plotView.Model = this.plotModel; // Asignar el modelo al PlotView
            }
        }

        private void FIFO_Load(object sender, EventArgs e)
        {
            load();
        }
        
    }

}