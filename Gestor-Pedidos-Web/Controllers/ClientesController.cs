using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace Gestor_Pedidos_Web.Controllers
{
    public class ClientesController : Controller
    {
        public ActionResult Index()
        {
            var service = new ClienteService.ClientesServiceClient();
            try
            {
                List<ClientesDTO> listaClientes = service.Obtener().ToList();
                return View("Index", listaClientes);
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
                CloseServiceClientes(service);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            ClientesDTO cliente = new ClientesDTO();
            return View("Create", cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(ClientesDTO clienteDTO)
        {
            var service = new ClienteService.ClientesServiceClient();
            try
            {
                service.Agregar(clienteDTO);
                TempData["success"] = "Cliente agregado correctamente";
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                TempData["error"] = ex.Message;
                return View("Create", clienteDTO);
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servicio";
                return RedirectToAction("Index");
            }
            finally
            {
                CloseServiceClientes(service);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var service = new ClienteService.ClientesServiceClient();
            try
            {
                ClientesDTO clienteDTO = service.ObtenerPorID(id);
                return View("Edit", clienteDTO);
            }
            catch (FaultException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servico";
                return RedirectToAction("Index");
            }
            finally
            {
                CloseServiceClientes(service);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(ClientesDTO clienteDTO)
        {
            var service = new ClienteService.ClientesServiceClient();
            try
            {
                service.Editar(clienteDTO);
                TempData["success"] = "Cliente editado correctamente";
                return RedirectToAction("Index");
            }
            catch (FaultException ex)
            {
                TempData["error"] = ex.Message;
                return View("Edit", clienteDTO);
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servicio";
                return RedirectToAction("Index");
            }
            finally
            {
                CloseServiceClientes(service);
            }
        }

        public ActionResult Buscar(string busqueda)
        {
            var service = new ClienteService.ClientesServiceClient();
            try
            {
                List<ClientesDTO> listaClientes = service.Buscar(busqueda).ToList();
                return View("Index", listaClientes);
            }
            catch (FaultException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un erro al comunicarse con el servicio";
                return RedirectToAction("Index");
            }
            finally
            {
                CloseServiceClientes(service);
            }
        }

        // Metodo auxiliar para cerrar el servicio de Clientes
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