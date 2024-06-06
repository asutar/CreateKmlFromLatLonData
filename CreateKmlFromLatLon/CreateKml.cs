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
   public static class CreateKml
    {
        public static string FinalPath;
        public static string Path = ConfigurationManager.AppSettings["KMLCreationPath"].ToString();
        public static string KmlName = "test";
        public static void CreateKML(CoordinateCollection Coordinates, Vector FirstCoordinate, Vector LastCoordinate)
        {
            try
            {
                DataTable dt = new DataTable();
                var document = new Document();
                document.Id = "Document";
                document.Name = "Document";

                Description dsc = new Description();
                dsc.Text = @"<h1>KML Creation</h1> ";


                OuterBoundary outerBoundary = new OuterBoundary();
                outerBoundary.LinearRing = new LinearRing();
                outerBoundary.LinearRing.Coordinates = Coordinates;

                // Polygon Setting:
                SharpKml.Dom.Polygon polygon = new SharpKml.Dom.Polygon();
                polygon.Extrude = true;
                polygon.AltitudeMode = AltitudeMode.ClampToGround;
                polygon.OuterBoundary = outerBoundary;

                //Color Style Setting:
                byte byte_Color_R = 00, byte_Color_G = 200, byte_Color_B = 00, byte_Color_A = 150; //you may get your own color by other method
                var style = new SharpKml.Dom.Style();

                style.Polygon = new PolygonStyle();
                style.Polygon.ColorMode = SharpKml.Dom.ColorMode.Normal;
                style.Polygon.Color = new Color32(byte_Color_A, byte_Color_B, byte_Color_G, byte_Color_R);

                //Set the polygon and style to the Placemark:
                Placemark placemark = new Placemark();
                placemark.Name = "Test Kml";
                placemark.Geometry = polygon;
                placemark.AddStyle(style);
                //Finally to the document and save it
                // Create Style with id
                SharpKml.Dom.LineStyle lineStyle = new SharpKml.Dom.LineStyle();
                lineStyle.Color = Color32.Parse("7f00ff00");
                lineStyle.Width = 1;

                Style roadStyle = new Style();
                roadStyle.Id = "RoadStyle";
                roadStyle.Line = lineStyle;

                document.AddFeature(placemark);
                var kml = new Kml();
                kml.Feature = document;

                KmlFile kmlFile = KmlFile.Create(kml, true);
                FinalPath = Path + "" + KmlName + ".kml";

                if (File.Exists(FinalPath))
                {
                    File.Delete(FinalPath);
                }

                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                using (var stream = System.IO.File.OpenWrite(FinalPath))
                {
                    kmlFile.Save(stream);
                }


            }
            catch (Exception ex)
            {
                ErrorLog.WriteLog("Create KML", ex.Message);
            }
        }
    }
}
