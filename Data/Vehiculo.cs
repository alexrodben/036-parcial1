using Microsoft.Data.SqlClient;

namespace Parcial1.Data
{
    public class Vehiculo
    {
        private string connectionString = "Server=svr-sql-ctezo.southcentralus.cloudapp.azure.com;Database=db_banco;User Id=usr_admin;Password=usrGuastaUMG!ng;TrustServerCertificate=True;";
        public string? Placa { get; set; }
        public string? marca { get; set; }
        public string? modelo { get; set; }
        public int year { get; set; }
        public string? estado { get; set; }

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
                cmd.Parameters.AddWithValue("@marca", Vehiculo.marca);
                cmd.Parameters.AddWithValue("@modelo", Vehiculo.modelo);
                cmd.Parameters.AddWithValue("@anio", Vehiculo.year);
                cmd.Parameters.AddWithValue("@estado", Vehiculo.estado);
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
            List<Vehiculo> lista = new List<Vehiculo>();
            string query = "SELECT * FROM vehiculos";
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                using SqlCommand cmd = new SqlCommand(query, conn);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Vehiculo Vehiculo = new()
                    {
                        Placa = reader["placa"].ToString() ?? "",
                        marca = reader["marca"].ToString() ?? "",
                        modelo = reader["modelo"].ToString() ?? "",
                        year = Convert.ToInt32(reader["anio"]),
                        estado = reader["estado"].ToString() ?? ""
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