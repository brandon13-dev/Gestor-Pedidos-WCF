using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shared.DTOs
{
    public class ClientesDTO
    {
        public int ClientesId { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public Nullable<bool> Estatus { get; set; }
    }
}