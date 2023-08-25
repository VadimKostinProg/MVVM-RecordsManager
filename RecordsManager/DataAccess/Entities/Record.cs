using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public sealed class Record : EntityBase
    {
        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly Time { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? Procedure { get; set; }

        [Required]
        [Range(0, 1e6)]
        public decimal Price { get; set; }
    }
}
