using System;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace 의료IT공학과.데이터베이스
{
    public class LocalDB
    {
        OleDbCommand comm = null;
        OleDbConnection conn = null; 
        OleDbDataReader reader = null;
        string connectionStr = null;

        public LocalDB(string connStr)
        {
            connectionStr = connStr;
        }

        public void Open()
        {
            if (conn != null) Close();

            comm = new OleDbCommand();
            conn = new OleDbConnection();

            conn.ConnectionString = connectionStr;
            conn.Open();
            comm.Connection = conn;
        }

        public void Close()
        {
            if (conn != null) conn.Close();
            if (reader != null) reader.Close();
            conn = null;
            comm = null;
            reader = null;
        }

        public void ExecuteNonQuery(string sql)
        {
            comm.CommandText = sql;
            comm.ExecuteNonQuery();
        }

        public void ExecuteReader(string sql)
        {
            if (reader != null) reader.Close();
            comm.CommandText = sql;
            reader = comm.ExecuteReader();
        }

        public bool Read()
        {
            if (reader == null) return false;

            return reader.Read();
        }

        public object GetData(string dataName)
        {
            return reader[dataName];
        }

        public object GetData(int index)
        {
            return reader[index];
        }

    }//class
}