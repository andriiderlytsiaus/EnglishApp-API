using AutoMapper;
using BLL;
using EnglishApp.Dto;
using EnglishApp.Interface;
using EnglishApp.Models;
using EnglishApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EnglishApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : Controller
    {
        public IMapper _mapper;
        public IQuizRepository _QuizRepository;
        public QuizController( IMapper mapper, IQuizRepository quizRepository)
        {
            _QuizRepository = quizRepository;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult AddQuiz([FromBody] QuizDto quizDto)
        {
            if (quizDto == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var quiz = _mapper.Map<Quiz>(quizDto);
            if (!_QuizRepository.AddQuiz(quiz))
            {
                ModelState.AddModelError("", "Something went wrong while saving Quiz");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created quiz");
        }

    }
}
