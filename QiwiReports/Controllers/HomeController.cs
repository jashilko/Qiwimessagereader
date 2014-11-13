using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QiwiReports.Models;
using Newtonsoft.Json;
using OpenPop.Pop3;
using OpenPop.Mime;
using OpenPop.Mime.Header;
using System.Globalization;


namespace QiwiReports.Controllers
{
    /*
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            // получаем из бд все объекты Book
           
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
            letter.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо, за покупку!";
        }

*/
    public class HomeController : Controller
    {
        // создаем контекст данных

        static IEnumerable<Letter> letters;
        static LetterContext db;

        private Boolean ChechExists(String[] split)
        {
            for (int i = 1; i < db.Letters.Count() - 1; i++)
            {
                if (db.Letters.ElementAt(i).txn_id == split[1]){
                    return false;
                }
                    
            }
                return true;
        }
        /*
        static HomeController()
        {
            books.Add(new Book { Id = 1, Name = "Война и мир", Author = "Л. Толстой", Year = 1863, Price = 220 });
            books.Add(new Book { Id = 2, Name = "Отцы и дети", Author = "И. Тургенев", Year = 1862, Price = 195 });
            books.Add(new Book { Id = 3, Name = "Чайка", Author = "А. Чехов", Year = 1895, Price = 158 });
            books.Add(new Book { Id = 4, Name = "Подросток", Author = "Ф. Достоевский", Year = 1875, Price = 210 });
        }
        */

        public ActionResult Index()
        {
            db = new LetterContext();
            CheckData(db);
            letters = db.Letters;
            return View();
        }

        private void CheckData(LetterContext db)
        {
            Pop3Client pop3Client;
            pop3Client = new Pop3Client();
            pop3Client.Connect("pop.mail.ru", 995, true);
            pop3Client.Authenticate("spkristall@mail.ru", "kristall77", OpenPop.Pop3.AuthenticationMethod.UsernameAndPassword);

            int count = pop3Client.GetMessageCount();
            //DataTable dtMessages = new DataTable();
            String mesBody;
            
            for (int i = count; i >= 1; i--)
            {
                MessageHeader headers = pop3Client.GetMessageHeaders(i);
                DateTime ndt = DateTime.Parse(headers.Date.Substring(0, 25));
                
                if (ndt > DateTime.Today.AddDays(-1))
                {

                    Message message = pop3Client.GetMessage(i);
                    MessagePart messagePart = message.FindFirstPlainTextVersion();
                    if (messagePart != null)
                    {
                        mesBody = messagePart.BodyEncoding.GetString(messagePart.Body);
                        string[] stringSeparators = new string[] { "\r\n", "= " };
                        string[] split = mesBody.Split(stringSeparators, StringSplitOptions.None);
                        // Создаем новый класс письма. 
                        Letter nlet = new Letter(split);
                        if (db.Letters.ToList().Contains(nlet) == false)
                        {
                            // Проверяем, не содержится ли он уже
                            db.Letters.Add(nlet);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        public string GetData()
        {
            return JsonConvert.SerializeObject(letters);
            //return null;
        }
    }


}
