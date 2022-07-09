using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CatalogoClientes.Dominio.Entidades;
using CatalogoClientes.Dominio.Repositorio;
using PagedList;

namespace CatalogoClientes.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IRepositorio<Cliente> _repositorio;
        private ClienteContexto db = new ClienteContexto();

        public ClienteController()
        {
            _repositorio = new ClienteRepositorio(new ClienteContexto());
        }

        //GET: Cliente
        public ActionResult Catalogo(int? pagina)
        {
            int numPagina = pagina ?? 1;
            int tamPagina = 1;
            return View(_repositorio.GetTodos().ToPagedList(numPagina, tamPagina));
        }

        //// GET: Cliente
        //public ActionResult Catalogo()
        //{
        //    return View(db.Clientes.ToList());
        //}

        // GET: Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.FirstOrDefault(c=>c.ClienteId == id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "ClienteId,Nome,Email,Endereco,Imagem,ImagemTipo")] 

                Cliente cliente,HttpPostedFileBase upload )  //HttpPostedFileBase é para o upload de arquivos
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0) //Verificação se há algo no arquivo
                {
                    var arqImagem = new Cliente
                    {
                        ImagemTipo = upload.ContentType  //Nome do tipo do arquivo
                    };
                    using (var reader = new BinaryReader(upload.InputStream)) //Leitor bin transforma o inputstream em bits
                    {
                        arqImagem.Imagem = reader.ReadBytes(upload.ContentLength);
                    }
                    cliente.Imagem = arqImagem.Imagem;
                    cliente.ImagemTipo = arqImagem.ImagemTipo;
                }

                db.Clientes.Add(cliente);
                db.SaveChanges();
                TempData["Mensagem"] = string.Format($"{cliente.Nome} incluído com sucesso!");
                return RedirectToAction("Catalogo");
            }

            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClienteId,Nome,Email,Endereco,Imagem,ImagemTipo")] Cliente cliente, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if(upload != null && upload.ContentLength > 0)
                {
                    var arqImagem = new Cliente
                    {
                        ImagemTipo = upload.ContentType
                    };
                    using(var reader = new BinaryReader(upload.InputStream))
                    {
                        arqImagem.Imagem = reader.ReadBytes(upload.ContentLength);
                    }
                    cliente.Imagem = arqImagem.Imagem;
                    cliente.ImagemTipo = arqImagem.ImagemTipo;
                }
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Mensagem"] = string.Format($"{cliente.Nome} atualizado com sucesso!");
                return RedirectToAction("Catalogo");
            }
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            TempData["Mensagem"] = string.Format($"{cliente.Nome} excluído com sucesso!");
            return RedirectToAction("Catalogo");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
