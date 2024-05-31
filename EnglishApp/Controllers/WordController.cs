using AutoMapper;
using BLL;
using BLL.WordModels;
using EnglishApp.Dto;
using EnglishApp.Interface;
using EnglishApp.Models;
using EnglishApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EnglishApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : Controller
    {
        private readonly IWordRepository _wordRepository;
        public IMapper _mapper;
        public WordController(IWordRepository wordRepository, IMapper mapper)
        {
            _wordRepository = wordRepository;
            _mapper = mapper;
        }

        [HttpGet("/GetInfo/{word}")]    
        public async Task<IActionResult> GetDefinitions(string word/*, string sentence*/)
        {

                var wordApi = await WordService.GetWord(word);
                if(wordApi == null) {
                    return NotFound();
                }
                var definitions = new List<DefinitionDto>();
                foreach (var meaning in wordApi.Meanings)
                {
                    foreach (var def in meaning.Definitions)
                    {
                        var definition = new DefinitionDto();
                        definition.PartOfSpeech = meaning.PartOfSpeech;
                        definition.Definition = def.Definition;
                        definition.Example = def.Example;
                        definitions.Add(definition);
                    }
                }
                return Ok(definitions); 
        }
    }
}
