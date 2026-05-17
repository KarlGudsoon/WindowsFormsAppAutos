using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PermisosVehiculos.Models;
using PermisosVehiculos.Data;

namespace PermisosVehiculos.Services
{
    public class AuthService
    {
        public bool Login(Usuario usuario)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT COUNT(*)
                                     FROM usuarios
                                     WHERE usuario = @usuario
                                     AND contrasena = @contrasena";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@usuario", usuario.NombreUsuario);
                    cmd.Parameters.AddWithValue("@contrasena", usuario.Contrasena);

                    int resultado = Convert.ToInt32(cmd.ExecuteScalar());
                    return resultado > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al iniciar sesión: " + ex.Message);
                }
            }
        }
    }
}