using TesteInfortronics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Exercicio1.ServiceCorreios;

namespace TesteInfortronics.Controllers
{
    public class HomeController : Controller
    {
        public static List<ModelExercicio1> numerosDigitados = new List<ModelExercicio1>();
        
       
        public ActionResult Index()
        {
            return View(numerosDigitados);
        }

        public ActionResult AdicionaNumeroView()
        {  
            return View("AdicionaNumero");
        }
              

        [HttpPost]
        public ActionResult AdicionaNumero(ModelExercicio1 model)
        {
            if(model.numero != null)
                numerosDigitados.Add(model);

            numerosDigitados = numerosDigitados.OrderBy(m => m.numero).ToList();
            return View("Index",numerosDigitados);
        }

        public ActionResult SalvaListaJson()
        {
            string jsonString = JsonConvert.SerializeObject(numerosDigitados);
            System.IO.File.WriteAllText(@"C:\objeto.json", jsonString);
            return View(numerosDigitados);
        }

        public ActionResult Correios()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdicionaCEP(ModelExercicio3 model)
        {
            using (var ws = new AtendeClienteClient())
            {
                var resposta = ws.consultaCEP(model.cep);
                model.endereco = resposta.end;
                model.bairro = resposta.bairro;
                model.cidade = resposta.cidade;
                model.UF = resposta.uf;
            }
            return View("Correios",model);
        }

        public ActionResult ValorPerfeito()
        {
            return View(new ModelExercicio4());
        }

        [HttpPost]
        public ActionResult VerificaNumeroPerfeito(ModelExercicio4 model)
        {
            int numero = model.numero;
            List<int> divisores = new List<int>();
            int soma = 0;

            for (int x = 1; x < numero; x++)
            {
                if (numero % x == 0)
                    divisores.Add(x);
            }

            if (divisores.Count > 0) 
            {  
                for (int x = 0; x < divisores.Count; x++)
                {
                    soma += divisores[x];
                }
            }

            if (soma == numero)
                model.valorPerfeito = true;
            else
                model.valorPerfeito = false;
            

            return View("ValorPerfeito", model);
        }

        public ActionResult Tabuada()
        {
            return View(new ModelExercicio5());
        }

        [HttpPost]
        public ActionResult Tabuada(ModelExercicio5 model)
        { 
            return View("Tabuada", model);
        }

    }
}