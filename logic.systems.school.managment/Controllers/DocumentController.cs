using ClosedXML.Excel;
using DinkToPdf;
using DinkToPdf.Contracts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using logic.systems.school.managment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using System.Collections.Generic;
using System.Text;
using static NuGet.Packaging.PackagingConstants;

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
        private XLColor bgColorHeader = XLColor.FromTheme(XLThemeColor.Accent1, 0.0);
        private XLColor fontColorHeader = XLColor.White;

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
                worksheet.Cell("E11").Value = result.Enrollment.PaymentEnrollment.PaymentWithoutVat + " MT";

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
                worksheet.Cell("E30").Value = result.Enrollment.PaymentEnrollment.PaymentWithoutVat + " MT";


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
                worksheet.Cell("E11").Value = result.Enrollment.PaymentEnrollment.PaymentWithoutVat + " MT";

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
                worksheet.Cell("E30").Value = result.Enrollment.PaymentEnrollment.PaymentWithoutVat + " MT";


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
            var currentUser = await _userManager.GetUserAsync(User);

            var result = await _DocumentService.GetTuitionInvoiceById(id);


            string relativePath = "template/invoice.html";
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, relativePath);
            var htmlContent = System.IO.File.ReadAllText(filePath);

            var formattedHtml = htmlContent.Replace("{0}", result.FirstOrDefault().Id.ToString())
                                           .Replace("{1}", result.FirstOrDefault().CreatedDate.ToString("dd/MM/yyyy"))
                                           .Replace("{2}", DateTime.Now.ToString("dd/MM/yyyy"))

                                           .Replace("{3}", $"Recibo de Mensalidade")
                                           .Replace("{4}", result.FirstOrDefault().Tuition.Enrollment.StudentId.ToString())
                                           .Replace("{5}", currentUser.UserName)

                                           .Replace("{6}", result.FirstOrDefault().Tuition.Enrollment.Student.Name + "<br> Mensalidades a pagar: " + string.Join(", ", result.Select(x => x.Tuition.MonthName).Distinct()));


            var TableLines = new List<string>();

            TableLines.Add(
            InvoiceTableLineDTO.Line
                                .Replace("{desc}", "Mensalidade")
                                .Replace("{unityPrice}", result.FirstOrDefault().PaymentWithVat + " MT")
                                .Replace("{quantity}", result.Count().ToString())
                                .Replace("{paymentDate}", result.FirstOrDefault().PaymentDate.ToString("dd/MM/yyyy"))
                                .Replace("{Classe}", await _AppService.SempleEntityDescriptionById(result.FirstOrDefault().Tuition.Enrollment.SchoolLevelId))
                                .Replace("{ClasseRoom}", await _AppService.SempleEntityDescriptionById(result.FirstOrDefault().Tuition.Enrollment.SchoolClassRoomId))
                                .Replace("{Year}", result.FirstOrDefault().Tuition.Enrollment.EnrollmentYear.ToString())
                                .Replace("{payment}", result.Sum(p => p.PaymentWithoutVat) + " MT"));


            var TotaltuitionFines = (decimal)0;
            if (result.Where(x => x.Tuition.TuitionFines is not null).Count() > 0)
            {

                var tuitionFines = result.Where(x => x.Tuition.TuitionFines is not null).Select(x => x.Tuition.TuitionFines);

                if (tuitionFines.Count() > 0)
                {
                    TotaltuitionFines = tuitionFines.Sum(p => p.Tuition.TuitionFines.FinesValue);

                    TableLines.Add(
                       InvoiceTableLineDTO.Line
                       .Replace("{desc}", "Multa")
                       .Replace("{unityPrice}", TotaltuitionFines + " MT")
                       .Replace("{quantity}", "1")
                       .Replace("{paymentDate}", tuitionFines.FirstOrDefault().PaidDate.Value.ToString("dd/MM/yyyy"))
                       .Replace("{Classe}", await _AppService.SempleEntityDescriptionById(result.FirstOrDefault().Tuition.Enrollment.SchoolLevelId))
                       .Replace("{ClasseRoom}", await _AppService.SempleEntityDescriptionById(result.FirstOrDefault().Tuition.Enrollment.SchoolClassRoomId))
                       .Replace("{Year}", tuitionFines.FirstOrDefault().Tuition.Enrollment.EnrollmentYear.ToString())
                       .Replace("{payment}", TotaltuitionFines + " MT"));
                }

            }


            TableLines.Add(
                    InvoiceTableLineDTO.Line
                                        .Replace("{desc}", "")
                                        .Replace("{unityPrice}", "")
                                        .Replace("{paymentDate}", "")
                                        .Replace("{quantity}", "")
                                        .Replace("{Classe}", "")
                                        .Replace("{ClasseRoom}", "")
                                        .Replace("{Year}", "")
                                        .Replace("{payment}", "SubTotal: " + (result.Sum(p => p.PaymentWithoutVat) + TotaltuitionFines) + " MT"));

            TableLines.Add(
                  InvoiceTableLineDTO.Line
                                        .Replace("{desc}", "")
                                        .Replace("{unityPrice}", "")
                                        .Replace("{paymentDate}", "")
                                        .Replace("{quantity}", "")
                                        .Replace("{Classe}", "")
                                        .Replace("{ClasseRoom}", "")
                                        .Replace("{Year}", "")
                                       .Replace("{payment}", "IVA 5%: " + (result.Sum(p => p.VatOfPayment)) + " MT"));


            TableLines.Add(
                   InvoiceTableLineDTO.Line
                                        .Replace("{desc}", "")
                                        .Replace("{unityPrice}", "")
                                        .Replace("{paymentDate}", "")
                                        .Replace("{quantity}", "")
                                        .Replace("{Classe}", "")
                                        .Replace("{ClasseRoom}", "")
                                        .Replace("{Year}", "")
                                        .Replace("{payment}", "Total: " + (result.Sum(p => p.PaymentWithVat) + TotaltuitionFines) + " MT"));


            var TableLinesIntoOneTring = string.Empty;

            foreach (var line in TableLines)
            {
                TableLinesIntoOneTring = TableLinesIntoOneTring + line;
            }

            formattedHtml = formattedHtml.Replace("{items}", TableLinesIntoOneTring);


            // Retorna o conteúdo HTML como uma resposta JSON
            return Json(new { HtmlContent = formattedHtml });

        }

        public async Task<IActionResult> EnrollmentInvoicePDF(int id)
        {
            var result = await _DocumentService.GetEnrollmentInvoiceByEnrollId(id);
            var currentUser = await _userManager.GetUserAsync(User);

            string relativePath = "template/invoice.html";
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, relativePath);
            var htmlContent = System.IO.File.ReadAllText(filePath);

            var sumTotalEnroll = result.Enrollment.PaymentEnrollment.PaymentWithoutVat + result.Enrollment.PaymentEnrollment.VatOfPayment;

            var formattedHtml = htmlContent.Replace("{0}", result.Id.ToString())
                                           .Replace("{1}", result.CreatedDate.ToString("dd/MM/yyyy"))
                                           .Replace("{2}", DateTime.Now.ToString("dd/MM/yyyy"))
                                           .Replace("{3}", "Recibo de Matricula")
                                           .Replace("{4}", result.Enrollment.StudentId.ToString())
                                           .Replace("{5}", currentUser.UserName)
                                           .Replace("{6}", result.Enrollment.Student.Name);



            var TableLines = new List<string>();
            TableLines.Add(
           InvoiceTableLineDTO.Line
                              .Replace("{desc}", "Matricula")
                              .Replace("{unityPrice}", result.Enrollment.PaymentEnrollment.PaymentWithoutVat + " MT")
                              .Replace("{quantity}", 1.ToString())
                              .Replace("{paymentDate}", result.Date.ToString("dd/MM/yyyy"))
                              .Replace("{Classe}", await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolLevelId))
                              .Replace("{ClasseRoom}", await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolClassRoomId))
                              .Replace("{Year}", result.Enrollment.EnrollmentYear.ToString())
                              .Replace("{payment}", result.Enrollment.PaymentEnrollment.PaymentWithoutVat + " MT"));





            // segundo os items da mattricula, caso tenha!
            var itensTotals = (decimal)0;
            if (result.Enrollment.EnrollmentItems is not null && result.Enrollment.EnrollmentItems.Count > 0)
            {
                foreach (var item in result.Enrollment.EnrollmentItems)
                {
                    TableLines.Add(
                    InvoiceTableLineDTO.Line
                                           .Replace("{desc}", item.Description)
                                           .Replace("{unityPrice}", item.Price + " MT")
                                           .Replace("{quantity}", 1.ToString())
                                           .Replace("{paymentDate}", result.Date.ToString("dd/MM/yyyy"))
                                           .Replace("{Classe}", await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolLevelId))
                                           .Replace("{ClasseRoom}", await _AppService.SempleEntityDescriptionById(result.Enrollment.SchoolClassRoomId))
                                           .Replace("{Year}", result.Enrollment.EnrollmentYear.ToString())
                                           .Replace("{payment}", item.Price + " MT"));


                    itensTotals = itensTotals + item.Price;
                }
            }


            TableLines.Add(
              InvoiceTableLineDTO.Line
                                   .Replace("{desc}", "")
                                   .Replace("{unityPrice}", "")
                                   .Replace("{paymentDate}", "")
                                   .Replace("{quantity}", "")
                                   .Replace("{Classe}", "")
                                   .Replace("{ClasseRoom}", "")
                                   .Replace("{Year}", "")
                                   .Replace("{payment}", "SubTotal: " + (sumTotalEnroll + itensTotals) + " MT"));

            TableLines.Add(
              InvoiceTableLineDTO.Line
                                   .Replace("{desc}", "")
                                   .Replace("{unityPrice}", "")
                                   .Replace("{paymentDate}", "")
                                   .Replace("{quantity}", "")
                                   .Replace("{Classe}", "")
                                   .Replace("{ClasseRoom}", "")
                                   .Replace("{Year}", "")
                                   .Replace("{payment}", "IVA 0%: " + result.Enrollment.PaymentEnrollment.VatOfPayment + " MT"));

            TableLines.Add(
                 InvoiceTableLineDTO.Line
                                      .Replace("{desc}", "")
                                      .Replace("{unityPrice}", "")
                                      .Replace("{paymentDate}", "")
                                      .Replace("{quantity}", "")
                                      .Replace("{Classe}", "")
                                      .Replace("{ClasseRoom}", "")
                                      .Replace("{Year}", "")
                                      .Replace("{payment}", "Total: " + (sumTotalEnroll + itensTotals) + " MT"));


            var TableLinesIntoOneTring = string.Empty;

            foreach (var line in TableLines)
            {
                TableLinesIntoOneTring = TableLinesIntoOneTring + line;
            }

            formattedHtml = formattedHtml.Replace("{items}", TableLinesIntoOneTring);

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
            var currentUser = await _userManager.GetUserAsync(User);

            // update suspendes
            await _ITuitionService.CheckFee(0, currentUser.Id);
            List<BeneficiariesSuspededReportDTO> results = await _DocumentService.GetBeneficiariesSuspeded();

            return await GenerateSuspended(results, "Relatório de Estudantes Suspensos");
        }

        private async Task<FileContentResult> GenerateSuspended(List<BeneficiariesSuspededReportDTO> data, string Name = "")
        {
            using var workBook = new XLWorkbook();
            var worksheet = workBook.Worksheets.Add("Estudantes suspensos");

            #region Headers


            worksheet.Range(@$"A{1}" + ":" + @$"K{1}").Merge();
            worksheet.Range(@$"A{2}" + ":" + @$"K{2}").Merge();
            worksheet.Range(@$"A{3}" + ":" + @$"K{3}").Merge();


            worksheet.Cell(1, 1).Value = Name;
            worksheet.Cell(2, 1).Value = "Data de Emissão: " + DateTime.Now;
            worksheet.Cell(3, 1).Value = "COOPERATIVA DE ENSINO KALIMANY";

            worksheet.Range(@$"A{1}" + ":" + @$"K{1}").Style.Font.SetBold();
            worksheet.Range(@$"A{2}" + ":" + @$"K{2}").Style.Font.SetBold();
            worksheet.Range(@$"A{3}" + ":" + @$"K{3}").Style.Font.SetBold();

            worksheet.Range(@$"A{1}" + ":" + @$"K{1}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Range(@$"A{2}" + ":" + @$"K{2}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Range(@$"A{3}" + ":" + @$"K{3}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            worksheet.Range(@$"A{1}" + ":" + @$"K{1}").Style.Fill.BackgroundColor = bgColorHeader;
            worksheet.Range(@$"A{2}" + ":" + @$"K{2}").Style.Fill.BackgroundColor = bgColorHeader;
            worksheet.Range(@$"A{3}" + ":" + @$"K{3}").Style.Fill.BackgroundColor = bgColorHeader;


            worksheet.Range(@$"A{1}" + ":" + @$"K{1}").Style.Font.FontColor = fontColorHeader;
            worksheet.Range(@$"A{2}" + ":" + @$"K{2}").Style.Font.FontColor = fontColorHeader;
            worksheet.Range(@$"A{3}" + ":" + @$"K{3}").Style.Font.FontColor = fontColorHeader;





            var line = 5;

            worksheet.Cell(line, 1).Value = "Estudante";
            worksheet.Cell(line, 2).Value = "Gênero";
            worksheet.Cell(line, 3).Value = "Data de nascimento";
            worksheet.Cell(line, 4).Value = "Classe actual";
            worksheet.Cell(line, 5).Value = "Mensalidade";
            worksheet.Cell(line, 6).Value = "Classe da mensalidade";
            worksheet.Cell(line, 7).Value = "Valor da mesalidade";
            worksheet.Cell(line, 8).Value = "Estado da mensalidade";
            worksheet.Cell(line, 9).Value = "Primeiro prazo de pagamento";
            worksheet.Cell(line, 10).Value = "Multa da mensalidade";
            worksheet.Cell(line, 11).Value = "Segundo prazo de pagamento (data de suspensão)";

            for (int i = 1; i <= 11; i++)
            {
                worksheet.Cell(line, i).Style.Fill.BackgroundColor = bgColorHeader;
                worksheet.Cell(line, i).Style.Font.FontColor = fontColorHeader;
                worksheet.Cell(line, i).Style.Font.SetBold();
            }

            foreach (var item in data)
            {



                for (int i = 1; i <= 4; i++)
                {
                    worksheet.Cell(line, i).Style.Fill.BackgroundColor = bgColorHeader;
                    worksheet.Cell(line, i).Style.Font.FontColor = fontColorHeader;
                    worksheet.Cell(line, i).Style.Font.SetBold();
                }

                line++;

                worksheet.Cell(line, 1).Value = item.StudendName;
                worksheet.Cell(line, 2).Value = item.StudendGender;
                worksheet.Cell(line, 3).Value = item.StudendBirthDate;
                worksheet.Cell(line, 4).Value = item.StudentClassLevel;




                line--;




                for (int i = 5; i <= 11; i++)
                {
                    worksheet.Cell(line, i).Style.Fill.BackgroundColor = bgColorHeader;
                    worksheet.Cell(line, i).Style.Font.FontColor = fontColorHeader;
                    worksheet.Cell(line, i).Style.Font.SetBold();
                }

                line++;

                foreach (var t in item.items)
                {
                    worksheet.Cell(line, 5).Value = t.MonthTuition;
                    worksheet.Cell(line, 6).Value = t.AssociatedLevel;
                    worksheet.Cell(line, 7).Value = t.MonthTuitionValue;
                    worksheet.Cell(line, 8).Value = t.TuitionPaimentStatus;
                    worksheet.Cell(line, 9).Value = t.PaymentTerm_first;
                    worksheet.Cell(line, 10).Value = t.MonthTuitionFee;
                    worksheet.Cell(line, 11).Value = t.PaymentTerm_Secund;

                    //    worksheet.Cell(line, 8).Style.Font.FontColor = XLColor.Red;

                    line++;
                }


                // for (int i = 1; i <= 11; i++)
                // {
                //     worksheet.Cell(line, i).Style.Fill.BackgroundColor = bgColorHeader;
                // }

                line++;
            }


            for (int i = 1; i < 12; i++)
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

        private async Task<FileContentResult> PaymentTuitionListMethod(ReportDataFilterDTO filters, string Name = "")
        {
            var results = await _DocumentService.GetPaymentTuitionList(filters.StartDate, filters.EndDate);


            using var workBook = new XLWorkbook();
            var worksheet = workBook.Worksheets.Add("Fecho de contas");

            #region Headers



            worksheet.Cell(1, 1).Value = Name;
            worksheet.Cell(2, 1).Value = "Data de Emissão: " + results.EmissionDate;
            worksheet.Cell(3, 1).Value = results.Site;
            worksheet.Cell(4, 1).Value = "Data inicial" + results.StartDate + " - " + "Data final" + results.EndDate;

            for (int i = 1; i <= 4; i++)
            {
                worksheet.Range(@$"A{i}" + ":" + @$"E{i}").Merge();
                worksheet.Range(@$"A{i}" + ":" + @$"E{i}").Style.Font.SetBold();
                worksheet.Range(@$"A{i}" + ":" + @$"E{i}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                worksheet.Range(@$"A{i}" + ":" + @$"E{i}").Style.Fill.BackgroundColor = bgColorHeader;
                worksheet.Range(@$"A{i}" + ":" + @$"E{i}").Style.Font.FontColor = fontColorHeader;
                worksheet.Column(i).AdjustToContents();

            }

            worksheet.Cell(6, 1).Value = "NºR";
            worksheet.Cell(6, 2).Value = "Nome";
            worksheet.Cell(6, 3).Value = "Valor";
            worksheet.Cell(6, 4).Value = "Iva (5%)";
            worksheet.Cell(6, 5).Value = "Total";
             


            for (int i = 1; i <= 5; i++)
            {
                worksheet.Cell(6, i).Style.Fill.BackgroundColor = bgColorHeader;
                worksheet.Cell(6, i).Style.Font.FontColor = fontColorHeader;
                worksheet.Cell(6, i).Style.Font.SetBold();
                worksheet.Column(i).AdjustToContents();
            }

            worksheet.Cell(1, 1).Style.Font.SetBold();
            worksheet.Cell(2, 1).Style.Font.SetBold();
            worksheet.Cell(3, 1).Style.Font.SetBold();
            worksheet.Cell(4, 1).Style.Font.SetBold();
            worksheet.Cell(4, 2).Style.Font.SetBold();

            #endregion

            #region body
            var currentRow = 6;

            decimal TotalMonthlyFeeWithoutVat = 0;
            decimal TotalVatOfMonthlyFee = 0;
            decimal TotalMonthlyFeeWithVat = 0;

            foreach (var item in results.InvoiceLine)
            {
                currentRow++;

              worksheet.Cell(currentRow, 1).Value = item.InvoiceId;
              worksheet.Cell(currentRow, 2).Value = item.Type;
              worksheet.Cell(currentRow, 3).Value = item.InvoicePrice;
              worksheet.Cell(currentRow, 4).Value = item.InvoiceVat;
              worksheet.Cell(currentRow, 5).Value = item.InvoicePriceWithVat; 
             
            }

            currentRow++;

            worksheet.Range(@$"A{currentRow}" + ":" + @$"B{currentRow}").Merge();
            worksheet.Range(@$"A{currentRow}" + ":" + @$"B{currentRow}").Style.Fill.BackgroundColor = bgColorHeader;
            worksheet.Range(@$"A{currentRow}" + ":" + @$"B{currentRow}").Style.Font.FontColor = fontColorHeader;

            worksheet.Cell(currentRow, 1).Value = "TOTAL";
            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell(currentRow, 1).Style.Font.SetBold();

            worksheet.Cell(currentRow, 3).Value =  results.TotalInvoicePrice;
            worksheet.Cell(currentRow, 4).Value = results.TotalInvoiceVat;
            worksheet.Cell(currentRow, 5).Value = results.TotalInvoicePriceWithVat;


            for (int i = 1; i <= 5; i++)
            {
                worksheet.Cell(currentRow, i).Style.Font.SetBold();
                worksheet.Cell(currentRow, i).Style.Fill.BackgroundColor = bgColorHeader;
                worksheet.Cell(currentRow, i).Style.Font.FontColor = fontColorHeader;
                worksheet.Column(i).AdjustToContents();
            }

            #endregion

            #region Style   
            for (int i = 1; i <= 11; i++)
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
