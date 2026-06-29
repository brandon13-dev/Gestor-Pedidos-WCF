using Entity.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Gestor_Pedidos_Service.Contracts
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IPedidosService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IPedidosService
    {
        [OperationContract]
        List<PedidosDTO> Obtener();

        [OperationContract]
        PedidosDTO ObtenerPorId(int id);

        [OperationContract]
        void Agregar(PedidosDTO pedidoDto);

        [OperationContract]
        void Editar(PedidosDTO pedidoDTO);

        [OperationContract]
        void Eliminar(int id);

        [OperationContract]
        List<PedidosDTO> Buscar(string busqueda);

        [OperationContract]
        List<PedidosDTO> Reporte(DateTime fechaInicial, DateTime fechaFinal);
    }
}
