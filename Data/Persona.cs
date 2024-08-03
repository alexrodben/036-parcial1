using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Parcial1.Data
{
    public class Persona
    {
        private readonly string connectionString = "Server=svr-sql-ctezo.southcentralus.cloudapp.azure.com;Database=db_banco;User Id=usr_admin;Password=usrGuastaUMG!ng;TrustServerCertificate=True;";

        public string? CUI { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }

        public string GuardarPersona(Persona persona)
        {
            string qry = @"
                INSERT INTO personas (CUI, nombre, apellido, telefono, direccion)
                VALUES (@CUI, @nombre, @apellido, @telefono, @direccion)";

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@CUI", persona.CUI);
                cmd.Parameters.AddWithValue("@nombre", persona.Nombre);
                cmd.Parameters.AddWithValue("@apellido", persona.Apellido);
                cmd.Parameters.AddWithValue("@telefono", persona.Telefono);
                cmd.Parameters.AddWithValue("@direccion", persona.Direccion);

                cmd.ExecuteNonQuery();
                return "Persona guardada exitosamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Persona> ListarPersonas()
        {
            List<Persona> lista = new();
            string query = "SELECT * FROM personas";
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(query, conn);
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Persona persona = new()
                    {
                        CUI = reader["CUI"].ToString(),
                        Nombre = reader["nombre"].ToString(),
                        Apellido = reader["apellido"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Direccion = reader["direccion"].ToString()
                    };
                    lista.Add(persona);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return lista;
        }

        public Persona? ObtenerPersona(string cui)
        {
            string query = "SELECT * FROM personas WHERE CUI = @CUI";
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(query, conn);
                cmd.Parameters.AddWithValue("@CUI", cui);
                using SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Persona persona = new()
                    {
                        CUI = reader["CUI"].ToString(),
                        Nombre = reader["nombre"].ToString(),
                        Apellido = reader["apellido"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Direccion = reader["direccion"].ToString()
                    };
                    return persona;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public string ActualizarPersona(Persona persona)
        {
            string qry = @"
                UPDATE personas 
                SET nombre = @nombre, apellido = @apellido, telefono = @telefono, direccion = @direccion
                WHERE CUI = @CUI";

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@CUI", persona.CUI);
                cmd.Parameters.AddWithValue("@nombre", persona.Nombre);
                cmd.Parameters.AddWithValue("@apellido", persona.Apellido);
                cmd.Parameters.AddWithValue("@telefono", persona.Telefono);
                cmd.Parameters.AddWithValue("@direccion", persona.Direccion);
                cmd.ExecuteNonQuery();
                return "Persona actualizada exitosamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string EliminarPersona(string cui)
        {
            string qry = "DELETE FROM personas WHERE CUI = @CUI";
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();
                using SqlCommand cmd = new(qry, conn);
                cmd.Parameters.AddWithValue("@CUI", cui);
                cmd.ExecuteNonQuery();
                return "Persona eliminada exitosamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
