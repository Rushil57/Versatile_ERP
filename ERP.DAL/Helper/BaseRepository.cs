using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace ERP.DAL.Helper
{
    public class BaseRepository
    {
        private bool connection_open;
        protected SqlConnection connection;
        public void ErrorLogData(Exception ex)
        {
            string appData = "";//HostingEnvironment.MapPath("~/ErrorLog");
            string filePath = Path.Combine(appData, "ErrorLog_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt");
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }

        }
        protected SqlConnection Get_Connection(IConfiguration configuration)
        {
            connection_open = false;
            connection = new SqlConnection();
            connection.ConnectionString = configuration.GetConnectionString("VTPL_ERP_ConnectionString");
            if (Open_Connection())
            {
                connection_open = true;
                return connection;
            }
            else
            {
                throw new Exception("Error");
            }
        }

        private bool Open_Connection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
