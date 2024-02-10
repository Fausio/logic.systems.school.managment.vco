﻿using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    [Table("Enrollment")]
    public class Enrollment : Common
    {
        public virtual SimpleEntity SchoolLevel { get; set; } 
        public int SchoolLevelId { get; set; }
        public virtual EnrollmentPayment PaymentEnrollment { get; set; }
        public int PaymentEnrollmentId { get; set; }

        public virtual Student Student { get; set; }
        public int StudentId { get; set; }

        public List<Tuition> Tuitions { get; set; } = new List<Tuition>();
        public List<EnrollmentItem> EnrollmentItems { get; set; } = new List<EnrollmentItem>();

        public List<Quarter> Quarter { get; set; }  = new List<Quarter> { };

        public int EnrollmentYear { get; set; }
        public int SchoolClassRoomId { get; set; }
    }
}
