using System.ComponentModel.DataAnnotations.Schema;

namespace logic.systems.school.managment.Models
{
    // avaliacoes
    [Table("Assessment")]
    public class Assessment : Common
    {
        public SimpleEntity? Subject { get; set; }
        public int? SubjectId { get; set; }

        public Quarter? Quarter { get; set; }
        public int? QuarterId { get; set; }

      // public decimal ACSAverage { get; set; }
      // public decimal AssessmentAverage { get; set; }
      //
      // //Notas
      // public Grade[] ACSs { get; set; } = new Grade[2];
      // public Grade AP { get; set; } = new Grade();
      //
      // public void GetACSAverage()
      // {
      //     decimal TotalACS = 0;
      //     foreach (var item in ACSs)
      //     {
      //         TotalACS = TotalACS + item.Value;
      //     }
      //
      //     if (TotalACS == 0)
      //     {
      //         ACSAverage = 0;
      //     }
      //     else
      //     {
      //         ACSAverage = TotalACS / 3;
      //     }
      // }
      //
      // public void GetQuarterAverage()
      // {
      //     GetACSAverage();
      //
      //
      //     if (AP.Value > 0 )
      //     {
      //         var result = (2 * ACSAverage + AP.Value) / 3;
      //     }
      //
      //     
      // }


    }
}
