using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AnimalShelter.Models;

namespace AnimalShelter.Controllers
{
    public class AnimalController : Controller
    {
        [HttpGet("/animals")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("/animals/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpGet("/animals/show")]
        public ActionResult Show()
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            
            List<AnimalType> typeList = AnimalType.GetTypes();
            List<AnimalClass> animalList = AnimalClass.GetAll();

            model.Add("types" , typeList);
            model.Add("animals" , animalList);
            return View(model);
        }

        [HttpGet("/animals/show/{id}")]
        public ActionResult Animal(int id)
        {
            List<AnimalClass> animalList = AnimalClass.GetAll();
            foreach(AnimalClass animal in animalList)
            {
                if(animal.GetId() == id)
                {
                    return View(animal);
                }
            }
            return View("Show");
        }

        [HttpPost("/animals/show")]
        public ActionResult Create(string animalName, string animalType, string animalSex, string animalDate, int animalAge)
        {
            AnimalClass animal = new AnimalClass(animalName, animalType, animalSex, animalDate, animalAge);
            animal.Save();
            return View("New");
        }

        [HttpPost("/animals/show/type")]
        public ActionResult AnimalsByType(string type)
        {
            List<AnimalClass> animalList = AnimalClass.GetAll();
            List<AnimalClass> typeList = new List<AnimalClass>() { };
            foreach(AnimalClass animal in animalList)
            {
                if (animal.GetType() == type)
                {
                    typeList.Add(animal);
                }
            }
            return View(typeList);
        }
    }  
}
