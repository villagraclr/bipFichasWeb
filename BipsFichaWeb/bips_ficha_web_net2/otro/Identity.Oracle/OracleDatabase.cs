using Oracle.DataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading;

namespace MDS.Identity
{
    /// <summary>
    /// Clase que encapsula las conexiones a la BD oracle y sus operaciones CRUD
    /// </summary>
    public class OracleDatabase : IDisposable
    {
        private OracleConnection conexion;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public OracleDatabase() : this("DB_MDS") { }

        /// <summary>
        /// Constructor que recibe el nomnbre de la cadena de conexion
        /// </summary>
        /// <param name="nombreConexion"></param>
        public OracleDatabase(string nombreConexion)
        {
            var cadenaConexion = ConfigurationManager.ConnectionStrings[nombreConexion].ConnectionString;
            conexion = new OracleConnection(cadenaConexion);
        }

        /// <summary>
        /// Ejecuta las sentencias non-query de oracle
        /// </summary>
        /// <param name="commandText">Query a ejecutar</param>
        /// <param name="parameters">Parámetros opcionales</param>
        /// <returns>Total de registros afectados por la sentencia</returns>
        public int Execute(string commandText, IEnumerable parameters)
        {
            int result;

            if (String.IsNullOrEmpty(commandText))
            {
                throw new ArgumentException("Command text cannot be null or empty.");
            }

            try
            {
                ensureConnectionOpen();
                var command = createCommand(commandText, parameters);
                result = command.ExecuteNonQuery();
            }
            finally
            {
                conexion.Close();
            }

            return result;
        }

        /// <summary>
        /// Ejecuta una consulta oracle que retorna un valor escalar como resultado
        /// </summary>
        /// <param name="commandText">Query a ejecutar</param>
        /// <param name="parameters">Parámetros opcionales</param>
        /// <returns></returns>
        public object QueryValue(string commandText, IEnumerable parameters)
        {
            object result;

            if (String.IsNullOrEmpty(commandText))
            {
                throw new ArgumentException("Command text cannot be null or empty.");
            }

            try
            {
                ensureConnectionOpen();
                var command = createCommand(commandText, parameters);
                result = command.ExecuteScalar();
            }
            finally
            {
                ensureConnectionClosed();
            }

            return result;
        }

        /// <summary>
        /// Ejecuta una consulta sql que retorne una lista de filas como resultado
        /// </summary>
        /// <param name="commandText">Consulta a ejecutar</param>
        /// <param name="parameters">Parámetros de la consulta</param>
        /// <returns>Lista de tipo Dictionary, valores agrupados como nombreColumna y valor</returns>
        public List<Dictionary<string, string>> Query(string commandText, IEnumerable parameters)
        {
            List<Dictionary<string, string>> rows;
            if (String.IsNullOrEmpty(commandText))
            {
                throw new ArgumentException("Command text cannot be null or empty.");
            }

            try
            {
                ensureConnectionOpen();
                var command = createCommand(commandText, parameters);
                using (var reader = command.ExecuteReader())
                {
                    rows = new List<Dictionary<string, string>>();
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, string>();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var columnName = reader.GetName(i);
                            var columnValue = reader.IsDBNull(i) ? null : reader.GetValue(i).ToString();
                            row.Add(columnName, columnValue);
                        }
                        rows.Add(row);
                    }
                }
            }
            finally
            {
                ensureConnectionClosed();
            }

            return rows;
        }

        /// <summary>
        /// Abre conexión, si no está abierta
        /// </summary>
        private void ensureConnectionOpen()
        {
            var retries = 3;
            if (conexion.State == ConnectionState.Open)
            {
                return;
            }
            while (retries >= 0 && conexion.State != ConnectionState.Open)
            {
                conexion.Open();
                retries--;
                Thread.Sleep(30);
            }
        }

        /// <summary>
        /// Cierra conexión, si está abierta
        /// </summary>
        private void ensureConnectionClosed()
        {
            if (conexion.State == ConnectionState.Open)
            {
                conexion.Close();
            }
        }

        /// <summary>
        /// Crea un OracleCommand con los parámetros enviados
        /// </summary>
        /// <param name="commandText">Consulta a ejecutar</param>
        /// <param name="parameters">Parámetros de la consulta</param>
        /// <returns></returns>
        private OracleCommand createCommand(string commandText, IEnumerable parameters)
        {
            var command = conexion.CreateCommand();
            command.BindByName = true;
            command.CommandText = commandText;
            addParameters(command, parameters);

            return command;
        }

        /// <summary>
        /// Agrega parámetros al oracle command
        /// </summary>
        /// <param name="command">Consulta a ejecutar</param>
        /// <param name="parameters">Parámetros de la consulta</param>
        private static void addParameters(OracleCommand command, IEnumerable parameters)
        {
            if (parameters == null) return;

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
        }

        /// <summary>
        /// Método helper para las consultas que retornan un valor string
        /// </summary>
        /// <param name="commandText">Consulta a ejecutar</param>
        /// <param name="parameters">Parámetros de la consulta</param>
        /// <returns>Valor string</returns>
        public string GetStrValue(string commandText, IEnumerable parameters)
        {
            var value = QueryValue(commandText, parameters) as string;
            return value;
        }

        public void Dispose()
        {
            if (conexion == null) return;

            conexion.Dispose();
            conexion = null;
        }
    }
}
