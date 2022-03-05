using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSSInvoiceApp.Data
{
    public static class Extensions
    {
        public static Invoice CloneInvoice(this Invoice existing)
        {
            Invoice _invoice = new()
            {
                TotalAmount = existing.TotalAmount,
                Customer = existing.Customer,
                CustomerId = existing.CustomerId,
                Date = existing.Date,
                DateReceived = existing.DateReceived,
                DueDate = existing.DueDate,
                Id = existing.Id,
                InvoiceNumber = existing.InvoiceNumber,
                ReceivedPayment = existing.ReceivedPayment
            };

            return _invoice;
        }

        public static InvoiceItem CloneInvoiceItem(this InvoiceItem existing)
        {
            InvoiceItem _invoiceItem = new()
            {
                Description = existing.Description,
                InvoiceId = existing.InvoiceId,
                IsTax = existing.IsTax,
                Price = existing.Price,
                Quantity = existing.Quantity,
                TotalPrice = existing.TotalPrice,
                Id = existing.Id
            };

            return _invoiceItem;
        }

        public static Customer CloneCustomer(this Customer existing)
        {
            Customer _customer = new()
            {
                Address = existing.Address,
                CityState = existing.CityState,
                Email = existing.Email,
                Date = existing.Date,
                Name = existing.Name,
                Phone = existing.Phone,
                Id = existing.Id,
                Zip = existing.Zip
            };

            return _customer;
        }

        public static Expense CloneExpense(this Expense existing)
        {
            Expense _expense = new()
            {
                TotalAmount = existing.TotalAmount,
                Date = existing.Date,
                Name = existing.Name,
                Id = existing.Id
            };

            return _expense;
        }
    }
}
