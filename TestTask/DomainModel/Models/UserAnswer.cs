using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel.Models
{
    public class UserAnswer : EntityBase
    {
        public string Name { get; set; }
        public virtual ICollection<UserAnswerSectors> UserSectors { get; set; } = new List<UserAnswerSectors>();
        public bool AgreedToTerms { get; set; }

        [NotMapped]
        public short[] UserSectorCodes { get; set; }
    }
}