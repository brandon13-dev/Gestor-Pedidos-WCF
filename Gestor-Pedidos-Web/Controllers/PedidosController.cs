using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace Gestor_Pedidos_Web.Controllers
{
    public class PedidosController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var service = new PedidoService.PedidosServiceClient();
            try
            {
                List<PedidosDTO> listaPedidos = service.Obtener().ToList();

                return View("Index", listaPedidos);
            }
            catch (FaultException ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servicio";
                return View("Error");
            }
            finally
            {
                CloseServicePedidos(service);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            var serviceCliente = new ClienteService.ClientesServiceClient();
            try
            {
                PedidosDTO pedido = new PedidosDTO();
                List<ClientesDTO> ls = serviceCliente.Obtener().ToList();
                ViewBag.ClientesId = new SelectList(ls, "ClientesId", "Nombre");
                return View("Create", pedido);
            }
            catch (FaultException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servicio";
                return RedirectToAction("Index");
            }
            finally
            {
                CloseServiceClientes(serviceCliente);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(PedidosDTO pedidoDTO)
        {
            var service = new PedidoService.PedidosServiceClient();
            var serviceCliente = new ClienteService.ClientesServiceClient();
            try
            {
                service.Agregar(pedidoDTO);
                TempData["success"] = "Pedido agregado correctamente.";
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                TempData["error"] = ex.Message;
                List<ClientesDTO> ls = serviceCliente.Obtener().ToList();
                ViewBag.ClientesId = new SelectList(ls, "ClientesId", "Nombre", pedidoDTO.ClientesId);
                return View("Create", pedidoDTO);
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servicio";
                return RedirectToAction("Index");
            }
            finally
            {
                CloseServicePedidos(service);
                CloseServiceClientes(serviceCliente);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var service = new PedidoService.PedidosServiceClient();
            var serviceCliente = new ClienteService.ClientesServiceClient();
            try
            {
                PedidosDTO pedidoDTO = service.ObtenerPorId(id);
                List<ClientesDTO> ls = serviceCliente.Obtener().ToList();
                ViewBag.ClientesId = new SelectList(ls, "ClientesId", "Nombre", pedidoDTO.ClientesId);
                return View("Edit", pedidoDTO);
            }
            catch (FaultException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servicio";
                return RedirectToAction("Index");
            }
            finally
            {
                CloseServicePedidos(service);
                CloseServiceClientes(serviceCliente);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(PedidosDTO pedidoDTO)
        {
            var service = new PedidoService.PedidosServiceClient();
            var serviceCliente = new ClienteService.ClientesServiceClient();
            try
            {
                service.Editar(pedidoDTO);
                TempData["success"] = "Pedido editado correctamente";
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                TempData["error"] = ex.Message;
                List<ClientesDTO> ls = serviceCliente.Obtener().ToList();
                ViewBag.ClientesId = new SelectList(ls, "ClientesId", "Nombre", pedidoDTO.ClientesId);
                return View("Edit", pedidoDTO);
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servicio";
                return RedirectToAction("Index");
            }
            finally
            {
                CloseServiceClientes(serviceCliente);
                CloseServicePedidos(service);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var service = new PedidoService.PedidosServiceClient();
            try
            {
                PedidosDTO pedidoDTO = service.ObtenerPorId(id);
                return View("Delete", pedidoDTO);
            }
            catch (FaultException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servicio";
                return RedirectToAction("Index");
            }
            finally
            {
                CloseServicePedidos(service);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = new PedidoService.PedidosServiceClient();
            try
            {
                service.Eliminar(id);
                TempData["success"] = "Pedido eliminado correctamente";
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                TempData["error"] = ex.Message;
                PedidosDTO pedidoDTO = service.ObtenerPorId(id);
                return View("Delete", pedidoDTO);
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servicio";
                return RedirectToAction("Index");
            }
            finally
            {
                CloseServicePedidos(service);
            }
        }

        public ActionResult Buscar(string busqueda)
        {
            var service = new PedidoService.PedidosServiceClient();
            try
            {
                List<PedidosDTO> listaPedidos = service.Buscar(busqueda).ToList();

                return View("Index", listaPedidos);
            }
            catch (FaultException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servicio";
                return RedirectToAction("Index");
            }
            finally
            {
                CloseServicePedidos(service);
            }
        }

        public ActionResult IrReporte()
        {
            var service = new PedidoService.PedidosServiceClient();
            try
            {
                List<PedidosDTO> listaPedidos = service.Obtener().ToList();

                return View("Reporte", listaPedidos);
            }
            catch (FaultException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servicio";
                return RedirectToAction("Index");
            }
            finally
            {
                CloseServicePedidos(service);
            }
        }

        public ActionResult Reporte(DateTime? fechaInicial, DateTime? fechaFinal)
        {
            if (!fechaInicial.HasValue || !fechaFinal.HasValue)
            {
                TempData["error"] =
                    "Debe proporcionar ambas fechas";

                return View("Reporte",
                    new List<PedidosDTO>());
            }

            var service = new PedidoService.PedidosServiceClient();
            try
            {
                List<PedidosDTO> listaPedidosDTO = service.Reporte(fechaInicial.Value, fechaFinal.Value).ToList();

                ViewBag.FechaInicial = fechaInicial?.ToString("yyyy-MM-dd") ?? "";
                ViewBag.FechaFinal = fechaFinal?.ToString("yyyy-MM-dd") ?? "";

                return View("Reporte", listaPedidosDTO);
            }
            catch (FaultException ex)
            {
                TempData["error"] = ex.Message;
                ViewBag.FechaInicial = fechaInicial?.ToString("yyyy-MM-dd") ?? "";
                ViewBag.FechaFinal = fechaFinal?.ToString("yyyy-MM-dd") ?? "";
                return View("Reporte", new List<PedidosDTO>());
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servicio";
                return RedirectToAction("Index");
            }
            finally
            {
                CloseServicePedidos(service);
            }
        }

        // Metodo auxiliar para cerrar servicio de Pedidos
        private void CloseServicePedidos(PedidoService.PedidosServiceClient service)
        {
            try
            {
                if (service.State == CommunicationState.Faulted)
                    service.Abort();
                else
                    service.Close();
            }
            catch
            {
                service.Abort();
            }
        }

        // Metodo auxiliar para cerrar servicios de Clientes
        private void CloseServiceClientes(ClienteService.ClientesServiceClient service)
        {
            try
            {
                if (service.State == CommunicationState.Faulted)
                    service.Abort();
                else
                    service.Close();
            }
            catch
            {
                service.Abort();
            }
        }
    }
}