using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace QiwiReports.Models
{
    public class LetterContext : DbContext
    {
        public DbSet<Letter> Letters { get; set; }
    }
}