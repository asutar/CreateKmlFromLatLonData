using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateKmlFromLatLon
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessLatLonData _ObjProcessLatLonData = new ProcessLatLonData();
            LatLonData.Data();
            ///Getting data from LatLonData.Data() so that you can pass your lat,lon data in string format
            _ObjProcessLatLonData.ProcessKML(LatLonData.Data());
        }
    }
}
