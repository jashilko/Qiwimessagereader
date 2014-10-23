using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace QiwiReports.Models
{
    public class LettersDbInitializer : DropCreateDatabaseAlways<LetterContext>
    {
        
        protected override void Seed(LetterContext db)
        {
            
            db.Letters.Add(new Letter { txn_id = "1324", account = "123", amount = 220, trm_id = "444", trm_txn_id = "555", account1 = "200", from_amount = 232 });
            db.Letters.Add(new Letter { txn_id = "32333", account = "12313", amount = 220, trm_id = "4444", trm_txn_id = "5555", account1 = "2600", from_amount = 232 });
            db.Letters.Add(new Letter { txn_id = "132224", account = "1253", amount = 2720, trm_id = "4844", trm_txn_id = "5955", account1 = "2000", from_amount = 232 });

            base.Seed(db);
        }




    }
}