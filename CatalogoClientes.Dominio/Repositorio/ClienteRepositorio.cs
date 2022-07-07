using CatalogoClientes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoClientes.Dominio.Repositorio
{
    public class ClienteRepositorio : IRepositorio<Cliente>
    {
        private readonly ClienteContexto _contexto;

        public ClienteRepositorio(ClienteContexto contexto)
        {
            _contexto = contexto;
        }
        public IEnumerable<Cliente> GetTodos()
        {
            return _contexto.Clientes.ToList();
        }
    }
}
