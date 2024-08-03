using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Parcial1.Data
{
    public class Vehiculo
    {
        private readonly string connectionString = "Server=svr-sql-ctezo.southcentralus.cloudapp.azure.com;Database=db_banco;User Id=usr_admin;Password=usrGuastaUMG!ng;TrustServerCertificate=True;";
        public string? Placa { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int Year { get; set; }
        public string? Estado { get; set; }

        public string GuardarVehiculo(Vehiculo vehiculo)
        {
            string qry = @"
    INSERT INTO vehiculos (placa, marca, modelo, anio, estado)
    VALUES (@placa, @marca, @modelo, @anio, @estado)";

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@placa", vehiculo.Placa);
                cmd.Parameters.AddWithValue("@marca", vehiculo.Marca);
                cmd.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                cmd.Parameters.AddWithValue("@anio", vehiculo.Year);
                cmd.Parameters.AddWithValue("@estado", vehiculo.Estado);
                cmd.ExecuteNonQuery();
                return "Vehículo guardado exitosamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Vehiculo> ListarVehiculos()
        {
            List<Vehiculo> lista = new();
            string query = "SELECT * FROM vehiculos";
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(query, conn);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Vehiculo vehiculo = new()
                    {
                        Placa = reader["placa"].ToString() ?? "",
                        Marca = reader["marca"].ToString() ?? "",
                        Modelo = reader["modelo"].ToString() ?? "",
                        Year = Convert.ToInt32(reader["anio"]),
                        Estado = reader["estado"].ToString() ?? ""
                    };
                    lista.Add(vehiculo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lista;
        }

        public Vehiculo? ObtenerVehiculo(string placa)
        {
            string query = "SELECT * FROM vehiculos WHERE placa = @placa";
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@placa", placa);
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Vehiculo vehiculo = new()
                    {
                        Placa = reader["placa"].ToString() ?? "",
                        Marca = reader["marca"].ToString() ?? "",
                        Modelo = reader["modelo"].ToString() ?? "",
                        Year = Convert.ToInt32(reader["anio"]),
                        Estado = reader["estado"].ToString() ?? ""
                    };
                    return vehiculo;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public string ActualizarVehiculo(Vehiculo vehiculo)
        {
            string qry = @"
    UPDATE vehiculos 
    SET marca = @marca, modelo = @modelo, anio = @anio, estado = @estado
    WHERE placa = @placa";

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@placa", vehiculo.Placa);
                cmd.Parameters.AddWithValue("@marca", vehiculo.Marca);
                cmd.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                cmd.Parameters.AddWithValue("@anio", vehiculo.Year);
                cmd.Parameters.AddWithValue("@estado", vehiculo.Estado);
                cmd.ExecuteNonQuery();
                return "Vehículo actualizado exitosamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarVehiculo(string placa)
        {
            string qry = "DELETE FROM vehiculos WHERE placa = @placa";
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@placa", placa);
                cmd.ExecuteNonQuery();
                return "Vehículo eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
