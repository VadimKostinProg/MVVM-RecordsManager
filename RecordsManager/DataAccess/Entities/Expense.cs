using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public sealed class Expense : EntityBase
    {
        [Required]
        public DateTime ExpenseDateTime { get; set; }

        [Required]
        public string Purpose { get; set; } = null!;

        [Required]
        [Range(0, 1e6)]
        public decimal Price { get; set; }
    }
}
