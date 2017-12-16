using Animales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Animales.Controllers
{
    public class HomeController : Controller
    {
        PredioEntities cnx;
        
        public HomeController()
        {
            cnx = new PredioEntities();

        }
        public ActionResult Formulario()
        {
            return View();
        }
        public ActionResult Guardar(int Diio, string nombre, string sexo, string raza, int edad, string tipo, string rebaño)
        {

            Animales.Models.Animal animal = new Animales.Models.Animal
            {
                DIIO = Diio,
                Nombre = nombre,
                Sexo = sexo,
                Raza = raza,
                Edad = edad,
                Tipo = tipo,
                Rebaño = rebaño

            };

            cnx.Animals.Add(animal);
            cnx.SaveChanges();

            return View("Listar", ListadoAnimal());
        }
        public ActionResult Listar()
        {

            return View("Listar", ListadoAnimal());
        }
        public ActionResult Ficha(string nombre)
        {

            return View(BuscaAnimal(nombre));
        }

        private Animal BuscaAnimal(string nombre)
        {
            Animal nueva = new Animal();
            foreach (Animal animal in cnx.Animals.ToList())
            {
                if (animal.Nombre.Equals(nombre))
                {
                    nueva = animal;
                }
            }
            return nueva;
        }
        public ActionResult Visualizar(string nombre)
        {
            Animal nueva = BuscaAnimal(nombre);

            if (nueva != null)
            {
                return View("Ficha", nueva);
            }
            return View("Listado", cnx.Animals.ToList());
        }


        private List<Animales.Models.Animal> ListadoAnimal()
        {

            cnx.Database.Connection.Open();


            List<Animales.Models.Animal> auto = cnx.Animals.ToList();

            cnx.Database.Connection.Close();

            return auto;
        }

    }
}