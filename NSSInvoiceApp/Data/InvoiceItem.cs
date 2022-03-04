using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSSInvoiceApp.Data
{
    [Serializable]
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }

        public int InvoiceId { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; } = 1;

        [Required]
        public double Price { get; set; } = 0;

        [Required]
        public bool IsTax { get; set; } = false;


        public double TotalPrice { get; set; }
    }
}
