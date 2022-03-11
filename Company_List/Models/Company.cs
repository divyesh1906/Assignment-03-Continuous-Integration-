using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Company_List.Models
{
    public class Company
    {
        /// <summary>
        /// Company Variable are set and get here
        /// </summary>
        [Key]
        public int CompanyID { get; set; }
        [Required(ErrorMessage = "Please enter a CompanyName.")]
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please enter a Address.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter a Ticker Symbol.")]
        public string TickerSymbol { get; set; }


    }
}
