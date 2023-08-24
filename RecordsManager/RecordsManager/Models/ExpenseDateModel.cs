using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsManager.Models
{
    public class ExpenseDateModel
    {
        public string Date { get; set; } = null!; 
        public string PurposeJoined { get; set; } = null!;
        public decimal TotalPrice { get; set; }
    }
}
