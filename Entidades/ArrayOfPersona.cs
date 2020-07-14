using System.Collections.Generic;
using System.Xml.Serialization;

namespace Entidades
{
    //[XmlRoot("ArrayOfEducation", Namespace = "http://schemas.datacontract.org/2004/07/CovUni.Domain.Admissions")]
    //xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    //[XmlRoot("ArrayOfPersona", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
    public class ArrayOfPersona
    {
        [XmlElement("Persona")]
        public List<Persona> personas { get; set; }
    }
}
