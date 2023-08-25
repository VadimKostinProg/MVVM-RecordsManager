using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsManager.Models
{
    public class ExpenseModel
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public string Purpose { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
