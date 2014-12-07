using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Globalization;

namespace QiwiReports.Models
{

    public class Letter : IComparable, IEquatable<Letter>
    {
        public Letter(ArrayList spl)
        {
            
            this.txn_id = (String)spl[1];
            this.account = (String)spl[3];
            this.amount = Convert.ToDecimal(spl[5], CultureInfo.InvariantCulture);
            this.trm_id = (String)spl[7];
            this.trm_txn_id = (String)spl[9];
            this.from_amount = Convert.ToDecimal(spl[11], CultureInfo.InvariantCulture);
            this.account1 = (String)spl[13];
            DateTime ss = (DateTime)spl[14];
            this.dateReceive = ss;
            
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

        // Дата принятия почты
        public DateTime dateReceive { get; set; }

        // Обработано.
        public Boolean IsComplete { get; set; }

/*

        public int CompareTo(object obj)
        {
            Letter let_new = obj as Letter;
            if (let_new.txn_id == this.txn_id)
                return 0;
            if (let_new.txn_id != this.txn_id)
                return 1;
            else
                throw new NotImplementedException();
        }
 * */

        public int CompareTo(object obj)
        {
            Letter let = obj as Letter;
            if (let.txn_id == this.txn_id)
                return 0;
            else
                return 1;

        }

        public bool Equals(Letter obj)
        {
            if (obj == null)
                return false;
            else
                return obj.txn_id.Equals(this.txn_id);
        }

        public override bool Equals(object obj)
        {
            Letter let = obj as Letter;
            if (obj == null)
                return false;
            else
                return let.txn_id.Equals(this.txn_id);
        }


        public override int GetHashCode()
        {
            return this.txn_id.GetHashCode();
        }
    }

    
}