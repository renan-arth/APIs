using System.Data;
using System.Data.Common;

namespace Platform.Repository.Base
{
    public class BaseDAO
    {
        private readonly string _connectionString;
        private readonly string _providerName;

        public BaseDAO()
        {
            _connectionString = ConfigMirror.ConnectionString;
            _providerName = ConfigMirror.ProviderName ?? "Microsoft.Data.SqlClient";
        }

        public IDbConnection GetConnection()
        {
            var factory = DbProviderFactories.GetFactory(_providerName);
            var connection = factory.CreateConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }

        public void Insert<T>(T entity, IDbTransaction transaction = null)
        {
            var idProperty = typeof(T).GetProperty("Id");
            var idValue = idProperty?.GetValue(entity);

            if (idValue == null || idValue is long l && l == 0)
            {
                //Insert
                if (transaction != null)
                {
                    InsertWithTransaction(entity, transaction);
                }
                else
                {
                    using var connection = GetConnection();
                    using var tran = connection.BeginTransaction();
                    try
                    {
                        long id = InsertWithTransaction(entity, tran);
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
            else
            {
                // UPDATE
                if (transaction != null)
                {
                    UpdateWithTransaction(entity, transaction);
                }
                else
                {
                    using var connection = GetConnection();
                    using var tran = connection.BeginTransaction();
                    try
                    {
                        long id = UpdateWithTransaction(entity, tran);
                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        protected virtual long InsertWithTransaction<T>(T entity, IDbTransaction transaction)
        {
            // Aqui você pode chamar uma procedure ou montar o comando de insert
            // Exemplo fictício:
            using var command = transaction.Connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = $"Insert{typeof(T).Name}";

            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.Name == "Id") continue;
                var param = command.CreateParameter();
                param.ParameterName = $"@param{prop.Name}";
                param.Value = prop.GetValue(entity) ?? DBNull.Value;
                command.Parameters.Add(param);
            }

            // Executa e captura o Id gerado
            var result = command.ExecuteScalar();
            long id = Convert.ToInt64(result);

            // Seta o Id no objeto, se possível
            var idProp = typeof(T).GetProperty("Id");
            if (idProp != null && idProp.CanWrite)
                idProp.SetValue(entity, id);

            return id;
        }

        protected virtual long UpdateWithTransaction<T>(T entity, IDbTransaction transaction)
        {
            using var command = transaction.Connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = $"Update{typeof(T).Name}";

            foreach (var prop in typeof(T).GetProperties())
            {
                var param = command.CreateParameter();
                param.ParameterName = $"@param{prop.Name}";
                param.Value = prop.GetValue(entity) ?? DBNull.Value;
                command.Parameters.Add(param);
            }

            // Executa e captura o Id gerado
            var result = command.ExecuteScalar();
            long id = Convert.ToInt64(result);

            // Seta o Id no objeto, se possível
            var idProp = typeof(T).GetProperty("Id");
            if (idProp != null && idProp.CanWrite)
                idProp.SetValue(entity, id);

            return id;
        }

        protected IDbCommand CreateStoredProcedureCommand(string procedureName, Dictionary<string, object> paramValues, IDbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = procedureName;

            foreach (var kvp in paramValues)
            {
                var dbParam = command.CreateParameter();
                dbParam.ParameterName = $"@{kvp.Key}";
                dbParam.Value = kvp.Value ?? DBNull.Value;
                command.Parameters.Add(dbParam);
            }

            return command;

        }
    }
}