using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QiwiReports.Models
{
    public class Letter
    {
        public Letter(String[] spl)
        {
            this.txn_id = spl[1];
            this.account = spl[3];
            this.amount = Convert.ToDecimal(spl[5]);
            this.trm_id = spl[7];
            this.trm_txn_id = spl[9];
            this.from_amount = Convert.ToDecimal(spl[11]);
            this.account1 = spl[13];
        }

        public Letter()
        {
        }


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