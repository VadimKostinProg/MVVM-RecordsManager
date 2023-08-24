using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public Guid Id { get; set; }

        public EntityBase()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
