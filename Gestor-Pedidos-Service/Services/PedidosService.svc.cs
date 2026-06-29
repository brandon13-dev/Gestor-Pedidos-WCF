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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "PedidosService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione PedidosService.svc o PedidosService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class PedidosService : IPedidosService
    {
        private BusinessPedidos businessPedidos = new BusinessPedidos();

        // Metodo para Obtener todos los Pedidos
        public List<PedidosDTO> Obtener()
        {
            try
            {
                return businessPedidos.Obtener();
            }
            catch (Exception)
            {
                throw new FaultException("Ocurrio un error interno al obtener los pedidos.");
            }
        }

        // Metodo para Obtener un Pedido por ID
        public PedidosDTO ObtenerPorId(int id)
        {
            try
            {
                return businessPedidos.Obtener(id);
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

        // Metodo para Agregar un Pedido
        public void Agregar(PedidosDTO pedidoDto)
        {
            try
            {
                businessPedidos.Agregar(pedidoDto);
            }
            catch (ArgumentException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception)
            {
                throw new FaultException("Ocurrio un error al agregar el pedido.");
            }
        }

        // Metodo para Editar un Pedido
        public void Editar(PedidosDTO pedidoDTO)
        {
            try
            {
                businessPedidos.Editar(pedidoDTO);
            }
            catch (ArgumentException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception)
            {
                throw new FaultException("Ocurrio un error al editar el pedido.");
            }
        }

        // Metodo para Eliminar un Pedido
        public void Eliminar(int id)
        {
            try
            {
                businessPedidos.Eliminar(id);
            }
            catch (ArgumentException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception)
            {
                throw new FaultException("Ocurrio un error al eliminar el pedido.");
            }
        }

        // Metodo para Buscar Pedidos
        public List<PedidosDTO> Buscar(string busqueda)
        {
            try
            {
                return businessPedidos.Buscar(busqueda);
            }
            catch (ArgumentException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception)
            {
                throw new FaultException("Ocurrio un error interno al buscar los pedidos.");
            }
        }

        // Metodo para Generar Reporte de Pedidos
        public List<PedidosDTO> Reporte(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                return businessPedidos.Reporte(fechaInicial, fechaFinal);
            }
            catch (ArgumentException ex)
            {
                throw new FaultException(ex.Message);
            }
            catch (Exception)
            {
                throw new FaultException("Ocurrio un error interno al generar el reporte de los pedidos.");
            }
        }
    }
}
