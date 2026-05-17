using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PermisosVehiculos.Data;
using PermisosVehiculos.Models;

namespace PermisosVehiculos.Services
{
    public class ContribuyenteService
    {
        public int Insertar(Contribuyente contribuyente)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"INSERT INTO contribuyentes 
                                (rut, nombre, apellido, nacionalidad, direccion, comuna)
                             VALUES 
                                (@rut, @nombre, @apellido, @nacionalidad, @direccion, @comuna);";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@rut", contribuyente.Rut);
                    cmd.Parameters.AddWithValue("@nombre", contribuyente.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", contribuyente.Apellido);
                    cmd.Parameters.AddWithValue("@nacionalidad", contribuyente.Nacionalidad);
                    cmd.Parameters.AddWithValue("@direccion", contribuyente.Direccion);
                    cmd.Parameters.AddWithValue("@comuna", contribuyente.Comuna);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al insertar contribuyente: " + ex.Message);
                }
            }
        }
        public void Actualizar(Contribuyente contribuyente)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"UPDATE contribuyentes 
                             SET rut = @rut,
                                 nombre = @nombre,
                                 apellido = @apellido,
                                 nacionalidad = @nacionalidad,
                                 direccion = @direccion,
                                 comuna = @comuna
                             WHERE id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@rut", contribuyente.Rut);
                    cmd.Parameters.AddWithValue("@nombre", contribuyente.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", contribuyente.Apellido);
                    cmd.Parameters.AddWithValue("@nacionalidad", contribuyente.Nacionalidad);
                    cmd.Parameters.AddWithValue("@direccion", contribuyente.Direccion);
                    cmd.Parameters.AddWithValue("@comuna", contribuyente.Comuna);
                    cmd.Parameters.AddWithValue("@id", contribuyente.Id);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar contribuyente: " + ex.Message);
                }
            }
        }
        public List<Contribuyente> ObtenerTodos()
        {
            List<Contribuyente> lista = new List<Contribuyente>();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT id, rut, nombre, apellido, 
                                     nacionalidad, direccion, comuna 
                                     FROM contribuyentes";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Contribuyente c = new Contribuyente
                        {
                            Id = reader.GetInt32("id"),
                            Rut = reader.GetString("rut"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            Nacionalidad = reader.GetString("nacionalidad"),
                            Direccion = reader.GetString("direccion"),
                            Comuna = reader.GetString("comuna")
                        };

                        lista.Add(c);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener contribuyentes: " + ex.Message);
                }
            }

            return lista;
        }
    }
}