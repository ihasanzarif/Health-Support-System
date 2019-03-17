using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace HealthSupportApp.Gateway
{
    public class ParentGateway
    {
        public string ConnectionString =
            WebConfigurationManager.ConnectionStrings["HealthSupportDbContext"].ConnectionString;

        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        public SqlDataReader Reader { get; set; }
        public string Query { get; set; }

        public ParentGateway()
        {
            Connection = new SqlConnection(ConnectionString);
        }
    }
}