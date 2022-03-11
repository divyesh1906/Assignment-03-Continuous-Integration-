using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Company_List.Models
{
    /// <summary>
    /// Properties of Transactiontype
    /// </summary>
    public class TransactionType
    {
        
        public string TransactionTypeId { get; set; }
        
        public string TransactionName { get; set; }

        public double? CommissionFee { get; set; }




    }
}
