using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsManager.Models
{
    public class RecordModel
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string Name { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Procedure { get; set; }
        public decimal Price { get; set; }
    }
}
