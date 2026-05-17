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
    public partial class FrmEditarVehiculo : Form
    {
        private readonly VehiculoService _vehiculoService = new VehiculoService();
        private readonly Vehiculo _vehiculo;

        // Recibe el vehículo a editar
        public FrmEditarVehiculo(Vehiculo vehiculo)
        {
            InitializeComponent();
            _vehiculo = vehiculo;

            txt_patente.Text = _vehiculo.Patente;
            txt_marca.Text = _vehiculo.Marca;
            txt_modelo.Text = _vehiculo.Modelo;
            txt_color.Text = _vehiculo.Color;
            txt_año.Text = _vehiculo.Anio.ToString();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_patente.Text) ||
                string.IsNullOrWhiteSpace(txt_marca.Text) ||
                string.IsNullOrWhiteSpace(txt_modelo.Text) ||
                string.IsNullOrWhiteSpace(txt_color.Text) ||
                string.IsNullOrWhiteSpace(txt_año.Text))
            {
                MessageBox.Show("Completa todos los campos.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txt_año.Text, out int anio))
            {
                MessageBox.Show("El año debe ser un número válido.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Actualizar los datos del vehículo
                _vehiculo.Patente = txt_patente.Text.Trim().ToUpper();
                _vehiculo.Marca = txt_marca.Text.Trim();
                _vehiculo.Modelo = txt_modelo.Text.Trim();
                _vehiculo.Color = txt_color.Text.Trim();
                _vehiculo.Anio = anio;

                _vehiculoService.Actualizar(_vehiculo);

                MessageBox.Show("Vehículo actualizado correctamente.", "Éxito",
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