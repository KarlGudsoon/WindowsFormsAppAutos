using PermisosVehiculos.Models;
using PermisosVehiculos.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAppCirculacion.Forms;

namespace WindowsFormsAppCirculacion
{
    public partial class Form1 : Form
    {

        private readonly AuthService _authService = new AuthService();
        private readonly AppContext _appContext;
        private bool _loginExitoso = false;
        public Form1(AppContext appContext)
        {
            InitializeComponent();
            _appContext = appContext;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (!_loginExitoso) // Solo cierra la app si NO fue login exitoso
            {
                Application.Exit();
            }
        }


        private void btn_sesion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_usuario.Text) ||
                string.IsNullOrWhiteSpace(txt_contrasena.Text))
            {
                lbError.Text = "Completa todos los campos.";
                lbError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // 2. Armar el objeto Usuario con lo ingresado
            Usuario usuario = new Usuario
            {
                NombreUsuario = txt_usuario.Text.Trim(),
                Contrasena = txt_contrasena.Text.Trim()
            };

            // 3. Llamar al servicio — el formulario NO sabe nada de SQL
            try
            {
                bool acceso = _authService.Login(usuario);

                if (acceso)
                {
                    _loginExitoso = true;
                    lbError.Text = "";

                    this.Close();                  // Cierra el login
                    _appContext.MostrarPanel();
                }
                else
                {
                    lbError.Text = "Usuario o contraseña incorrectos.";
                    lbError.ForeColor = System.Drawing.Color.Red;
                    txt_contrasena.Clear();
                    txt_contrasena.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de conexión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
