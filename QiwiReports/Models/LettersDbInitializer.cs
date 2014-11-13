using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using OpenPop.Pop3;
using OpenPop.Mime;
using System.Data;


namespace QiwiReports.Models
{
    public class LettersDbInitializer : DropCreateDatabaseAlways<LetterContext>
    {


        protected override void Seed(LetterContext db)
        {
            
            Pop3Client pop3Client;
            pop3Client = new Pop3Client();
            pop3Client.Connect("pop.mail.ru", 995, true);
            pop3Client.Authenticate("spkristall@mail.ru", "kristall77", OpenPop.Pop3.AuthenticationMethod.UsernameAndPassword);

            int count = pop3Client.GetMessageCount();
            DataTable dtMessages = new DataTable();
            String mesBody;
            int counter = 0;
            for (int i = count; i >= 1; i--)
            {
                Message message = pop3Client.GetMessage(i);
                MessagePart messagePart = message.FindFirstPlainTextVersion();
                mesBody = messagePart.BodyEncoding.GetString(messagePart.Body);

                string[] stringSeparators = new string[] { "\r\n", "= " };
                string[] split = mesBody.Split(stringSeparators, StringSplitOptions.None);
                db.Letters.Add(new Letter(split));
                base.Seed(db);
                
                counter++;
                if (counter > 4)
                {
                    break;
                }
                
             
            }
            
        
            
            
            //db.Letters.Add(new Letter { txn_id = "1324", account = "123", amount = 220, trm_id = "444", trm_txn_id = "555", account1 = "200", from_amount = 232 });
            //db.Letters.Add(new Letter { txn_id = "32333", account = "12313", amount = 220, trm_id = "4444", trm_txn_id = "5555", account1 = "2600", from_amount = 232 });
            //db.Letters.Add(new Letter { txn_id = "132224", account = "1253", amount = 2720, trm_id = "4844", trm_txn_id = "5955", account1 = "2000", from_amount = 232 });

        }
    }
}