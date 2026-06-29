using Data;
using Shared.DTOs;
using System;
using System.Collections.Generic;

namespace Business
{
    public class BusinessPedidos
    {
        DataPedidos dataPedidos = new DataPedidos();

        // Metodo para Obtener Pedidos
        public List<PedidosDTO> Obtener()
        {
            return dataPedidos.Obtener();
        }

        // Metodo para Obtener Pedido por ID
        public PedidosDTO Obtener(int id)
        {
            // Validamos que el ID sea correcto
            ValidarID(id);

            // Si no hay errores -> Obtener Pedidos
            return dataPedidos.Obtener(id);
        }

        // Metodo para Agregar Pedido
        public void Agregar(PedidosDTO pedidoDTO)
        {
            // Validamos todo el PedidoDTO
            ValidarPedido(pedidoDTO);

            // Si no hay errores -> Agregamos y retornamos un mensaje de confirmacion
            dataPedidos.Agregar(pedidoDTO);
        }

        // Metodo para Editar Pedido
        public void Editar(PedidosDTO pedidoDTO)
        {
            // Validamos todo el pedidoDTO
            ValidarPedido(pedidoDTO);

            // Validsmoa que el ID sea correcto
            ValidarID(pedidoDTO.PedidoId);

            // Si no hay errores -> Editamos
            dataPedidos.Editar(pedidoDTO);
        }

        // Metodo para Eliminar Pedido
        public void Eliminar(int id)
        {
            // Valismoa que el ID sea correcto
            ValidarID(id);

            // Si no hay errores -> Eliminamos
            dataPedidos.Eliminar(id);
        }

        // Metodo para Buscar Pedidos
        public List<PedidosDTO> Buscar(string busqueda)
        {
            // Validamos que la busqueda no sea vacia
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                return Obtener();
            }

            // Si no hay errores -> Buscamos
            return dataPedidos.Buscar(busqueda);
        }

        // Metodo para Generar Reporte de Pedidos entre Fechas
        public List<PedidosDTO> Reporte(DateTime? fechaInicial, DateTime? fechaFinal)
        {
            // Validamos que las fechas no sean nulas
            if (!fechaInicial.HasValue || !fechaFinal.HasValue)
            {
                throw new ArgumentException("Debe de proporcionar ambas fechas");
            }

            // Validamos que la fecha inicial no sea mayor que la fecha final
            if (fechaInicial > fechaFinal)
            {
                throw new ArgumentException("La fecha inicial no puede ser mayor a la final");
            }

            // Si no hay errores -> Generamos Reporte
            return dataPedidos.Reporte(fechaInicial.Value, fechaFinal.Value);
        }

        // Metodo auxiliar para validar que el pedido no tenga nulos
        private string Validaciones(PedidosDTO pedidoDTO)
        {
            string errores = "";
            if (!pedidoDTO.Fecha.HasValue)
            {
                errores += "La fecha del pedido es obligatoria.<br />";
            }
            else if (pedidoDTO.Fecha.Value < DateTime.Today)
            {
                errores += "La fecha del pedido no puede ser anterior a hoy. <br />";
            }
            if (!pedidoDTO.Total.HasValue || pedidoDTO.Total <= 0)
            {
                errores += "El total del pedido debe de ser mayor a $0.00<br />";
            }
            if (string.IsNullOrWhiteSpace(pedidoDTO.Descripcion))
            {
                errores += "La descripcion del pedido es obligatoria.<br />";
            }
            if (!pedidoDTO.ClientesId.HasValue)
            {
                errores += "El cliente del pedido es obligatorio";
            }
            return errores;
        }

        // Metodo auxiliar para Validar Pedido DTO
        private void ValidarPedido(PedidosDTO pedidoDTO)
        {
            // Validamos que el pedidoDTO no venga nulo
            if (pedidoDTO == null)
            {
                throw new ArgumentNullException(nameof(pedidoDTO), "No puede ser nulo");
            }

            // Validamos los campos
            string errores = Validaciones(pedidoDTO);

            if (!string.IsNullOrEmpty(errores))
            {
                throw new ArgumentException(errores);
            }
        }

        // Metodo auxiliar para Validar el ID
        private void ValidarID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID invalido");
            }
        }
    }
}
