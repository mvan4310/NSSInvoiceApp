using System;
using System.Collections.Generic;
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

    }
}
