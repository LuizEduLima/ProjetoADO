using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ADO.Data.Repository
{
    public  class BaseADO : IDisposable
    {
        private IConfiguration Configure { get; set; }
        public SqlConnection Connection { get; }


     
        public BaseADO(IConfiguration configure)
        {
            Configure = configure;
            Connection = new SqlConnection(Configure.GetConnectionString("ADOdb"));

        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
