using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainModel.Models
{
    public class Sector : EntityBase
    {
        public short Code { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(ParentSector))]
        public Guid? ParentId { get; set; }
        public virtual Sector ParentSector { get; set; }
    }
}
