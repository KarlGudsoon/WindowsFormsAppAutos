using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PermisosVehiculos.Models;
using PermisosVehiculos.Services;

namespace PermisosVehiculos
{
    public partial class FrmEditarContribuyente : Form
    {
        private readonly ContribuyenteService _contribuyenteService = new ContribuyenteService();
        private readonly Contribuyente _contribuyente;

        public FrmEditarContribuyente(Contribuyente contribuyente)
        {
            InitializeComponent();
            _contribuyente = contribuyente;

            txt_rut.Text = _contribuyente.Rut;
            txt_nombre.Text = _contribuyente.Nombre;
            txt_apellido.Text = _contribuyente.Apellido;
            txt_nacionalidad.Text = _contribuyente.Nacionalidad;
            txt_direccion.Text = _contribuyente.Direccion;
            txt_comuna.Text = _contribuyente.Comuna;

            txt_rut.Enabled = false;
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_nombre.Text) ||
                string.IsNullOrWhiteSpace(txt_apellido.Text) ||
                string.IsNullOrWhiteSpace(txt_nacionalidad.Text) ||
                string.IsNullOrWhiteSpace(txt_direccion.Text) ||
                string.IsNullOrWhiteSpace(txt_comuna.Text))
            {
                MessageBox.Show("Completa todos los campos.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _contribuyente.Nombre = txt_nombre.Text.Trim();
                _contribuyente.Apellido = txt_apellido.Text.Trim();
                _contribuyente.Nacionalidad = txt_nacionalidad.Text.Trim();
                _contribuyente.Direccion = txt_direccion.Text.Trim();
                _contribuyente.Comuna = txt_comuna.Text.Trim();

                _contribuyenteService.Actualizar(_contribuyente);

                MessageBox.Show("Contribuyente actualizado correctamente.", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}