using Microsoft.Data.SqlClient;

namespace Webappi1.Reposo
{
    public class Auto
    {
        private string connectionString = "Server=svr-sql-ctezo.southcentralus.cloudapp.azure.com;Database=db_banco;User Id=usr_admin;Password=usrGuastaUMG!ng;TrustServerCertificate=True;";

        public string placa { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public int year { get; set; }
        public string color { get; set; }
        public string tipo { get; set; }

        public string estado { get; set; }

        public string GuardarAuto(Auto Auto)
        {
            string qry = @"
    INSERT INTO vehiculos (placa, marca, modelo, anio, color, tipo, estado)
    VALUES (@placa, @marca, @modelo, @anio, @color, @tipo, @estado)";

            try
            {
                //importante, cargar conector SQL
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //abrir conexion
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(qry, conn))
                    {
                        cmd.Parameters.AddWithValue("@placa", Auto.placa);
                        cmd.Parameters.AddWithValue("@marca", Auto.marca);
                        cmd.Parameters.AddWithValue("@modelo", Auto.modelo);
                        cmd.Parameters.AddWithValue("@anio", Auto.year);
                        cmd.Parameters.AddWithValue("@color", Auto.color);
                        cmd.Parameters.AddWithValue("@tipo", Auto.tipo);
                        cmd.Parameters.AddWithValue("@estado", Auto.estado);

                        // Execute the command
                        cmd.ExecuteNonQuery();
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return (ex.Message);
            }
        }
        public List<Auto> ListarVehiculos()
        {
            List<Auto> lista = new List<Auto>();
            string query = "SELECT * FROM vehiculos";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Auto Auto = new Auto();
                                Auto.placa = reader["placa"].ToString();
                                Auto.marca = reader["marca"].ToString();
                                Auto.modelo = reader["modelo"].ToString();
                                Auto.year = Convert.ToInt32(reader["anio"]);
                                Auto.color = reader["color"].ToString();
                                Auto.tipo = reader["tipo"].ToString();
                                Auto.estado = reader["estado"].ToString();
                                lista.Add(Auto);
                            }
                        }
                    }
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