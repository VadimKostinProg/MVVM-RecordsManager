using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public sealed class Record : EntityBase
    {
        [NotMapped]
        public TimeOnly Time
        {
            get { return new TimeOnly(DateTimeProperty.Hour, DateTimeProperty.Minute, DateTimeProperty.Second); }
            set
            {
                DateTimeProperty = new DateTime(DateTimeProperty.Year, DateTimeProperty.Month, DateTimeProperty.Day,
                    value.Hour, value.Minute, value.Second);
            }
        }

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
