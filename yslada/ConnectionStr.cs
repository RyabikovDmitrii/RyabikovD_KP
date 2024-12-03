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
            string conStr = $"Server=127.0.0.1; Database=yslada_upd; Uid=root; Pwd=root";
            return conStr;
        }
    }
}
