using CatalogoClientes.Dominio.Entidades;
using CatalogoClientes.Dominio.Repositorio;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatalogoClientes.Web.Controllers
{
    public class ClienteController : Controller
    {
        private IRepositorio<Cliente> _repositorioCliente;

        public ClienteController()
        {
            _repositorioCliente = new ClienteRepositorio(new ClienteContexto());
        }
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Catalogo(int? pagina)
        {
            int tamPagina = 1;
            int numPagina = pagina ?? 1;
            return View(_repositorioCliente.GetTodos().ToPagedList(numPagina, tamPagina));
        }
    }
}