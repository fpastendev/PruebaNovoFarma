using System.Collections.Generic;
using Entidades;
using DALXml;

namespace BLL
{
    public class Negocio
    {
        Acceso acc = new Acceso();

        public List<Persona> ListaPersonas(int i = 0)
        {
            return acc.ListaPersonas(i);
        }

        public bool CreaPersona(Persona p)
        {
            return acc.CreaPersona(p);
        }

        public bool EditaPersona(Persona p)
        {
            return acc.EditaPersona(p);
        }

        public bool DesactivaPersona(string id)
        {
            return acc.DesactivaPersona(id);
        }

        public Persona GetPersona(string id)
        {
            return acc.GetPersona(id);
        }
    }
}
