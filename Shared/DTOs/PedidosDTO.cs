using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class PedidosDTO
    {
        public int PedidoId { get; set; }

        public DateTime? Fecha { get; set; }

        public decimal? Total { get; set; }

        public string Descripcion { get; set; }

        public Nullable<int> ClientesId { get; set; }

        public ClientesDTO Clientes { get; set; }
    }
}