using AspMap;
using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateKmlFromLatLon
{
    class ProcessLatLonData
    {
        string TRAVELPATH_LATLONG = string.Empty;
        public StringBuilder bpoints = new StringBuilder();
        public static string FinalPath;
        public static string Path = ConfigurationManager.AppSettings["KMLCreationPath"].ToString();
        public static string KmlName = "test";
        public bool ProcessKML(string LATLONG)
        {
            bpoints.Clear();
            try
            {
                TRAVELPATH_LATLONG = string.Empty;
                CoordinateCollection coordinates = new CoordinateCollection();
                Vector Firstcoordinates = new Vector();
                Vector Lastcoordinates = new Vector();

                DataTable dtTripData = new DataTable();
                TRAVELPATH_LATLONG = LATLONG;
                DataTable dt = new DataTable();

                //Route_Id = Convert.ToString(SUB_ROUTE_ID);
                int bCount = 0;

                AspMap.Polyline line = new AspMap.Polyline();

                AspMap.Shape buffer;

                string[] PolyParamaters = TRAVELPATH_LATLONG.Split(':');
                string[] PolygaonCoordinates;
                AspMap.Points part = new AspMap.Points();
                int a = 0;
                AspMap.Points P_point = new AspMap.Points();
                AspMap.Point l_point = new AspMap.Point();
                DataTable dtRoutePoints = new DataTable();
                AspMap.Web.Map map1 = new AspMap.Web.Map(); //


                if (PolyParamaters.Length > 1)
                {
                    //double Lastlat = 0.0;
                    //double Lastlon = 0.0;

                    for (a = 0; a < PolyParamaters.Length - 2; a++)
                    {
                        PolygaonCoordinates = PolyParamaters[a].ToString().Split(',');
                        //double lat;
                        //double lon;
                        if (Convert.ToDouble(PolygaonCoordinates[1].ToString()) > Convert.ToDouble(PolygaonCoordinates[0].ToString()))
                        {
                            l_point.X = Convert.ToDouble(PolygaonCoordinates[1].ToString());
                            l_point.Y = Convert.ToDouble(PolygaonCoordinates[0].ToString());
                        }
                        else
                        {
                            l_point.X = Convert.ToDouble(PolygaonCoordinates[0].ToString());
                            l_point.Y = Convert.ToDouble(PolygaonCoordinates[1].ToString());
                        }
                        P_point.Add(l_point);
                    }

                    line.Add(P_point);

                    AspMap.Shape shape = new AspMap.Shape(AspMap.ShapeType.Line);
                    AspMap.Shape shape2 = (AspMap.Shape)line;
                    shape = line;
                    var distance = 50.00;
                    //BufferScale

                    double bufferInMeter = map1.ConvertDistance(distance, MeasureUnit.Meter, MeasureUnit.Degree);
                    //double bufferInMeter = distance;
                    buffer = shape2.Buffer(Convert.ToDouble(bufferInMeter));
                    bCount = buffer.PointCount;

                    AspMap.Point p = new AspMap.Point();
                    AspMap.Point point = new AspMap.Point();
                    int point_distance = 0;
                    AspMap.Point prev_point = new AspMap.Point();
                    //p = new AspMap.Point();
                    for (int l = 0; l < bCount - 1; l++)
                    {

                        if (Convert.ToDouble(buffer.GetPoint(l).X.ToString()) > Convert.ToDouble(buffer.GetPoint(l).Y.ToString()))
                        {
                            point.X = Convert.ToDouble(buffer.GetPoint(l).X.ToString());
                            point.Y = Convert.ToDouble(buffer.GetPoint(l).Y.ToString());

                        }
                        else
                        {
                            point.X = Convert.ToDouble(buffer.GetPoint(l).Y.ToString());
                            point.Y = Convert.ToDouble(buffer.GetPoint(l).X.ToString());
                        }

                        p = point;

                        if (l > 0)
                        {
                            point_distance = (int)(Math.Sqrt((Math.Pow((prev_point.X - p.X), 2) * 111) + (Math.Pow((prev_point.Y - p.Y), 2) * 103)) * 10 * 1000);
                            //Console.WriteLine("Distance : " + point_distance);
                            if (point_distance >= 5000)
                            {
                                //Console.WriteLine("Previous Point : " + prev_point.X + " : " + prev_point.Y);
                                // Console.WriteLine("Skip Point : " + p.X + " : " + p.Y);
                            }

                            else
                            {
                                bpoints.Append(p.X + "," + p.Y + ":");
                                prev_point.X = p.X;
                                prev_point.Y = p.Y;
                            }
                        }
                        else if (l == 0)
                        {
                            prev_point.X = p.X;
                            prev_point.Y = p.Y;
                        }
                    }


                    string[] BString = bpoints.ToString().Split(':').ToArray(); ;
                    string[] BStringCoordinates;

                    if (BString.Length > 1)
                    {

                        for (a = 0; a < BString.Length - 2; a++)
                        {
                            BStringCoordinates = BString[a].ToString().Split(',');
                            double lat;
                            double lon;
                            if (Convert.ToDouble(BStringCoordinates[1].ToString()) > Convert.ToDouble(BStringCoordinates[0].ToString()))
                            {

                                lat = Convert.ToDouble(BStringCoordinates[0].ToString());
                                lon = Convert.ToDouble(BStringCoordinates[1].ToString());

                            }
                            else
                            {
                                lat = Convert.ToDouble(BStringCoordinates[1].ToString());
                                lon = Convert.ToDouble(BStringCoordinates[0].ToString());
                            }

                            coordinates.Add(new Vector(lat, lon, 0));

                        }
                    }

                    //Method to Create KML
                    CreateKml.CreateKML(coordinates, Firstcoordinates, Lastcoordinates);

                }


            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog("ProcessKML", ex.Message);
                return false;
            }
            finally
            {

            }
            return true;
        }
     
    }
}
