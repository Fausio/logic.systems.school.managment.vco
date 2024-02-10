using System.ComponentModel.DataAnnotations.Schema;
using static ClosedXML.Excel.XLPredefinedFormat;

namespace logic.systems.school.managment.Models
{
    // avaliacoes
    [Table("Assessment")]
    public class Assessment : Common
    {


        public Assessment()
        {

            Grades.AddRange(
                   new List<Grade>()
                    {
                     new Grade()
                       {
                        Type = Grade.GradeType_ACS,
                        Number = 1
                       },
                     new Grade()
                       {
                        Type = Grade.GradeType_ACS,
                        Number = 2
                       },
                     new Grade()
                       {
                        Type = Grade.GradeType_ACS,
                        Number = 3
                       },
                     new Grade()
                       {
                        Type = Grade.GradeType_AP,
                        Number = 1
                       },
                    }
           );


        }


        public SimpleEntity? Subject { get; set; }
        public int? SubjectId { get; set; }

        public Quarter? Quarter { get; set; }
        public int? QuarterId { get; set; }
        public List<Grade> Grades { get; set; } = new List<Grade>();





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
