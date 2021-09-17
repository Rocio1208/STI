using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace DSGCBD
{
    public class BD_SAGETHDataBase : IDisposable
    {
        //Se inicializa en el constructor.
        private string _connectionString;

        //Miembro de clase para liberar la conexión en el destructor
        //y permitir el uso de objetos SqldataReader.
        private SqlConnection _connection;


        public BD_SAGETHDataBase()
        {
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["BD_SAGETHConnectionString"].ConnectionString;
            }
            catch (Exception e)
            {
                throw new Exception($"La cadena de conexión del repositorio de datos no está configurado correctamente: {e.Message}");
            }
        }

        public object Nullable(object valor)
        {
            if (valor == null)
                return DBNull.Value;
            else
                return valor;
        }


        public SqlDataReader ExecuteReader(SqlCommand commandToExecute)

        {
            try
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
                commandToExecute.Connection = _connection;

                return commandToExecute.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }


        public DataSet ExecutDataSet(SqlCommand commandToExecute)
        {
            try
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
                commandToExecute.Connection = _connection;

                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(commandToExecute);
                adapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message.ToString());
            }

        }


        public void ExecuteNonQuery(ref SqlCommand commandToExecute)
        {
            try
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
                commandToExecute.Connection = _connection;

                commandToExecute.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }

        public SqlBulkCopy GetSqlBulkCopy(string destinationTableName)
        {
            try
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();

                return new SqlBulkCopy(_connection) { DestinationTableName = destinationTableName };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }


        public DataTable GetSchema(string collectionName, string[] restrictionValues)
        {
            try
            {

                if (_connection == null)
                    _connection = new SqlConnection(_connectionString);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                return _connection.GetSchema(collectionName, restrictionValues);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }

        }


        #region IDisposable Support
        private bool _disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_connection != null && _connection.State != ConnectionState.Closed)
                        _connection.Close();
                }

                _disposedValue = true;
            }
        }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            Dispose(true);

        }
        #endregion
    }
}

