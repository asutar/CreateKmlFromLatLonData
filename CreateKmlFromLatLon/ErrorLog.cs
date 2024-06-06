using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateKmlFromLatLon
{
    public class ErrorLog
    {
        public static void WriteLog(string sfunc, string sMsg)
        {
            string logFileDir = ConfigurationManager.AppSettings["LogLocation"].ToString();
            string logFile = logFileDir + "\\App_Log" + DateTime.Now.ToString("ddMMMyyyy").Trim() + ".txt";

            if (!Directory.Exists(logFileDir))
                Directory.CreateDirectory(logFile);

            using (StreamWriter strWriter = new StreamWriter(logFile, true))
            {
                strWriter.WriteLine((sfunc + "  >> " + sMsg + ">>" + DateTime.Now.ToString()));
                strWriter.Flush();
                strWriter.Dispose();
            }

        }
    }
}
