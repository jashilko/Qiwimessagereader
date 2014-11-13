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
    public class LettersDbInitializer : DropCreateDatabaseIfModelChanges<LetterContext>
    {


        protected override void Seed(LetterContext db)
        {
            

        }
    }
}