using Microsoft.Data.SqlClient;

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

        public string GuardarVehiculo(Vehiculo Vehiculo)
        {
            string qry = @"
    INSERT INTO vehiculos (placa, marca, modelo, anio, color, tipo, estado)
    VALUES (@placa, @marca, @modelo, @anio, @color, @tipo, @estado)";

            try
            {
                //importante, cargar conector SQL
                using SqlConnection conn = new(connectionString);
                //abrir conexion
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@placa", Vehiculo.Placa);
                cmd.Parameters.AddWithValue("@marca", Vehiculo.Marca);
                cmd.Parameters.AddWithValue("@modelo", Vehiculo.Modelo);
                cmd.Parameters.AddWithValue("@anio", Vehiculo.Year);
                cmd.Parameters.AddWithValue("@estado", Vehiculo.Estado);
                // Execute the command
                cmd.ExecuteNonQuery();
                return "";
            }
            catch (Exception ex)
            {
                // Handle exception
                return (ex.Message);
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
                    Vehiculo Vehiculo = new()
                    {
                        Placa = reader["placa"].ToString() ?? "",
                        Marca = reader["marca"].ToString() ?? "",
                        Modelo = reader["modelo"].ToString() ?? "",
                        Year = Convert.ToInt32(reader["anio"]),
                        Estado = reader["estado"].ToString() ?? ""
                    };
                    lista.Add(Vehiculo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lista;
        }

    }
}