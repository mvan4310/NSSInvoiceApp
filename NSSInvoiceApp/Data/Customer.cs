using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSSInvoiceApp.Data
{
    [Serializable]
    public class Customer
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        [Display(Name = "City/State")]
        public string CityState { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 5)]
        [Display(Name = "Zip")]
        public string Zip { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 5)]
        [Phone]
        public string Phone { get; set; }
    }
}
