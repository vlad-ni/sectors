using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainModel.Models
{
    public class UserAnswerSectors : EntityBase
    {
        [ForeignKey(nameof(UserAnswer))]
        public Guid UserId { get; set; }
        public virtual UserAnswer UserAnswer { get; set; }

        [ForeignKey(nameof(Sector))]
        public Guid SectorId { get; set; }
        public virtual Sector Sector { get; set; }
    }
}
