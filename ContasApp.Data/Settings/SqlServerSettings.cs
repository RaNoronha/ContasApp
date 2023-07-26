using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Settings
{
    public class SqlServerSettings
    {
        public static string ConnectionSql()
        {
            //return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BDContasApp;Integrated Security=True;Connect Timeout=30;Encrypt=False";
            return "Data Source=SQL8005.site4now.net;Initial Catalog=db_a9cbcf_contasapp;User Id=db_a9cbcf_contasapp_admin;Password=Lz5365@pq";
        }
    }
}
