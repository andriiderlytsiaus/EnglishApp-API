using AutoMapper;
using EnglishApp.Dto;
using EnglishApp.Interface;
using EnglishApp.Models;
using EnglishApp.Repository;
using Microsoft.AspNetCore.Mvc;
using BLL;

namespace EnglishApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedWordController : Controller
    {
        private readonly ISavedWordRepository _savedWordRepository;
        private readonly IWordRepository _wordRepository;
        public IMapper _mapper;
        public SavedWordController(ISavedWordRepository savedWordRepository,
           IWordRepository wordRepository, IMapper mapper)
        {
            _savedWordRepository = savedWordRepository;
            _wordRepository = wordRepository;
            _mapper = mapper;
        }

        [HttpGet("/SavedWordsByUser/{userId}")]
        public IActionResult GetWordsByUser(int userId)
        {
            var savedWords = _mapper.Map<List<WordDto>>(_savedWordRepository.GetWordsByUser(userId));
            return Ok(savedWords);
        }
        [HttpGet("{word}")]
        public IActionResult GetSavedWordDetails(string word)
        {

            return Ok(_savedWordRepository.GetSavedWordDetails(word));
        }
        [HttpPost("{word}&{userId}")]
        public async Task<IActionResult> CreateSavedWord(string word, int userId)
        {
            
            if (_savedWordRepository.SavedWordExists(word, userId))
            {
                ModelState.AddModelError("", "You have already saved this word");
                return StatusCode(422, ModelState);
            }
            var savedWord = new SavedWord() { UserId = userId };
            if (!_wordRepository.WordExists(word))
            {
                await _wordRepository.CreateWord(word);
            }
            savedWord.WordId = _wordRepository.GetWord(word).Id;
            if (!_savedWordRepository.CreateSavedWord(savedWord))
            {
                ModelState.AddModelError("", "Something went wrong while saving Word");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully saved Word");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteSavedWord(int id) {
            if (! _savedWordRepository.SavedWordExists(id))
                return NotFound();
            var savedWord = _savedWordRepository.GetSavedWord(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_savedWordRepository.DeleteSavedWord(savedWord))
            {
                ModelState.AddModelError("", "Something went wrong while deleting saved word");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfull deleted");
        }
        [HttpDelete("{wordId}&{userId}")]
        public IActionResult DeleteSavedWord(int wordId, int userId)
        {
            var savedWord = _savedWordRepository.GetSavedWordsByUser(userId)
                .FirstOrDefault(x => x.WordId == wordId);
            if (savedWord == null)
                return NotFound();
            if (!_savedWordRepository.DeleteSavedWord(savedWord))
            {
                ModelState.AddModelError("", "Something went wrong while deleting saved word");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfull deleted");
        }
    }
}
