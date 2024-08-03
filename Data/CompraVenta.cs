using Microsoft.Data.SqlClient;

namespace Webappi1.Reposo
{
    public class CompraVenta
    {
        private string connectionString = "Server=svr-sql-ctezo.southcentralus.cloudapp.azure.com;Database=db_banco;User Id=usr_admin;Password=usrGuastaUMG!ng;TrustServerCertificate=True;";

        public string idCompraVenta { get; set; }
        public string placa { get; set; }
        public string cuiComprador { get; set; }   
        public string cuiVendedor { get; set; }
        public string fechaTransaccion { get; set; }
        public string perecioVenta { get; set; }

        public string GuardarCompraVenta(CompraVenta CompraVenta)
        {
            string qry = @"
                INSERT INTO compraVentra (idCompraVenta, placa, cuiComprador, cuiVendedor, fechaTransaccion, perecioVenta)
                VALUES (@idCompraVenta, @placa, @cuiComprador, @cuiVendedor, @fechaTransaccion, @perecioVenta)";

            try
            {
                //importante, cargar conector SQL
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    //abrir conexion
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(qry, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCompraVenta", CompraVenta.idCompraVenta);
                        cmd.Parameters.AddWithValue("@placa", CompraVenta.placa);
                        cmd.Parameters.AddWithValue("@cuiComprador", CompraVenta.cuiComprador);
                        cmd.Parameters.AddWithValue("@anio", CompraVenta.cuiVendedor);
                        cmd.Parameters.AddWithValue("@fechaTransaccion", CompraVenta.fechaTransaccion);

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
        public List<CompraVenta> ListarCompraVentas()
        {
            List<CompraVenta> lista = new List<CompraVenta>();
            string query = "SELECT * FROM compraVentas";
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
                                CompraVenta compraVenta = new CompraVenta();
                                compraVenta.idCompraVenta = reader["idCompraVenta"].ToString();
                                compraVenta.placa = reader["placa"].ToString();
                                compraVenta.cuiComprador = reader["cuiComprador"].ToString();
                                compraVenta.cuiVendedor = reader["anio"].ToString();
                                compraVenta.fechaTransaccion = reader["fechaTransaccion"].ToString();
                                compraVenta.perecioVenta = reader["perecioVenta"].ToString();
                                lista.Add(compraVenta);
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