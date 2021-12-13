using System;
using System.Collections.Generic;
using System.Data;

namespace DapperDemo.Repository.Dapper
{
    public interface IGenericRepository
    {
        string ConnectionString { get; set; }

        void Execute(string name, CommandType commandType = CommandType.StoredProcedure);
        void Execute(string name, object param, CommandType commandType = CommandType.StoredProcedure);
        List<T> List<T>(string name, CommandType commandType = CommandType.StoredProcedure);
        List<T> List<T>(string name, int id, CommandType commandType = CommandType.StoredProcedure);
        List<T> List<T>(string name, object param, CommandType commandType = CommandType.StoredProcedure);
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> List<T1, T2, T3>(string name, object param, CommandType commandType = CommandType.StoredProcedure);
        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string name, object param, CommandType commandType = CommandType.StoredProcedure);
        void QueryExecute(string name, CommandType commandType = CommandType.Text);
        void QueryExecute(string name, object param, CommandType commandType = CommandType.Text);
        T Single<T>(string name, int id, CommandType commandType = CommandType.StoredProcedure);
        T Single<T>(string name, object param, CommandType commandType = CommandType.StoredProcedure);
    }
}