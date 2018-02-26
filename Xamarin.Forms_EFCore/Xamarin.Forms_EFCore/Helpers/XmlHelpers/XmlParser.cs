using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Xamarin.Forms_EFCore.Helpers.XmlHelpers
{
    public class XmlParser
    {

        public void ParseGeo()
        {
            XmlDocument geodoc = new XmlDocument();

            var assembly = typeof(XmlParser).GetTypeInfo().Assembly;

            /*Definovanie cesty suboru*/
            Stream stream = assembly.GetManifestResourceStream("Xamarin.Forms_EFCore.geodata.xml");


            try
            {
                geodoc.Load(stream);
            }catch(Exception r)
            {
                throw r;
            }


            foreach (XmlNode node in geodoc)
            {
                try
                {
                    string pokus = node.Attributes[0].Value;
                System.Diagnostics.Debug.WriteLine("moje xml " + pokus);
                }
                catch (Exception r)
                {
                    throw r;
                }
            }
                       
        }

        public async void LoadXMLData()
        {
            List<GPS> rawData = null;
            await Task.Factory.StartNew(delegate {
                XDocument doc = XDocument.Load("geodata.xml");
                IEnumerable<GPS> gpss = from s in doc.Descendants("trkpt")
                                        select new GPS
                                        {
                                            Lat = float.Parse(s.Attribute("lat").Value),
                                            Lon = float.Parse(s.Attribute("lon").Value)

                                        };
               
                rawData = gpss.ToList();
            });
           foreach(var a in rawData)
            {
                System.Diagnostics.Debug.WriteLine("moje xml " + a.Lat + " " + a.Lon);

            }
        }


    }

}
