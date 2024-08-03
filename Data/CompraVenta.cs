using Microsoft.Data.SqlClient;

namespace Parcial1.Data
{
    public class CompraVenta
    {
        private string connectionString = "Server=svr-sql-ctezo.southcentralus.cloudapp.azure.com;Database=db_banco;User Id=usr_admin;Password=usrGuastaUMG!ng;TrustServerCertificate=True;";

        public string IdCompraVenta { get; set; }
        public string Placa { get; set; }
        public string CuiComprador { get; set; }
        public string CuiVendedor { get; set; }
        public string FechaTransaccion { get; set; }
        public string PerecioVenta { get; set; }

        public string GuardarCompraVenta(CompraVenta CompraVenta)
        {
            string qry = @"
                INSERT INTO compraVentra (idCompraVenta, placa, cuiComprador, cuiVendedor, fechaTransaccion, perecioVenta)
                VALUES (@idCompraVenta, @placa, @cuiComprador, @cuiVendedor, @fechaTransaccion, @perecioVenta)";

            try
            {
                //importante, cargar conector SQL
                using SqlConnection conn = new(connectionString);
                //abrir conexion
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@idCompraVenta", CompraVenta.IdCompraVenta);
                cmd.Parameters.AddWithValue("@placa", CompraVenta.Placa);
                cmd.Parameters.AddWithValue("@cuiComprador", CompraVenta.CuiComprador);
                cmd.Parameters.AddWithValue("@anio", CompraVenta.CuiVendedor);
                cmd.Parameters.AddWithValue("@fechaTransaccion", CompraVenta.FechaTransaccion);
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
        public List<CompraVenta> ListarCompraVentas()
        {
            List<CompraVenta> lista = new List<CompraVenta>();
            string query = "SELECT * FROM compraVentas";
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                using SqlCommand cmd = new SqlCommand(query, conn);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CompraVenta compraVenta = new CompraVenta();
                    compraVenta.IdCompraVenta = reader["idCompraVenta"].ToString();
                    compraVenta.Placa = reader["placa"].ToString();
                    compraVenta.CuiComprador = reader["cuiComprador"].ToString();
                    compraVenta.CuiVendedor = reader["anio"].ToString();
                    compraVenta.FechaTransaccion = reader["fechaTransaccion"].ToString();
                    compraVenta.PerecioVenta = reader["perecioVenta"].ToString();
                    lista.Add(compraVenta);
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