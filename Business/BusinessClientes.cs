using Data;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessClientes
    {
        private DataClientes dataClientes = new DataClientes();

        // Metodo para Obtener Clientes
        public List<ClientesDTO> Obtener()
        {
            return dataClientes.Obtener();
        }

        // Metodo para Obtener Cliente por ID
        public ClientesDTO Obtener(int id)
        {
            // Validamos que el ID sea correcto
            ValidarId(id);

            // Si no hay errores -> Obtenemos Cliente
            return dataClientes.Obtener(id);
        }

        // Metodo para Agregar Cliente
        public void Agregar(ClientesDTO clienteDTO)
        {
            // Validamos todo el ClienteDTO
            ValidarCliente(clienteDTO);

            // Si no hay errores -> Agregamos
            dataClientes.Agregar(clienteDTO);
        }

        // Metodo para Editar Cliente
        public void Editar(ClientesDTO clienteDTO)
        {
            // Validamos todo el ClienteDTO
            ValidarCliente(clienteDTO);

            // Validamos que el ID sea correcto
            ValidarId(clienteDTO.ClientesId);

            // Si no hay errores -> Editamos
            dataClientes.Editar(clienteDTO);
        }

        // Metodo para Buscar Clientes
        public List<ClientesDTO> Buscar(string busqueda)
        {
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                return Obtener();
            }

            // Si busqueda no es vacio -> Buscamos
            return dataClientes.Buscar(busqueda);
        }

        // Metodo auxiliar para Validar propiedades del ClienteDTO
        private string Validaciones(ClientesDTO clienteDTO)
        {
            string errores = "";
            if (string.IsNullOrWhiteSpace(clienteDTO.Nombre))
            {
                errores += "El nombre del cliente es obligatorio.<br />";
            }
            if (string.IsNullOrWhiteSpace(clienteDTO.Ciudad))
            {
                errores += "La ciudad del cliente es obligatoria.<br />";
            }
            return errores;
        }

        // Metodo auxiliar para Validar Cliente DTO
        private void ValidarCliente(ClientesDTO clienteDTO)
        {
            // Validamos que el clienteDTO no venga nulo
            if (clienteDTO == null)
            {
                throw new ArgumentNullException(nameof(clienteDTO), "No puede ser nulo");
            }

            // Validamos antes de pasar a capa de datos
            string errores = Validaciones(clienteDTO);

            if (!string.IsNullOrEmpty(errores))
            {
                throw new ArgumentException(errores);
            }
        }

        // Metodo auxiliar para Validar el ID
        private void ValidarId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID invalido");
            }
        }
    }
}
