﻿using System.ComponentModel.DataAnnotations.Schema;
namespace logic.systems.school.managment.Models
{
    [Table("PaymentTuition")]
    public class PaymentTuition : Payment
    {
        public virtual Tuition Tuition { get; set; }
        public int TuitionId { get; set; }

        public virtual TuitionInvoice TuitionInvoice { get; set; }
        public int TuitionInvoiceId { get; set; }
         
    }
}
