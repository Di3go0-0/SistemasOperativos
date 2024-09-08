using System;
using System.Windows.Forms;

namespace SoForm.Helpers
{
    public class PanelManager
    {
        public static void ShowFormInPanel(Form form, Panel panel)
        {
            panel.Controls.Clear();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panel.Controls.Add(form);
            panel.Tag = form;
            form.Show();
        }
    }
}