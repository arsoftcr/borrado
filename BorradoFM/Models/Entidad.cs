using System;
namespace BorradoFM.Models
{
    public class Entidad
    {
        public Entidad()
        {
        }

        public string EntidadString { get; set; }

        public string Id { get; set; }

        public Payload Payload { get; set; }

    }


    public class Payload
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class Token
    {

        public string token { get; set; }
    }
}
