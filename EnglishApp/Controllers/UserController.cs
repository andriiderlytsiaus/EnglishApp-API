using AutoMapper;
using EnglishApp.Dto;
using EnglishApp.Interface;
using EnglishApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace EnglishApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository  = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_mapper.Map<List<UserDto>>(_userRepository.GetUsers()));
        }
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            if(!_userRepository.UserExists(id)) {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDto>(_userRepository.GetUser(id)));
        }
        [HttpPost("/SignIn")]
        public IActionResult SignIn([FromBody] CredentialsDto credentialsDto)
        {
            if (credentialsDto == null)
                return BadRequest(ModelState);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var person = _userRepository.GetUsers().FirstOrDefault(x => x.Email == credentialsDto.Email);
            if (person == null)
            {
                ModelState.AddModelError("", "User with this email does not exist");
                return StatusCode(422, ModelState);
            }
            if (person.Password != credentialsDto.Password)
            {
                ModelState.AddModelError("", "Wrong password");
                return StatusCode(422, ModelState);
            }
            return Ok(person.Id);
        }

            [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto userDto) {
            if (userDto == null)
                return BadRequest(ModelState);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(_userRepository.GetUsers().Any(x=>x.Email == userDto.Email))
            {
                ModelState.AddModelError("", "User with the same email already exists");
                return StatusCode(422, ModelState);
            }
            var user = _mapper.Map<User>(userDto);
            if (!_userRepository.CreateUser(user))
            {
                ModelState.AddModelError("", "Something went wrong while saving User");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created account");
        }
   
    }   
}
