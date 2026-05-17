using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAppCirculacion.Forms;

namespace WindowsFormsAppCirculacion
{
    public class AppContext : ApplicationContext
    {
        public AppContext()
        {
            MostrarLogin();
        }

        public void MostrarLogin()
        {
            Form1 frmLogin = new Form1(this);
            frmLogin.Show();
        }

        public void MostrarPanel()
        {
            FrmPanel frmPanel = new FrmPanel(this);
            frmPanel.Show();
        }
    }
}
