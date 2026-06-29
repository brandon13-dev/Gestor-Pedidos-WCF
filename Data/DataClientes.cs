using Entity.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataClientes
    {
        private PedidosClientesEntities _db = new PedidosClientesEntities();

        // Obtener Clientes
        public List<ClientesDTO> Obtener()
        {
            List<Clientes> listaClientes = _db.Clientes.ToList();
            List<ClientesDTO> listaClientesDTO = new List<ClientesDTO>();

            // Mapeamos
            foreach (Clientes cliente in listaClientes)
            {
                listaClientesDTO.Add(MapearDTO(cliente));
            }

            return listaClientesDTO;
        }

        // Metodo para Obtener un Cliente por ID
        public ClientesDTO Obtener(int id)
        {
            Clientes cliente = ExisteCliente(id);

            // Mapeamos
            return MapearDTO(cliente);
        }

        // Metodo par Agregar un Cliente
        public void Agregar(ClientesDTO clienteDTO)
        {
            // Mapeamos el ClienteDTO en un Cliente
            Clientes cliente = new Clientes()
            {
                Nombre = clienteDTO.Nombre,
                Ciudad = clienteDTO.Ciudad,
                Estatus = true
            };

            _db.Clientes.Add(cliente);
            _db.SaveChanges();
        }

        // Metodo para Editar un Cliente
        public void Editar(ClientesDTO clienteDTO)
        {
            Clientes cliente = ExisteCliente(clienteDTO.ClientesId);

            cliente.Nombre = clienteDTO.Nombre;
            cliente.Ciudad = clienteDTO.Ciudad;
            cliente.Estatus = clienteDTO.Estatus;

            _db.SaveChanges();
        }

        // Metodo para Buscar Clientes
        public List<ClientesDTO> Buscar(string busqueda)
        {
            List<Clientes> listaClientes = _db.Clientes
                .Where(c => c.Nombre.Contains(busqueda) ||
                            c.Ciudad.Contains(busqueda))
                .ToList();
            List<ClientesDTO> listaClientesDTO = new List<ClientesDTO>();

            // Mapeamos
            foreach (Clientes cliente in listaClientes)
            {
                listaClientesDTO.Add(MapearDTO(cliente));
            }

            return listaClientesDTO;
        }

        // Metodo auxiliar para Mapear de Clientes a ClientesDTO
        private ClientesDTO MapearDTO(Clientes cliente)
        {
            return new ClientesDTO()
            {
                ClientesId = cliente.ClientesId,
                Nombre = cliente.Nombre,
                Ciudad = cliente.Ciudad,
                Estatus = cliente.Estatus
            };
        }

        // Metodo auxiliar para validar Existencia de Cliente
        private Clientes ExisteCliente(int id)
        {
            Clientes cliente = _db.Clientes
                .FirstOrDefault(c => c.ClientesId == id);

            if (cliente == null)
            {
                throw new Exception("El cliente no existe");
            }

            return cliente;
        }

    }
}
