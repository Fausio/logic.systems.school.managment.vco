﻿using logic.systems.school.managment.Dto;
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

        [HttpGet]
        public async Task<IActionResult> Create(GradeConfigDTO dto)
        {
            ViewBag.GradeHeader = await GetGradeHeader(dto);

            return View(new AssessmentCreateDTO()
            {dto= dto,
                Assessments = await _IGradeService.ReadAssessmentsByClassLevelClassRoomSubjectQuarter(dto)
            });
        }

        [HttpPost]
        public ActionResult Create(AssessmentCreateDTO CreateDTO)
        {
            ViewBag.GradeHeader =   GetGradeHeader(CreateDTO.dto).Result;
            TempData["MensagemSucess"] = "Lançamento de notas bem-sucedida!";
            ViewBag.Mensagem = TempData["MensagemSucess"];
            return View(CreateDTO);
        }

        public async Task<IActionResult> Config(GradeConfigDTO dto)
        {
            await ConfigView();

            if (ModelState.IsValid)
            {
                string serializedDto = JsonConvert.SerializeObject(dto);
                TempData["ConfigDTO"] = serializedDto;
                return RedirectToAction("Create", "Grade");
            }

            return View(dto);
        }



        private async Task ConfigView()
        {

            ViewBag.SchoolLevels = await _SempleEntityService.GetByTypeOrderById("SchoolLevel");
            ViewBag.SchoolClassRooms = await _SempleEntityService.GetByTypeOrderById("SchoolClassRoom");
            ViewBag.Subjects = await _SempleEntityService.GetByTypeOrderById("Subject");
            ViewBag.Quarters = new List<int>() { 1, 2, 3 };
        }

        private async Task<GradeHeaderDTO> GetGradeHeader(GradeConfigDTO dto)
        {

            var SchoolLevel = await _SempleEntityService.GetById(dto.ClassLevel);
            var SchoolClassRoom = await _SempleEntityService.GetById(dto.ClassRoom);
            var Subject = await _SempleEntityService.GetById(dto.Subject);
            var Quarter = dto.Quarter;



            var dtoResult = new GradeHeaderDTO()
            {
                ClassLevel = SchoolLevel.Description,
                ClassRoom = SchoolClassRoom.Description,
                Subject = Subject.Description,
                Quarter = Quarter.ToString(),
            };


            return dtoResult;
        }
    }
}
