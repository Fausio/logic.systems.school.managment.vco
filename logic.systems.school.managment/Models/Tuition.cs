﻿using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Tuition")]
    public class Tuition : Common
    {
        public SchoolLevel AssociatedLevel { get; set; }
        public SchoolLevel AssociatedLevelId { get; set; }
        public decimal MonthlyFee { get; set; }
    }
}
