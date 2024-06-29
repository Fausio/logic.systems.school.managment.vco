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

        }

        public Assessment(bool instanceWithGrade)
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




        public decimal GetACSAverage()
        {
            decimal totalACS = Grades.Where(x => x.Type == Grade.GradeType_ACS).Sum(x => x.Value != null ? x.Value.Value : 0);
            var result = Math.Round((totalACS == 0 ? 0 : totalACS / 3), 2);
            return result;
        }

        public decimal GetQuarterAverage()
        {
            GetACSAverage();

            var AP = Grades.FirstOrDefault(x => x.Type == Grade.GradeType_AP);

            var Numerator = 2 * GetACSAverage() + (AP?.Value ?? 0);

            var result = Math.Round((Numerator == 0 ? 0 : Numerator / 3), 2);

            return result;
        }



    }
}
