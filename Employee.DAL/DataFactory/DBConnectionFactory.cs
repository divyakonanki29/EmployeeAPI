using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Employee.DAL.DataFactory
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

    }
}
