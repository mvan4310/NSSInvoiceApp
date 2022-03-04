using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSSInvoiceApp.Data
{
    public class UserData
    {
        public List<Invoice> Invoices { get; set; } = new();
        public List<InvoiceItem> InvoiceItems { get; set; } = new();
        public List<Expense> Expenses { get; set; } = new();
        public List<Customer> Customers { get; set; } = new();
        public string LastUpdated { get; set; } = DateTime.MinValue.ToString();
        public bool EnableDarkMode { get; set; } = true;

        [Required]
        [StringLength(120, MinimumLength = 5)]
        [Display(Name = "Name")]
        public string CompanyName { get; set; } = "";

        [Required]
        [StringLength(120, MinimumLength = 5)]
        [Display(Name = "Address")]
        public string CompanyAddress { get; set; } = "";

        [Required]
        [StringLength(120, MinimumLength = 5)]
        [Display(Name = "City/State")]
        public string CompanyCityState { get; set; } = "";

        [Required]
        [StringLength(10, MinimumLength = 5)]
        [Display(Name = "Zip")]
        public string CompanyZip { get; set; } = "";

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string CompanyEmail { get; set; } = "";

        [Phone]
        [Display(Name = "Phone")]
        public string CompanyPhone { get; set; } = "";
    }
}
