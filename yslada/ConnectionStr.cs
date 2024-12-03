using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yslada
{
    public class ConnectionStr
    {
        public static string connectionString()
        {
            string server = Properties.Settings.Default.Server;
            string database = Properties.Settings.Default.database;
            string uid = Properties.Settings.Default.uid;
            string pwd = Properties.Settings.Default.pwd;
            string conStr = $"Server={server}; Database={database}; Uid={uid}; Pwd={pwd}";
            return conStr;
        }
    }
}
