using AutoMapper;
using EnglishApp.Dto;
using EnglishApp.Models;

namespace EnglishApp.Helper
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles() {
            CreateMap<User, UserDto>();
            CreateMap<Word, WordDto>();
            CreateMap<Lesson, LessonDto>();
            CreateMap<SavedWord, SavedWordDto>();
            CreateMap<Quiz, QuizDto>();
            CreateMap<Question, QuestionDto>();

            CreateMap<UserDto, User>();
            CreateMap<WordDto, Word>();
            CreateMap<LessonDto, Lesson>();
            CreateMap<SavedWordDto, SavedWord>();
            CreateMap<QuizDto, Quiz>();
            CreateMap<QuestionDto, Question>();
        }
    }
}
