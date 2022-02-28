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

        [Required]
        public int CustomerId { get; set; }
        public string Customer { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public double TotalAmount { get; set; }
        public bool ReceivedPayment { get; set; }
        public DateTime DateReceived { get; set; }
    }
}
