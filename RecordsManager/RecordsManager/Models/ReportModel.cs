using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsManager.Models
{
    public class ReportModel
    {
        public decimal TotalIncomes { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal Profit
        {
            get { return TotalIncomes - TotalExpenses; }
        }
    }
}
