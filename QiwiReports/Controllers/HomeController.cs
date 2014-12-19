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
using System.Collections;
using Newtonsoft.Json.Converters;
using System.Data.Entity;




namespace QiwiReports.Controllers
{
    
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

        [Authorize]
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

            // Получаем максимальную дату записи в БД. 
            DateTime MaxTime = db.Letters.Max(w => w.dateReceive);

            int count = pop3Client.GetMessageCount();
            //DataTable dtMessages = new DataTable();
            String mesBody;
            
            for (int i = count; i > 0; i--)
            {
                MessageHeader headers = pop3Client.GetMessageHeaders(i);
                DateTime datePost = DateTime.Parse(headers.DateSent.ToString());
                datePost = datePost.AddHours(3);

                if (datePost > MaxTime)
                {
                    Message message = pop3Client.GetMessage(i);
                    MessagePart messagePart = message.FindFirstPlainTextVersion();
                    if (messagePart != null)
                    {

                        mesBody = messagePart.BodyEncoding.GetString(messagePart.Body);
                        string[] stringSeparators = new string[] { "\r\n", "= " };
                        string[] split = (mesBody.Split(stringSeparators, StringSplitOptions.None));
                        ArrayList ss = new ArrayList(split);
                        ss.Add(datePost);

                        // Создаем новый класс письма. 
                        Letter nlet = new Letter(ss);
                        //if (db.Letters.Where(x => x.txn_id.ToString() == (String)ss[1]).ToList().Count == 0)
                        if (db.Letters.ToList().Contains(nlet) == false)
                        {
                            // Проверяем, не содержится ли он уже
                            db.Letters.Add(nlet);
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    break;
                }
            }
        }


        [HttpPost]
        public ActionResult Edit(List<String> name)
        {
            Newtonsoft.Json.Linq.JArray arr = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(name[0]);
            for (int i = 0; i < arr.Count; i++)
            {
               int qq = arr.Value<int>(i);
               Letter oldlet = db.Letters.Find(qq);
               db.Entry(oldlet).State = EntityState.Modified;
               oldlet.IsComplete = true;
               UpdateModel(oldlet);
               db.SaveChanges();
            }

             return null;
             
         }
      
        public string GetData(Boolean _search)
        {
            return JsonConvert.SerializeObject(letters);
        }
    }


}
