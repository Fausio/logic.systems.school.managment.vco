using DocumentFormat.OpenXml.InkML;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using Microsoft.EntityFrameworkCore;

namespace logic.systems.school.managment.Services
{
    public class DocumentService : Idocument
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>());
        public async Task<List<PaymentTuitionListReportDTO>> GetPaymentTuitionList(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var sql = @"
                                SELECT	s.[name] StudendName,
		                                class.[description] StudentClassLevel,
		                                Cast(t.MonthNumber as nvarchar(100))+' - '+ t.[MonthName] +' - '+  Cast(t.[Year] as nvarchar(100)) MonthPaid,
		
		                                p.MonthlyFeeWithoutVat	,
		                                p.VatOfMonthlyFee		,
		                                p.MonthlyFeeWithVat 

                                FROM	Tuition t
                                JOIN	Student s ON s.id = t.StudentId
                                JOIN	SimpleEntity class ON class.id = s.CurrentSchoolLevelId
                                JOIN	Payment p ON   p.TuitionId = t.id AND t.paid =1    
                                WHERE	1 = 1
                                AND		P.PaymentDate BETWEEN @StartDate AND  @EndDate
                        "
                ;
                var result = from t in db.Tuitions
                             join s in db.Students on t.StudentId equals s.Id
                             join classLevel in db.SimpleEntitys on s.CurrentSchoolLevelId equals classLevel.Id
                             join p in db.Payments on t.Id equals p.TuitionId
                             where t.Paid == true && p.PaymentDate >= startDate && p.PaymentDate <= endDate
                             select new PaymentTuitionListReportDTO()
                             {
                                 StudendName = s.Name,
                                 StudentClassLevel = classLevel.Description,
                                 MonthPaid = t.MonthNumber + " - " + t.MonthName + " - " + t.Year,
                                 MonthlyFeeWithoutVat = p.MonthlyFeeWithoutVat,
                                 VatOfMonthlyFee = p.VatOfMonthlyFee,
                                 MonthlyFeeWithVat = p.MonthlyFeeWithVat
                             };

             return   result.ToList(); 
            }
            catch (Exception x)
            {

                throw x;
            }
        }
    }
}
