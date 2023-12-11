using ClosedXML.Excel;
using logic.systems.school.managment.Data;
using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using Microsoft.AspNetCore.Mvc;

namespace logic.systems.school.managment.Controllers
{
    public class DocumentController : Controller
    {
        private Idocument _DocumentService;
        public DocumentController(Idocument DocumentService)
        {
            this._DocumentService = DocumentService;
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

            var results = await _DocumentService.GetPaymentTuitionList(filters.StartDate, filters.EndDate);


            using var workBook = new XLWorkbook();
            var worksheet = workBook.Worksheets.Add("Fecho de contas");

            #region Headers
            worksheet.Cell(1, 1).Value = "Relatorio de Fecho de contas por datas";
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

            worksheet.Range(@$"A{currentRow}"+":"+ @$"C{currentRow}").Merge();
            worksheet.Cell(currentRow, 1).Value =   "TOTAL";
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


            //worksheet.Protect();

            using var stream = new MemoryStream();
            workBook.SaveAs(stream);
            var content = stream.ToArray();

            var ReportName = "Relatorio de Fecho de contas por datas - " + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", ReportName);
        }


    }
}
