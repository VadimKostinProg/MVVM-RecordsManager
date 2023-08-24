using System;

namespace RecordsManager.Models
{
    public class RecordDateModel
    {
        public string Date { get; set; } = null!;
        public string TimeJoined { get; set; } = null!;
        public decimal TotalPrice { get; set; }
    }
}
