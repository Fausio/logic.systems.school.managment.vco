using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    public class AppUser : IdentityUser
    {
        [NotMapped]
        public string RoleName { get; set; }

        public List<ProfessorConfig> ProfessorConfigs { get; set; } = new List<ProfessorConfig>();
    }
}
