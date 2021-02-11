using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sisteminha.Controllers
{
    public class HomeController : Controller
    {
        private readonly SisteminhaEntities db;
        public HomeController()
        {
            db = new SisteminhaEntities();

        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string Senha)
        {
            //string hash = Utis.sha256(senha);
            try
            {
                var cadastro = db.cadastro.Where(x => x.email == email && x.senha == Senha).FirstOrDefault();
                cadastro.senha = cadastro.senha == null ? "" : cadastro.senha;
                if (cadastro.senha != string.Empty)
                {

                    return RedirectToAction("Menu");
                }

                else
                    return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult cadastrousers()
        {
            return View();
        }

        public ActionResult Menu()
        {
            return View();
        }


        [HttpPost]
        public ActionResult cadastrousers(string nome, string sobrenome, string datanascimento, string fone, string email, string sexo, string senha)
        {
            cadastro Novo = new cadastro();
            Novo.nome = nome;
            Novo.sobrenome = sobrenome;
            Novo.dataNasc = Convert.ToDateTime(datanascimento);
            Novo.telefone = fone;
            Novo.email = email;
            Novo.sexo = sexo;
            Novo.senha = senha;
            db.cadastro.Add(Novo);
            db.SaveChanges();


            return RedirectToAction("About");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}