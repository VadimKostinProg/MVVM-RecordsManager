using System;

namespace RecordsManager.Models
{
    public class RecordGroupedModel
    {
        public string Date { get; set; } = null!;
        public string TimeGrouped { get; set; } = null!;
        public decimal TotalPrice { get; set; }
    }
}
