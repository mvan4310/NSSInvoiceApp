using System;
using System.Collections.Generic;
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

        public string CompanyName { get; set; } = "Test Company";
        public string CompanyAddress { get; set; } = "";
        public string CompanyCityState { get; set; } = "";
        public string CompanyZip { get; set; } = "";
        public string CompanyEmail { get; set; } = "";
        public string CompanyPhone { get; set; } = "";
    }
}
