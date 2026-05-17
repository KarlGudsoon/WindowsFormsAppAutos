using MySql.Data.MySqlClient;
using PermisosVehiculos.Data;
using PermisosVehiculos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermisosVehiculos.Services
{
    public class VehiculoService
    {
        public int Insertar(Vehiculo vehiculo)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"INSERT INTO vehiculos 
                                (patente, marca, modelo, color, anio, id_contribuyente)
                             VALUES 
                                (@patente, @marca, @modelo, @color, @anio, @idContribuyente);";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idContribuyente", vehiculo.IdContribuyente);
                    cmd.Parameters.AddWithValue("@patente", vehiculo.Patente);
                    cmd.Parameters.AddWithValue("@marca", vehiculo.Marca);
                    cmd.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                    cmd.Parameters.AddWithValue("@color", vehiculo.Color);
                    cmd.Parameters.AddWithValue("@anio", vehiculo.Anio);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al insertar vehículo: " + ex.Message);
                }
            }
        }
        public void Actualizar(Vehiculo vehiculo)
        {
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"UPDATE vehiculos 
                             SET patente = @patente,
                                 marca   = @marca,
                                 modelo  = @modelo,
                                 color   = @color,
                                 anio    = @anio
                             WHERE id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@patente", vehiculo.Patente);
                    cmd.Parameters.AddWithValue("@marca", vehiculo.Marca);
                    cmd.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                    cmd.Parameters.AddWithValue("@color", vehiculo.Color);
                    cmd.Parameters.AddWithValue("@anio", vehiculo.Anio);
                    cmd.Parameters.AddWithValue("@id", vehiculo.Id);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al actualizar vehículo: " + ex.Message);
                }
            }
        }
        public List<Vehiculo> ObtenerTodos()
        {
            List<Vehiculo> lista = new List<Vehiculo>();

            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT 
                                v.id, 
                                v.patente, 
                                v.marca, 
                                v.modelo, 
                                v.color, 
                                v.anio,
                                v.id_contribuyente,
                                c.rut AS contribuyente_rut
                             FROM vehiculos v
                             INNER JOIN contribuyentes c ON v.id_contribuyente = c.id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Vehiculo v = new Vehiculo
                        {
                            Id = reader.GetInt32("id"),
                            Patente = reader.GetString("patente"),
                            Marca = reader.GetString("marca"),
                            Modelo = reader.GetString("modelo"),
                            Color = reader.GetString("color"),
                            Anio = reader.GetInt32("anio"),
                            IdContribuyente = reader.GetInt32("id_contribuyente"),
                            RutContribuyente = reader.GetString("contribuyente_rut")
                        };

                        lista.Add(v);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al obtener vehículos: " + ex.Message);
                }
            }

            return lista;
        }
    }
}