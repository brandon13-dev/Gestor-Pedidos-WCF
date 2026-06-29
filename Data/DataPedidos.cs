using Entity.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataPedidos
    {
        PedidosClientesEntities _db = new PedidosClientesEntities();

        // Metodo para Obtener todos los Pedidos
        public List<PedidosDTO> Obtener()
        {
            List<Pedidos> listaPedidos = _db.Pedidos.Include("Clientes").ToList();
            List<PedidosDTO> listaPedidosDTO = new List<PedidosDTO>();

            // Mapeamos
            foreach(Pedidos pedido in listaPedidos)
            {
                listaPedidosDTO.Add(MapearDTO(pedido));
            }
            return listaPedidosDTO;
        }

        // Metodo para Obtener un Pedido por su ID
        public PedidosDTO Obtener(int id)
        {
            Pedidos pedido = ExistePedido(id);

            // Mapeamos
            return MapearDTO(pedido);
        }

        // Metodo para Agregar un Pedido
        public void Agregar(PedidosDTO pedidoDTO)
        {
            // Validamos que exista Cliente
            ExisteCliente(pedidoDTO.ClientesId.Value);

            // Mapeamos el PedidoDTO a un Pedido
            Pedidos pedido = new Pedidos()
            {
                Fecha = pedidoDTO.Fecha,
                Total = pedidoDTO.Total,
                Descripcion = pedidoDTO.Descripcion,
                ClientesId = pedidoDTO.ClientesId
            };

            // Validamos que exista Cliente
            ExisteCliente(pedidoDTO.ClientesId.Value);

            _db.Pedidos.Add(pedido);
            _db.SaveChanges();
        }

        // Metodo para Editar un Pedido
        public void Editar(PedidosDTO pedidoDTO)
        {
            // Validamos que exista el Pedido
            Pedidos pedido = ExistePedido(pedidoDTO.PedidoId);

            // Validamos que exista Cliente
            ExisteCliente(pedidoDTO.ClientesId.Value);

            pedido.Fecha = pedidoDTO.Fecha;
            pedido.Total = pedidoDTO.Total;
            pedido.Descripcion = pedidoDTO.Descripcion;
            pedido.ClientesId = pedidoDTO.ClientesId;

            _db.SaveChanges();
        }

        // Metodo para Eliminar un Pedido
        public void Eliminar(int id)
        {
            Pedidos pedido = ExistePedido(id);

            _db.Pedidos.Remove(pedido);
            _db.SaveChanges();
        }

        // Metodo para Buscar Pedidos
        public List<PedidosDTO> Buscar(string busqueda)
        {
            List<Pedidos> listaPedidos = _db.Pedidos
                .Include("Clientes")
                .Where(x => x.Descripcion.Contains(busqueda) ||
                            x.Clientes.Nombre.Contains(busqueda))
                .ToList();

            List<PedidosDTO> listaPedidosDTO = new List<PedidosDTO>();

            // Mapeamos
            foreach (Pedidos pedido in listaPedidos)
            {
                listaPedidosDTO.Add(MapearDTO(pedido));
            }
            return listaPedidosDTO;
        }

        // Metodo para generar Reporte de Pedidos
        public List<PedidosDTO> Reporte(DateTime fechaInicial, DateTime fechaFinal)
        {
            List<Pedidos> listaPedidos = _db.Pedidos
                .Include("Clientes")
                .Where(p => p.Fecha >= fechaInicial && p.Fecha <= fechaFinal)
                .ToList();

            List<PedidosDTO> listaPedidosDTO = new List<PedidosDTO>();

            // Mapeamos
            foreach (Pedidos pedido in listaPedidos)
            {
                listaPedidosDTO.Add(MapearDTO(pedido));
            }
            return listaPedidosDTO;
        }

        // Metodo auxiliar para Mapear de Pedidos a PedidosDTO
        private PedidosDTO MapearDTO(Pedidos pedido)
        {
            ClientesDTO clienteDTO = new ClientesDTO()
            {
                ClientesId = pedido.Clientes.ClientesId,
                Nombre = pedido.Clientes.Nombre,
                Ciudad = pedido.Clientes.Ciudad,
                Estatus = pedido.Clientes.Estatus
            };

            return new PedidosDTO()
            {

                PedidoId = pedido.PedidoId,
                Fecha = pedido.Fecha,
                Total = pedido.Total,
                Descripcion = pedido.Descripcion,
                ClientesId = pedido.ClientesId,

                Clientes = clienteDTO
            };
        }

        // Metodo auxiliar para Validar Existencia de un Pedido
        private Pedidos ExistePedido(int id)
        {
            Pedidos pedido = _db.Pedidos
                .Include("Clientes")
                .FirstOrDefault(p => p.PedidoId == id);

            if (pedido == null)
            {
                throw new ArgumentException("El pedido no existe");
            }

            return pedido;
        }

        // Metodo auxiliar para Validar Exitencia de un Cliente
        private void ExisteCliente(int id)
        {
            bool existe = _db.Clientes
                .Any(c => c.ClientesId == id);

            if (!existe)
            {
                throw new ArgumentException("El cliente no existe");
            }
        }
    }
}
