using logic.systems.school.managment.Dto;
using logic.systems.school.managment.Interface;
using logic.systems.school.managment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace logic.systems.school.managment.Controllers
{
    public class GradeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private ISempleEntityService _SempleEntityService;
        private IGradeService _IGradeService;

        public GradeController(UserManager<IdentityUser> _userManager, ISempleEntityService SempleEntityService, IGradeService iGradeService)
        {
            this._userManager = _userManager;
            this._SempleEntityService = SempleEntityService;
            _IGradeService = iGradeService;

        }
        public async Task<IActionResult> Index()
        {
            await ConfigView();
            return View(new GradeConfigDTO());
        }
        public async Task<IActionResult> YearView()
        {
            await ConfigView();
            return View(new GradeConfigDTO());
        }


        public async Task<IActionResult> FilterYearView()
        {
            await ConfigView();
            return View(new GradeConfigDTO());
        }

        public async Task<IActionResult> Filter()
        {
            await ConfigView();
            return View(new GradeConfigDTO());
        }


        [HttpGet]
        public async Task<IActionResult> YearViewGrade(GradeConfigDTO param)
        {
            await ConfigView();
            ViewBag.Filter = new GradeConfigDTO();
            ViewBag.GradeHeader = await GetGradeHeader(param);
            return View(new AssessmentCreateDTO()
            {
                dto = param,
                Assessments = await _IGradeService.ReadAssessmentsByClassLevelClassRoomSubjectYear(param)
            });

        }   
        
        
        [HttpGet]
        public async Task<IActionResult> Create(GradeConfigDTO param)
        {
            await ConfigView();
            ViewBag.Filter = new GradeConfigDTO();
            ViewBag.GradeHeader = await GetGradeHeader(param);
            return View(new AssessmentCreateDTO()
            {
                dto = param,
                Assessments = await _IGradeService.ReadAssessmentsByClassLevelClassRoomSubjectQuarter(param)
            });

        }

        [HttpPost]
        public async Task<IActionResult> Create(AssessmentCreateDTO CreateDTO)
        {

            ViewBag.GradeHeader = GetGradeHeader(CreateDTO.dto).Result;
            await ConfigView();
            var currentUser = await _userManager.GetUserAsync(User);
            await _IGradeService.Create(CreateDTO.Assessments.SelectMany(x => x.Grades).ToList(), currentUser.Id);
            TempData["MensagemSucess"] = "Lançamento de notas bem-sucedida!";
            ViewBag.Mensagem = TempData["MensagemSucess"]; 
            CreateDTO.Assessments = await _IGradeService.ReadAssessmentsByClassLevelClassRoomSubjectQuarter(CreateDTO.dto);
            return View(CreateDTO);


        }




        private async Task ConfigView()
        {

            ViewBag.SchoolLevels = await _SempleEntityService.GetByTypeOrderById("SchoolLevel");
            ViewBag.SchoolClassRooms = await _SempleEntityService.GetByTypeOrderById("SchoolClassRoom");
            ViewBag.Subjects = await _SempleEntityService.GetByTypeOrderById("Subject");
            ViewBag.Quarters = new List<int>() { 1, 2, 3 };
            ViewBag.EnrollmentYears = new List<int>{
                     int.Parse(DateTime.Now.Year.ToString()),
                      int.Parse(DateTime.Now.AddYears(-1).Year.ToString()),
                 };
        }

        private async Task<GradeHeaderDTO> GetGradeHeader(GradeConfigDTO dto)
        {

            var SchoolLevel = await _SempleEntityService.GetById(dto.ClassLevel);
            var SchoolClassRoom = await _SempleEntityService.GetById(dto.ClassRoom);
            var Subject = await _SempleEntityService.GetById(dto.Subject);
           



            var dtoResult = new GradeHeaderDTO()
            {
                ClassLevel = SchoolLevel == null ? "" : SchoolLevel.Description,
                ClassRoom = SchoolClassRoom  == null ?  "": SchoolClassRoom.Description,
                Subject = Subject == null ? "" : Subject.Description,
                Quarter = dto.Quarter.ToString(),
                EnrollmentYears = dto.EnrollmentYears.ToString()
            };


            return dtoResult;
        }
    }
}
 