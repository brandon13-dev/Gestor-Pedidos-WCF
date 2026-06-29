using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Gestor_Pedidos_Service.Contracts
{
    [ServiceContract]
    public interface IClientesService
    {
        [OperationContract]
        List<ClientesDTO> Obtener();

        [OperationContract]
        ClientesDTO ObtenerPorID(int id);

        [OperationContract]
        void Agregar(ClientesDTO clienteDTO);

        [OperationContract]
        void Editar(ClientesDTO clienteDTO);

        [OperationContract]
        List<ClientesDTO> Buscar(string busqueda);
    }
}
