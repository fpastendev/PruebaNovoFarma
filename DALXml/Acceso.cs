using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Entidades;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace DALXml
{
    public class Acceso
    {
        string ruta = String.Format(@"{0}\{1}",Directory.GetCurrentDirectory(),"datos.xml");

        public List<Persona> ListaPersonas(int i = 0)
        {
            ActivaTodos();

            var lista = new List<Persona>();
            XmlSerializer reader = new XmlSerializer(typeof(ArrayOfPersona));
            StreamReader file;

            try
            {
                file = new StreamReader(ruta);
                ArrayOfPersona arr = (ArrayOfPersona)reader.Deserialize(file);

                foreach (var item in arr.personas)
                {
                    Persona p = new Persona();
                    p.Id = item.Id;
                    p.Nombre = item.Nombre;
                    p.Apellido = item.Apellido;
                    p.Direccion = item.Direccion;
                    p.Activo = item.Activo;

                    lista.Add(p);
                }

                file.Close();

            }
            catch (Exception ex)
            {

            }
            
            if (i == 1)
            {
                lista = lista.Where(x => x.Activo == true).ToList();
            }

            


            return lista;
        }

        public bool CreaPersona(Persona p)
        {
            var res = false;
            var lista = new List<Persona>();

            if (ExistePersona(p.Nombre,p.Apellido,p.Direccion))
            {
                return res;
            }

            try
            {
                if (!File.Exists(ruta))
                {
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.Indent = true;
                    xmlWriterSettings.NewLineOnAttributes = true;
                    using (XmlWriter xmlWriter = XmlWriter.Create(ruta, xmlWriterSettings))
                    {
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("ArrayOfPersona");

                        xmlWriter.WriteStartElement("Persona");
                        xmlWriter.WriteElementString("Id", p.Id);
                        xmlWriter.WriteElementString("Nombre", p.Nombre);
                        xmlWriter.WriteElementString("Apellido", p.Apellido);
                        xmlWriter.WriteElementString("Direccion", p.Direccion);
                        xmlWriter.WriteElementString("Activo", p.Activo.ToString().ToLower());
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument();

                        xmlWriter.Flush();
                        xmlWriter.Close();
                    }
                }
                else
                {
                    XDocument xDocument = XDocument.Load(ruta);
                    XElement root = xDocument.Element("ArrayOfPersona");
                    IEnumerable<XElement> rows = root.Descendants("Persona");
                    XElement lastRow = rows.Last();
                    lastRow.AddAfterSelf(
                      new XElement("Persona",
                      new XElement("Id", p.Id),
                      new XElement("Nombre", p.Nombre),
                      new XElement("Apellido", p.Apellido),
                      new XElement("Direccion", p.Direccion),
                      new XElement("Activo", p.Activo)
                      )
                      );
                    xDocument.Save(ruta);
                    xDocument = null;               

                }

                res = true;
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        public bool EditaPersona(Persona p)
        {
            var res = false;

            try
            {
                XDocument xDocument = XDocument.Load(ruta);

                var items = from item in xDocument.Descendants("Persona")
                            where item.Element("Id").Value == p.Id
                            select item;

                foreach (XElement itemElement in items)
                {
                    itemElement.SetElementValue("Nombre", p.Nombre);
                    itemElement.SetElementValue("Apellido", p.Apellido);
                    itemElement.SetElementValue("Direccion", p.Direccion);
                    itemElement.SetElementValue("Activo", p.Activo.ToString().ToLower());
                }

                xDocument.Save(ruta);
                xDocument = null;

                res = true;

            }
            catch (Exception ex)
            {

            }

            return res;
        }

        public bool DesactivaPersona(string id)
        {
            var res = false;

            try
            {
                XDocument xDocument = XDocument.Load(ruta);

                var items = from item in xDocument.Descendants("Persona")
                            where item.Element("Id").Value == id
                            select item;

                foreach (XElement itemElement in items)
                {
                    itemElement.SetElementValue("Activo", "false");
                }

                xDocument.Save(ruta);
                res = true;

            }
            catch (Exception ex)
            {

            }

            return res;
        }

        public Persona GetPersona(string id)
        {
            var p = new Persona();

            try
            {
                XDocument xDocument = XDocument.Load(ruta);

                var persona = (from item in xDocument.Descendants("Persona")
                            where item.Element("Id").Value == id
                            select item).FirstOrDefault();

                if (persona != null)
                {
                    p.Id = persona.Element("Id").Value.ToString();
                    p.Nombre = persona.Element("Nombre").Value.ToString();
                    p.Apellido = persona.Element("Apellido").Value.ToString();
                    p.Direccion = persona.Element("Direccion").Value.ToString();
                    p.Activo = Convert.ToBoolean(persona.Element("Activo").Value.ToString());
                }
            }
            catch (Exception ex)
            {

            }

            return p;
        }

        public bool ExistePersona(string nombre, string apellido, string direccion)
        {
            var p = new Persona();
            var res = false;

            try
            {
                XDocument xDocument = XDocument.Load(ruta);

                var persona = (from item in xDocument.Descendants("Persona")
                               where item.Element("Nombre").Value == nombre &&
                                item.Element("Apellido").Value == apellido &&
                                item.Element("Direccion").Value == direccion
                               select item).FirstOrDefault();

                if (persona != null)
                {
                    res = true;
                }
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        private FileStream GetFile(string ruta)
        {
            if (!File.Exists(ruta))
            {
                return File.Create(ruta);
            }
            else
            {
                return File.OpenWrite(ruta);
            }
        }

        private void ActivaTodos()
        {
            bool activa = Convert.ToBoolean(ConfigurationManager.AppSettings["inactivos"]);

            if (activa)
            {
                try
                {
                    XDocument xDocument = XDocument.Load(ruta);

                    var items = from item in xDocument.Descendants("Persona")
                                select item;

                    foreach (XElement itemElement in items)
                    {
                        itemElement.SetElementValue("Activo", "true");
                    }

                    xDocument.Save(ruta);

                }
                catch (Exception ex)
                {

                }
            }
        }

    }
}
