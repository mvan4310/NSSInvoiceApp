using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSSInvoiceApp.Data
{
    [Serializable]
    public class Expense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        [Display(Name = "Description")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public double TotalAmount { get; set; }
    }
}
