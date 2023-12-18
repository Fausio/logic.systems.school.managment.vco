using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;

namespace logic.systems.school.managment.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private Idocument _DocumentService;
        private IApp _AppService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DocumentController(Idocument DocumentService,
                                  IWebHostEnvironment hostingEnvironment,
                                  IApp appService,
                                  UserManager<IdentityUser> userManager)
        {
            this._DocumentService = DocumentService;
            this._hostingEnvironment = hostingEnvironment;
            this._AppService = appService;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PaymentTuitionList()
        {
            return View(new ReportDataFilterDTO());
        }

        [HttpPost]
        public async Task<IActionResult> PaymentTuitionList(ReportDataFilterDTO filters)
        {
            return await PaymentTuitionListMethod(filters, "Relatório de Fecho de contas por datas");
        }


        public async Task<IActionResult> EnrollmentInvoice(int id)
        {
            var result = await _DocumentService.GetEnrollmentInvoiceByEnrollId(id);


            string relativePath = "template/invoice.xlsx";
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, relativePath);

            if (System.IO.File.Exists(filePath))
            {
                using var workbook = new XLWorkbook(filePath);
                var worksheet = workbook.Worksheets.Worksheet(1);

                worksheet.Cell("E4").Value = DateTime.Now.ToString("dd/MM/yyyy");
                worksheet.Cell("E5").Value = result.Id;
                worksheet.Cell("E6").Value = result.Enrollment.StudentId;
                worksheet.Cell("E7").Value = "Recibo de Matricula";

                worksheet.Cell("C8").Value = result.Enrollment.Student.Name;

                worksheet.Cell("B11").Value = "#";
                worksheet.Cell("C11").Value = await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolClassRoomId);
                worksheet.Cell("D11").Value = result.Enrollment.EnrollmentYear;
                worksheet.Cell("E11").Value = result.Enrollment.PaymentEnrollment.PaymentWithoutVat + "MT";

                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser != null)
                {
                    worksheet.Cell("E13").Value = "Emitido por: " + currentUser.UserName;
                } 

                using var stream = new MemoryStream();

                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);

                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Recibo.xlsx");
            }
            else
            {
                throw new Exception("path not foun");
            }



        }


        public async Task<IActionResult> PaymentTuitionListDaily()
        {
            var today = DateTime.Now;

            return await PaymentTuitionListMethod(new ReportDataFilterDTO()
            {
                StartDate = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0),
                EndDate = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59)
            }, "Relatório de Fecho de contas diário");
        }


        private async Task<FileContentResult> PaymentTuitionListMethod(ReportDataFilterDTO filters, string Name = "")
        {
            var results = await _DocumentService.GetPaymentTuitionList(filters.StartDate, filters.EndDate);


            using var workBook = new XLWorkbook();
            var worksheet = workBook.Worksheets.Add("Fecho de contas");

            #region Headers
            worksheet.Cell(1, 1).Value = Name;
            worksheet.Cell(2, 1).Value = "Data de Emissão: " + DateTime.Now;
            worksheet.Cell(3, 1).Value = "COPPERATIVA DE ENSINO KALIMANY";
            worksheet.Cell(4, 1).Value = "Data inicial" + filters.StartDate;
            worksheet.Cell(4, 2).Value = "Data final" + filters.EndDate;
            worksheet.Cell(5, 1).Value = "Estudante";
            worksheet.Cell(5, 2).Value = "Classe do estudante";
            worksheet.Cell(5, 3).Value = "Mes a pagar";
            worksheet.Cell(5, 4).Value = "Valor em Metical";
            worksheet.Cell(5, 5).Value = "Iva";
            worksheet.Cell(5, 6).Value = "Total com IVA (5%)";

            worksheet.Cell(1, 1).Style.Font.SetBold();
            worksheet.Cell(2, 1).Style.Font.SetBold();
            worksheet.Cell(3, 1).Style.Font.SetBold();
            worksheet.Cell(4, 1).Style.Font.SetBold();
            worksheet.Cell(4, 2).Style.Font.SetBold();
            worksheet.Cell(5, 1).Style.Font.SetBold();
            worksheet.Cell(5, 2).Style.Font.SetBold();
            worksheet.Cell(5, 3).Style.Font.SetBold();
            worksheet.Cell(5, 4).Style.Font.SetBold();
            worksheet.Cell(5, 5).Style.Font.SetBold();
            worksheet.Cell(5, 6).Style.Font.SetBold();


            #endregion

            #region body
            var currentRow = 5;

            decimal TotalMonthlyFeeWithoutVat = 0;
            decimal TotalVatOfMonthlyFee = 0;
            decimal TotalMonthlyFeeWithVat = 0;

            foreach (var item in results)
            {
                currentRow++;

                worksheet.Cell(currentRow, 1).Value = item.StudendName;
                worksheet.Cell(currentRow, 2).Value = item.StudentClassLevel;
                worksheet.Cell(currentRow, 3).Value = item.MonthPaid;
                worksheet.Cell(currentRow, 4).Value = item.MonthlyFeeWithoutVat;
                worksheet.Cell(currentRow, 5).Value = item.VatOfMonthlyFee;
                worksheet.Cell(currentRow, 6).Value = item.MonthlyFeeWithVat;

                TotalMonthlyFeeWithoutVat = TotalMonthlyFeeWithoutVat + item.MonthlyFeeWithoutVat;
                TotalVatOfMonthlyFee = TotalVatOfMonthlyFee + item.VatOfMonthlyFee;
                TotalMonthlyFeeWithVat = TotalMonthlyFeeWithVat + item.MonthlyFeeWithVat;

            }

            currentRow++;

            worksheet.Range(@$"A{currentRow}" + ":" + @$"C{currentRow}").Merge();
            worksheet.Cell(currentRow, 1).Value = "TOTAL";
            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell(currentRow, 1).Style.Font.SetBold();

            worksheet.Cell(currentRow, 4).Value = TotalMonthlyFeeWithoutVat;
            worksheet.Cell(currentRow, 5).Value = TotalVatOfMonthlyFee;
            worksheet.Cell(currentRow, 6).Value = TotalMonthlyFeeWithVat;

            worksheet.Cell(currentRow, 4).Style.Font.SetBold();
            worksheet.Cell(currentRow, 5).Style.Font.SetBold();
            worksheet.Cell(currentRow, 6).Style.Font.SetBold();

            #endregion

            #region Style   
            for (int i = 1; i < 7; i++)
            {
                worksheet.Column(i).AdjustToContents();
            }

            #endregion


            worksheet.Protect();

            using var stream = new MemoryStream();
            workBook.SaveAs(stream);
            var content = stream.ToArray();

            var ReportName = Name + " - " + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ReportName);
        }
    }
}
