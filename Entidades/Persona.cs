using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Entidades
{
    public class Persona
    {
        [XmlElement(ElementName = "Id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "Nombre")]
        [Required(ErrorMessage = "Largo no superior a 15")]
        [StringLength(15)]
        public string Nombre { get; set; }
        [XmlElement(ElementName = "Apellido")]
        [Required(ErrorMessage = "Largo no superior a 30")]
        [StringLength(30)]
        public string Apellido { get; set; }
        [XmlElement(ElementName = "Direccion")]
        [Required(ErrorMessage = "Largo no superior a 30")]
        [StringLength(30)]
        public string Direccion { get; set; }
        [XmlElement(ElementName = "Activo")]
        public bool Activo { get; set; }
    }
}
