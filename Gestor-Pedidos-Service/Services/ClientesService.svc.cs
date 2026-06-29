using Business;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Gestor_Pedidos_Service.Contracts
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ClienteService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ClienteService.svc o ClienteService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ClienteService : IClientesService
    {
        private BusinessClientes businessClientes = new BusinessClientes();

        // Metodo para Obtener todos los Clientes
        public List<ClientesDTO> Obtener()
        {
            try
            {
                return businessClientes.Obtener();
            }
            catch (Exception)
            {
                throw new FaultException("Ocurrio un error interno al obtener los pedidos.");
            }
        }

        // Metodo para Obtener un Cliente por su ID
        public ClientesDTO ObtenerPorID(int id)
        {
            try
            {
                return businessClientes.Obtener(id);
            }
            catch (ArgumentException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception)
            {
                throw new FaultException("Ocurrio un error interno al obtener el pedido.");
            }
        }

        // Metodo para Agregar un Cliente
        public void Agregar(ClientesDTO clienteDTO)
        {
            try
            {
                businessClientes.Agregar(clienteDTO);
            }
            catch (ArgumentException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception)
            {
                throw new FaultException("Ocurrio un error al agregar el cliente.");
            }
        }

        // Metodo para Editar un Cliente
        public void Editar(ClientesDTO clienteDTO)
        {
            try
            {
                businessClientes.Editar(clienteDTO);
            }
            catch (ArgumentException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception)
            {
                throw new FaultException("Ocurrio un error al agregar el cliente.");
            }
        }

        // Metodo para Buscar Clientes
        public List<ClientesDTO> Buscar(string busqueda)
        {
            try
            {
                return businessClientes.Buscar(busqueda);
            }
            catch (ArgumentException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception)
            {
                throw new FaultException("Ocurrio un error un error interno al buscar los pedidos.");
            }
        }
    }
}
