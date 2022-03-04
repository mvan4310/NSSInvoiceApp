using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSSInvoiceApp.Data
{
    [Serializable]
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int InvoiceNumber { get; set; }

        [Required]
        public DateTime Date { get; set; }

        
        public int CustomerId { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 1)]
        [Display(Name = "Customer")]
        public string Customer { get; set; }


        [Required]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        public double TotalAmount { get; set; }
        public bool ReceivedPayment { get; set; }
        public DateTime DateReceived { get; set; }
    }
}
