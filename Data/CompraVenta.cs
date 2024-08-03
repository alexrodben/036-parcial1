using Microsoft.Data.SqlClient;

namespace Parcial1.Data
{
    public class CompraVenta
    {
        private readonly string connectionString = "Server=svr-sql-ctezo.southcentralus.cloudapp.azure.com;Database=db_banco;User Id=usr_admin;Password=usrGuastaUMG!ng;TrustServerCertificate=True;";

        public string? IdCompraVenta { get; set; }
        public string? Placa { get; set; }
        public string? CuiComprador { get; set; }
        public string? CuiVendedor { get; set; }
        public DateTime? FechaTransaccion { get; set; }
        public decimal? PerecioVenta { get; set; }

        public string GuardarCompraVenta(CompraVenta compraVenta)
        {
            string qry = @"
                INSERT INTO CompraVenta (idCompraVenta, placa, cuiComprador, cuiVendedor, fechaTransaccion, perecioVenta)
                VALUES (@idCompraVenta, @placa, @cuiComprador, @cuiVendedor, @fechaTransaccion, @perecioVenta)";

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@idCompraVenta", compraVenta.IdCompraVenta);
                cmd.Parameters.AddWithValue("@placa", compraVenta.Placa);
                cmd.Parameters.AddWithValue("@cuiComprador", compraVenta.CuiComprador);
                cmd.Parameters.AddWithValue("@cuiVendedor", compraVenta.CuiVendedor);
                cmd.Parameters.AddWithValue("@fechaTransaccion", compraVenta.FechaTransaccion);
                cmd.Parameters.AddWithValue("@perecioVenta", compraVenta.PerecioVenta);
                cmd.ExecuteNonQuery();
                return "CompraVenta guardada exitosamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<CompraVenta> ListarcompraVenta()
        {
            List<CompraVenta> lista = [];
            string query = "SELECT * FROM CompraVenta";
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(query, conn);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CompraVenta compraVenta = new()
                    {
                        IdCompraVenta = reader["idCompraVenta"].ToString(),
                        Placa = reader["placa"].ToString(),
                        CuiComprador = reader["cuiComprador"].ToString(),
                        CuiVendedor = reader["cuiVendedor"].ToString(),
                        FechaTransaccion = reader.GetDateTime(reader.GetOrdinal("fechaTransaccion")),
                        PerecioVenta = reader.GetDecimal(reader.GetOrdinal("perecioVenta"))
                    };
                    lista.Add(compraVenta);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lista;
        }

        public CompraVenta? ObtenerCompraVenta(string idCompraVenta)
        {
            string query = "SELECT * FROM CompraVenta WHERE idCompraVenta = @idCompraVenta";
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@idCompraVenta", idCompraVenta);
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    CompraVenta compraVenta = new()
                    {
                        IdCompraVenta = reader["idCompraVenta"].ToString(),
                        Placa = reader["placa"].ToString(),
                        CuiComprador = reader["cuiComprador"].ToString(),
                        CuiVendedor = reader["cuiVendedor"].ToString(),
                        FechaTransaccion = reader.GetDateTime(reader.GetOrdinal("fechaTransaccion")),
                        PerecioVenta = reader.GetDecimal(reader.GetOrdinal("perecioVenta"))
                    };
                    return compraVenta;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public string ActualizarCompraVenta(CompraVenta compraVenta)
        {
            string qry = @"
                UPDATE CompraVenta 
                SET placa = @placa, cuiComprador = @cuiComprador, cuiVendedor = @cuiVendedor, fechaTransaccion = @fechaTransaccion, perecioVenta = @perecioVenta
                WHERE idCompraVenta = @idCompraVenta";

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@idCompraVenta", compraVenta.IdCompraVenta);
                cmd.Parameters.AddWithValue("@placa", compraVenta.Placa);
                cmd.Parameters.AddWithValue("@cuiComprador", compraVenta.CuiComprador);
                cmd.Parameters.AddWithValue("@cuiVendedor", compraVenta.CuiVendedor);
                cmd.Parameters.AddWithValue("@fechaTransaccion", compraVenta.FechaTransaccion);
                cmd.Parameters.AddWithValue("@perecioVenta", compraVenta.PerecioVenta);
                cmd.ExecuteNonQuery();
                return "CompraVenta actualizada exitosamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarCompraVenta(string idCompraVenta)
        {
            string qry = "DELETE FROM CompraVenta WHERE idCompraVenta = @idCompraVenta";
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@idCompraVenta", idCompraVenta);
                cmd.ExecuteNonQuery();
                return "CompraVenta eliminada exitosamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
