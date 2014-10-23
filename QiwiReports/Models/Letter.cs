using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QiwiReports.Models
{
    public class Letter
    {
        public int Id { get; set; }

        public String txn_id { get; set; }

        // Аккаунт. 
        public String account { get; set; }

        // Сумма
        public Decimal amount { get; set; }

        public String trm_id { get; set; }

        public String trm_txn_id { get; set; }

        // Сумма к уплате. 
        public Decimal from_amount { get; set; }

        // Телефон. 
        public String account1 { get; set; }

 
    }
}