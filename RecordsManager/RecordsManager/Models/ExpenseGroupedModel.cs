using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsManager.Models
{
    public class ExpenseGroupedModel
    {
        public string Date { get; set; } = null!; 
        public string PurposeGrouped { get; set; } = null!;
        public decimal TotalPrice { get; set; }
    }
}
