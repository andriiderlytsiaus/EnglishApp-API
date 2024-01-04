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
            //if (_userRepository.GetUsers().Any(x => x.Email == quizDto.Email))
            //{
            //    ModelState.AddModelError("", "User with the same email already exists");
            //    return StatusCode(422, ModelState);
            //}
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
