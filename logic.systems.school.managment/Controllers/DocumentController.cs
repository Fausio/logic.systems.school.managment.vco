using ClosedXML.Excel;
using DinkToPdf;
using DinkToPdf.Contracts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using logic.systems.school.managment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using System.Text;

namespace logic.systems.school.managment.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private Idocument _DocumentService;
        private IApp _AppService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConverter _pdfConverter;
        private ITuitionService _ITuitionService;
        public DocumentController(Idocument DocumentService,
                                  IWebHostEnvironment hostingEnvironment,
                                  IApp appService,
                                   ITuitionService iTuitionService,
                                  UserManager<IdentityUser> userManager,
                                  IConverter pdfConverter
                                  )
        {
            this._DocumentService = DocumentService;
            this._hostingEnvironment = hostingEnvironment;
            this._AppService = appService;
            this._ITuitionService = iTuitionService;
            this._userManager = userManager;
            this._pdfConverter = pdfConverter ?? throw new ArgumentNullException(nameof(pdfConverter));

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


                #region first
                worksheet.Cell("E4").Value = DateTime.Now.ToString("dd/MM/yyyy");
                worksheet.Cell("E5").Value = result.Id;
                worksheet.Cell("E6").Value = result.Enrollment.StudentId;
                worksheet.Cell("E7").Value = "Recibo de Matricula";

                worksheet.Cell("C8").Value = result.Enrollment.Student.Name;

                worksheet.Cell("B11").Value = "#";
                worksheet.Cell("C11").Value = await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolClassRoomId);
                worksheet.Cell("C11").Value = await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolLevelId);
                worksheet.Cell("D11").Value = result.Enrollment.EnrollmentYear;
                worksheet.Cell("E11").Value = result.Enrollment.PaymentEnrollment.PaymentWithoutVat + "MT";

                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser != null)
                {
                    worksheet.Cell("E13").Value = "Emitido por: " + currentUser.UserName;
                }
                #endregion
                #region Secund
                worksheet.Cell("E23").Value = DateTime.Now.ToString("dd/MM/yyyy");
                worksheet.Cell("E24").Value = result.Id;
                worksheet.Cell("E25").Value = result.Enrollment.StudentId;
                worksheet.Cell("E26").Value = "Recibo de Matricula";

                worksheet.Cell("C27").Value = result.Enrollment.Student.Name;

                worksheet.Cell("B30").Value = "#";
                worksheet.Cell("C30").Value = await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolClassRoomId);
                worksheet.Cell("C30").Value = await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolLevelId);
                worksheet.Cell("D30").Value = result.Enrollment.EnrollmentYear;
                worksheet.Cell("E30").Value = result.Enrollment.PaymentEnrollment.PaymentWithoutVat + "MT";


                if (currentUser != null)
                {
                    worksheet.Cell("E32").Value = "Emitido por: " + currentUser.UserName;
                }
                #endregion


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

        public async Task<IActionResult> TuitionInvoice(int id)
        {
            var result = await _DocumentService.GetEnrollmentInvoiceByEnrollId(id);


            string relativePath = "template/invoice.xlsx";
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, relativePath);

            if (System.IO.File.Exists(filePath))
            {
                using var workbook = new XLWorkbook(filePath);
                var worksheet = workbook.Worksheets.Worksheet(1);


                #region first
                worksheet.Cell("E4").Value = DateTime.Now.ToString("dd/MM/yyyy");
                worksheet.Cell("E5").Value = result.Id;
                worksheet.Cell("E6").Value = result.Enrollment.StudentId;
                worksheet.Cell("E7").Value = "Recibo de Matricula";

                worksheet.Cell("C8").Value = result.Enrollment.Student.Name;

                worksheet.Cell("B11").Value = "#";
                worksheet.Cell("C11").Value = await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolClassRoomId);
                worksheet.Cell("C11").Value = await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolLevelId);
                worksheet.Cell("D11").Value = result.Enrollment.EnrollmentYear;
                worksheet.Cell("E11").Value = result.Enrollment.PaymentEnrollment.PaymentWithoutVat + "MT";

                var currentUser = await _userManager.GetUserAsync(User);

                if (currentUser != null)
                {
                    worksheet.Cell("E13").Value = "Emitido por: " + currentUser.UserName;
                }
                #endregion
                #region Secund
                worksheet.Cell("E23").Value = DateTime.Now.ToString("dd/MM/yyyy");
                worksheet.Cell("E24").Value = result.Id;
                worksheet.Cell("E25").Value = result.Enrollment.StudentId;
                worksheet.Cell("E26").Value = "Recibo de Matricula";

                worksheet.Cell("C27").Value = result.Enrollment.Student.Name;

                worksheet.Cell("B30").Value = "#";
                worksheet.Cell("C30").Value = await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolClassRoomId);
                worksheet.Cell("C30").Value = await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolLevelId);
                worksheet.Cell("D30").Value = result.Enrollment.EnrollmentYear;
                worksheet.Cell("E30").Value = result.Enrollment.PaymentEnrollment.PaymentWithoutVat + "MT";


                if (currentUser != null)
                {
                    worksheet.Cell("E32").Value = "Emitido por: " + currentUser.UserName;
                }
                #endregion


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


        public async Task<IActionResult> TuitionInvoicePDF(int id)
        {
            var result = await _DocumentService.GetTuitionInvoiceById(id);
            var currentUser = await _userManager.GetUserAsync(User);

            string relativePath = "template/invoice.html";
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, relativePath);
            // Lê o conteúdo do arquivo HTML
            var htmlContent = System.IO.File.ReadAllText(filePath);

            var sum = result.PaymentWithoutVat + result.VatOfPayment;



            var formattedHtml = htmlContent.Replace("{0}", result.Id.ToString())
                                           .Replace("{1}", result.CreatedDate.ToString("dd/MM/yyyy"))
                                           .Replace("{2}", DateTime.Now.ToString("dd/MM/yyyy"))

                                           .Replace("{3}", $"Recibo de Mensalidade ({result.Tuition.MonthNumber + "-" + result.Tuition.MonthName + "-" + result.Tuition.Year})")
                                           .Replace("{4}", result.Tuition.Enrollment.StudentId.ToString())
                                           .Replace("{5}", currentUser.UserName)

                                           .Replace("{6}", result.Tuition.Enrollment.Student.Name)

                                           .Replace("{7}", "#")
                                           .Replace("{8}", await _AppService.SempleEntityDescriptionById(result.Tuition.Enrollment.SchoolLevelId))
                                           .Replace("{9}", await _AppService.SempleEntityDescriptionById(result.Tuition.Enrollment.SchoolClassRoomId))
                                           .Replace("{10}", result.Tuition.Enrollment.EnrollmentYear.ToString())

                                           .Replace("{11}", result.PaymentWithoutVat + "MT")
                                           .Replace("{12}", result.VatOfPayment + "MT")
                                           .Replace("{13}", sum + "MT");

            // Retorna o conteúdo HTML como uma resposta JSON
            return Json(new { HtmlContent = formattedHtml });
        }

        public async Task<IActionResult> EnrollmentInvoicePDF(int id)
        {
            var result = await _DocumentService.GetEnrollmentInvoiceByEnrollId(id);
            var currentUser = await _userManager.GetUserAsync(User);

            string relativePath = "template/invoice.html";
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, relativePath);
            // Lê o conteúdo do arquivo HTML
            var htmlContent = System.IO.File.ReadAllText(filePath);

            var sum = result.Enrollment.PaymentEnrollment.PaymentWithoutVat + result.Enrollment.PaymentEnrollment.VatOfPayment;

            var formattedHtml = htmlContent.Replace("{0}", result.Id.ToString())
                                           .Replace("{1}", result.CreatedDate.ToString("dd/MM/yyyy"))
                                           .Replace("{2}", DateTime.Now.ToString("dd/MM/yyyy"))

                                           .Replace("{3}", "Recibo de Matricula")
                                           .Replace("{4}", result.Enrollment.StudentId.ToString())
                                           .Replace("{5}", currentUser.UserName)

                                           .Replace("{6}", result.Enrollment.Student.Name)

                                           .Replace("{7}", "#")
                                           .Replace("{8}", await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolLevelId))
                                           .Replace("{9}", await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolClassRoomId))
                                           .Replace("{10}", result.Enrollment.EnrollmentYear.ToString())
                                           .Replace("{11}", result.Enrollment.PaymentEnrollment.PaymentWithoutVat + "MT")

                                           .Replace("{12}", result.Enrollment.PaymentEnrollment.VatOfPayment + "MT")
                                           .Replace("{13}", sum + "MT");

            // Retorna o conteúdo HTML como uma resposta JSON
            return Json(new { HtmlContent = formattedHtml });
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

        public async Task<IActionResult> downloadsuspended()
        {

            // update suspendes
            await _ITuitionService.CheckFee(0);
            await _ITuitionService.AutomaticRegularization(0);


            // get all the data

            // create excel File

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

            worksheet.Cell(5, 1).Value = "Tipo";
            worksheet.Cell(5, 2).Value = "Estudante";
            worksheet.Cell(5, 3).Value = "Classe do estudante";
            worksheet.Cell(5, 4).Value = "Mes a pagar";
            worksheet.Cell(5, 5).Value = "Valor em Metical";
            worksheet.Cell(5, 6).Value = "Iva";
            worksheet.Cell(5, 7).Value = "Total com IVA (5%)";

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
            worksheet.Cell(5, 7).Style.Font.SetBold();

            #endregion

            #region body
            var currentRow = 5;

            decimal TotalMonthlyFeeWithoutVat = 0;
            decimal TotalVatOfMonthlyFee = 0;
            decimal TotalMonthlyFeeWithVat = 0;

            foreach (var item in results)
            {
                currentRow++;

                worksheet.Cell(currentRow, 1).Value = item.Type;
                worksheet.Cell(currentRow, 2).Value = item.StudendName;
                worksheet.Cell(currentRow, 3).Value = item.StudentClassLevel;
                worksheet.Cell(currentRow, 4).Value = item.MonthPaid;
                worksheet.Cell(currentRow, 5).Value = item.MonthlyFeeWithoutVat;
                worksheet.Cell(currentRow, 6).Value = item.VatOfMonthlyFee;
                worksheet.Cell(currentRow, 7).Value = item.MonthlyFeeWithVat;

                TotalMonthlyFeeWithoutVat = TotalMonthlyFeeWithoutVat + item.MonthlyFeeWithoutVat;
                TotalVatOfMonthlyFee = TotalVatOfMonthlyFee + item.VatOfMonthlyFee;
                TotalMonthlyFeeWithVat = TotalMonthlyFeeWithVat + item.MonthlyFeeWithVat;

            }

            currentRow++;

            worksheet.Range(@$"A{currentRow}" + ":" + @$"D{currentRow}").Merge();
            worksheet.Cell(currentRow, 1).Value = "TOTAL";
            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell(currentRow, 1).Style.Font.SetBold();

            worksheet.Cell(currentRow, 5).Value = TotalMonthlyFeeWithoutVat;
            worksheet.Cell(currentRow, 6).Value = TotalVatOfMonthlyFee;
            worksheet.Cell(currentRow, 7).Value = TotalMonthlyFeeWithVat;

            worksheet.Cell(currentRow, 5).Style.Font.SetBold();
            worksheet.Cell(currentRow, 6).Style.Font.SetBold();
            worksheet.Cell(currentRow, 7).Style.Font.SetBold();

            #endregion

            #region Style   
            for (int i = 1; i < 8; i++)
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

        public IActionResult GeneratePdf()
        {
            // Create an Excel workbook using ClosedXML
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");
                worksheet.Cell("A1").Value = "Hello";
                worksheet.Cell("A2").Value = "World";
                // Save the Excel workbook to a MemoryStream
                using (var excelStream = new MemoryStream())
                {
                    workbook.SaveAs(excelStream);
                    excelStream.Position = 0;

                    // Convert the Excel file to PDF using DinkToPdf
                    var pdfBytes = ConvertToPdf(excelStream);

                    // Return the PDF file as a response
                    return File(pdfBytes, "application/pdf", "output.pdf");
                }
            }
        }
        private byte[] ConvertToPdf(Stream excelStream)
        {
            // Convert the Excel stream to a byte array
            byte[] excelBytes;
            using (var memoryStream = new MemoryStream())
            {
                excelStream.CopyTo(memoryStream);
                excelBytes = memoryStream.ToArray();
            }

            var pdfDocument = new HtmlToPdfDocument
            {
                Objects = { new ObjectSettings { HtmlContent = Encoding.UTF8.GetString(excelBytes) } },
                GlobalSettings = { PaperSize = PaperKind.A4 },
            };

            return _pdfConverter.Convert(pdfDocument);
        }

    }
}
