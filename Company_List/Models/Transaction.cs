using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Company_List.Models;

namespace Company_List.Models
{
    public class Transaction
    {
        /// <summary>
        /// Transaction file varible are set and get here
        /// 
        /// </summary>
        [Key]
        public int TransactionId { get; set; }

        [Required(ErrorMessage = "Please enter a quantity.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be in  positive integer.")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "Please Enter Share price in Interger")]
        [Range(1.0, double.MaxValue, ErrorMessage = "Price Must be Greater than Zero;")]
        public double SharePrice { get; set; }

        public double? GetGrossValue()
        {
            return this.SharePrice * this.Quantity;
        }

        public double? GetNetValue()
        {
            if (this.TransactionType.TransactionName == "Buy")
                return this.GetGrossValue() + this.TransactionType.CommissionFee;
            else
                return this.GetGrossValue() - this.TransactionType.CommissionFee;
        }


        [ForeignKey("TransactionTypeId")]
        public string TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }

        [ForeignKey("CompanyID")]
        public int CompanyID { get; set; }

        public Company Company { get; set; }
        


    }
}
