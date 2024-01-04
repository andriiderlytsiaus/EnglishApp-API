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
    public class LessonController : Controller
    {
        private readonly ILessonRepository _lessonRepository;
        public IMapper _mapper;
        public LessonController(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }
        [HttpGet("/AllLessons")]
        public IActionResult GetAllLessons()
        {
            return Ok(_lessonRepository.GetLessons());
        }
        [HttpGet("/TopRatedLessons")]
        public IActionResult TopRatedLessons()
        {
            return Ok(_lessonRepository.GetTopRatedLessons());
        }
        [HttpGet("{id}")]
        public IActionResult GetLesson(int id)
        {
            var lesson = _lessonRepository.GetLesson(id);
            return Ok(_lessonRepository.GetLesson(id));
        }
        [HttpGet("MyLessons/{userId}")]
        public IActionResult GetLessonsByUser(int userId)
        {
            return Ok(_lessonRepository.GetLessonsByUser(userId));
        }
        [HttpPost("AddRating/{lessonId}")]
        public IActionResult AddRating(int lessonId, [FromBody] string rating)
        {
            if (!_lessonRepository.AddRating(lessonId, rating))
            {
                ModelState.AddModelError("", "Something went wrong while saving rating");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully added rating");
        }
        [HttpPost("AddTheory/{lessonId}")]
        public IActionResult AddTheory(int lessonId, [FromBody] TheoryDto theoryDto)
        {
            if (!_lessonRepository.LessonExists(lessonId))
            {
                return NotFound();
            }
            if (theoryDto == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var lesson = _lessonRepository.GetLesson(lessonId);
            lesson.Title = theoryDto.Title;
            lesson.Goal = theoryDto.Goal;
            lesson.Theory = theoryDto.Theory;
            if (!_lessonRepository.AddTheory(lesson))
            {
                ModelState.AddModelError("", "Something went wrong while adding theory");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully added theory");
        }
        [HttpPost("LessonInfo")]
        public IActionResult LessonInfo([FromBody] LessonInfoDto lessonInfoDto)
        {
            var lesson = _lessonRepository.GetLesson(lessonInfoDto.LessonId);
            lesson.Difficulty = lessonInfoDto.Difficulty;
            lesson.Keywords = lessonInfoDto.Keywords;
            return Ok(_lessonRepository.Save());
        }

            [HttpPost]
        public IActionResult CreateLesson([FromBody] LessonDto lessonDto)
        {
            if (lessonDto == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var lesson = new Lesson()
            {
                YouTubeVideoId = LessonService.GetYouTubeVideoId(lessonDto.YouTubeVideoId),
                CreationTime = DateTime.Now,
                Title = "Title has not been added to this lesson",
                UserId = lessonDto.UserId,
                TranscriptLines = new List<TranscriptLine>()
            };
            if (_lessonRepository.GetLessons().Any(x => x.YouTubeVideoId == lesson.YouTubeVideoId))
            {
                ModelState.AddModelError("", "This lesson already exists");
                return StatusCode(422, ModelState);
            }
            var transcriptItems = LessonService.GetTranscript(lesson.YouTubeVideoId);

            foreach(var transcriptItem in transcriptItems)
            {
                lesson.TranscriptLines.Add(new TranscriptLine()
                {
                    Value = transcriptItem.Text,
                    StartTime = transcriptItem.Start,
                    Duration  =transcriptItem.Duration

                });
            }
            if (!_lessonRepository.CreateLesson(lesson))
            {
                ModelState.AddModelError("", "Something went wrong while saving Lesson");
                return StatusCode(500, ModelState);
            }
            return Ok(lesson.Id);

        }
        [HttpDelete("id")]
        public IActionResult DeleteSavedWord(int id)
        {
            if (!_lessonRepository.LessonExists(id))
            {
                return NotFound();
            }
            var lesson = _lessonRepository.GetLesson(id);

            if (!_lessonRepository.DeleteLesson(lesson))
            {
                ModelState.AddModelError("", "Something went wrong while deleting saved word");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfull deleted");
        }
    }
}
