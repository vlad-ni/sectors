using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class UserAnswersViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [Display(Name = "Sectors")]
        public short[] UserSectors { get; set; }

        [Required]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You should agree to terms")]
        [Display(Name = "Agree to terms")]
        public bool AgreedToTerms { get; set; }

        public virtual List<Sector> AllSectors { get; set; }
    }
}
