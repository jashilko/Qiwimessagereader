using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QiwiReports.Models;

namespace QiwiReports.Controllers
{
    public class HomeController : Controller
    {
        // создаем контекст данных
         LetterContext db = new LetterContext();

        public ActionResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Letter> letters = db.Letters;
            // передаем все полученный объекты в динамическое свойство Books в ViewBag
            ViewBag.Letters = letters;
            // возвращаем представление
            return View();
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.LetterId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Letter letter)
        {
            /*
            letter.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
           */
            return "Спасибо, за покупку!";
             
        }

    }
}
