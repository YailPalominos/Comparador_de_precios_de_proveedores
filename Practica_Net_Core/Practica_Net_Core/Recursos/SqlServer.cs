using Microsoft.Data.SqlClient;
using System.Data;

namespace Practica_Net_Core.Recursos
{
    public class SqlServer
    {
        private readonly IConfiguration _configuration;

        public SqlServer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<T> EjecutarLector<T>(List<SqlParameter> parametros, string procedimiento, Func<IDataRecord, T> convertir)
        {
            string connectionString = _configuration?.GetConnectionString("Conexion") ?? "";
            List<T> resultados = new List<T>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(procedimiento, connection))
                    {
                        // Establecer el tipo de comando como procedimiento almacenado
                        command.CommandType = CommandType.StoredProcedure;
                        // Agregar parámetros al comando
                        foreach (var parametro in parametros)
                        {
                            command.Parameters.Add(parametro);
                        }
                        // Ejecutar el comando y leer los datos con SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Iterar sobre cada registro en el conjunto de resultados
                            while (reader.Read())
                            {
                                // Convertir el registro a un objeto del tipo T utilizando la función de conversión proporcionada
                                T resultado = convertir(reader);
                                resultados.Add(resultado);
                            }
                        }
                    }
                }
                return resultados;
            }
            catch
            {
                throw;
            }
        }

        public T EjecutarUnico<T>(List<SqlParameter> parametros, string procedimiento)
        {
            string connectionString = _configuration?.GetConnectionString("Conexion") ?? "";
            object? resultado = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(procedimiento, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        foreach (var parametro in parametros)
                        {
                            command.Parameters.Add(parametro);
                        }
                        resultado = command.ExecuteScalar();
                    }
                }
                // Convertir el resultado al tipo T
                return (T)Convert.ChangeType(resultado, typeof(T));
            }
            catch
            {
                throw;
            }
        }
    }
}