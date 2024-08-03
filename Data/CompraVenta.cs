using Microsoft.Data.SqlClient;

namespace Parcial1.Data
{
    public class CompraVenta
    {
        private readonly string connectionString = "Server=svr-sql-ctezo.southcentralus.cloudapp.azure.com;Database=db_banco;User Id=usr_admin;Password=usrGuastaUMG!ng;TrustServerCertificate=True;";

        public string? IdCompraVenta { get; set; }
        public string? Placa { get; set; }
        public string? CUIComprador { get; set; }
        public string? CUIVendedor { get; set; }
        public DateTime? FechaTransaccion { get; set; }
        public decimal? PrecioVenta { get; set; }

        public string GuardarCompraVenta(CompraVenta compraVenta)
        {
            string qry = @"
                INSERT INTO CompraVenta (IdCompraVenta, Placa, CUIComprador, CUIVendedor, FechaTransaccion, PrecioVenta)
                VALUES (@IdCompraVenta, @Placa, @CUIComprador, @CUIVendedor, @FechaTransaccion, @PrecioVenta)";

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@IdCompraVenta", compraVenta.IdCompraVenta);
                cmd.Parameters.AddWithValue("@Placa", compraVenta.Placa);
                cmd.Parameters.AddWithValue("@CUIComprador", compraVenta.CUIComprador);
                cmd.Parameters.AddWithValue("@CUIVendedor", compraVenta.CUIVendedor);
                cmd.Parameters.AddWithValue("@FechaTransaccion", compraVenta.FechaTransaccion);
                cmd.Parameters.AddWithValue("@PrecioVenta", compraVenta.PrecioVenta);
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
                        IdCompraVenta = reader["IdCompraVenta"].ToString(),
                        Placa = reader["Placa"].ToString(),
                        CUIComprador = reader["CUIComprador"].ToString(),
                        CUIVendedor = reader["CUIVendedor"].ToString(),
                        FechaTransaccion = reader.GetDateTime(reader.GetOrdinal("FechaTransaccion")),
                        PrecioVenta = reader.GetDecimal(reader.GetOrdinal("PrecioVenta"))
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

        public CompraVenta? ObtenerCompraVenta(string IdCompraVenta)
        {
            string query = "SELECT * FROM CompraVenta WHERE IdCompraVenta = @IdCompraVenta";
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@IdCompraVenta", IdCompraVenta);
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    CompraVenta compraVenta = new()
                    {
                        IdCompraVenta = reader["IdCompraVenta"].ToString(),
                        Placa = reader["Placa"].ToString(),
                        CUIComprador = reader["CUIComprador"].ToString(),
                        CUIVendedor = reader["CUIVendedor"].ToString(),
                        FechaTransaccion = reader.GetDateTime(reader.GetOrdinal("FechaTransaccion")),
                        PrecioVenta = reader.GetDecimal(reader.GetOrdinal("PrecioVenta"))
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
                SET Placa = @Placa, CUIComprador = @CUIComprador, CUIVendedor = @CUIVendedor, FechaTransaccion = @FechaTransaccion, PrecioVenta = @PrecioVenta
                WHERE IdCompraVenta = @IdCompraVenta";

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@IdCompraVenta", compraVenta.IdCompraVenta);
                cmd.Parameters.AddWithValue("@Placa", compraVenta.Placa);
                cmd.Parameters.AddWithValue("@CUIComprador", compraVenta.CUIComprador);
                cmd.Parameters.AddWithValue("@CUIVendedor", compraVenta.CUIVendedor);
                cmd.Parameters.AddWithValue("@FechaTransaccion", compraVenta.FechaTransaccion);
                cmd.Parameters.AddWithValue("@PrecioVenta", compraVenta.PrecioVenta);
                cmd.ExecuteNonQuery();
                return "CompraVenta actualizada exitosamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarCompraVenta(string IdCompraVenta)
        {
            string qry = "DELETE FROM CompraVenta WHERE IdCompraVenta = @IdCompraVenta";
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@IdCompraVenta", IdCompraVenta);
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
