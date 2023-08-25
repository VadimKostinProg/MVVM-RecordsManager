using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime DateTimeProperty { get; set; }

        [NotMapped]
        public DateOnly Date 
        { 
            get { return new DateOnly(DateTimeProperty.Year, DateTimeProperty.Month, DateTimeProperty.Day); }
            set 
            { 
                DateTimeProperty = new DateTime(value.Year, value.Month, value.Day, 
                    DateTimeProperty.Hour, DateTimeProperty.Minute, DateTimeProperty.Second);
            }
        }

        public EntityBase()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
