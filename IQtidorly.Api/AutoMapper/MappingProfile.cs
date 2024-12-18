using AutoMapper;
using IQtidorly.Api.Models.AgeGroups;
using IQtidorly.Api.Models.BookAuthors;
using IQtidorly.Api.Models.Books;
using IQtidorly.Api.Models.QuestionOptions;
using IQtidorly.Api.Models.Questions;
using IQtidorly.Api.Models.Quizzes;
using IQtidorly.Api.Models.SubjectChapters;
using IQtidorly.Api.Models.Subjects;
using IQtidorly.Api.Models.Users;
using IQtidorly.Api.ViewModels.AgeGroups;
using IQtidorly.Api.ViewModels.BookAuthors;
using IQtidorly.Api.ViewModels.Books;
using IQtidorly.Api.ViewModels.QuestionOptions;
using IQtidorly.Api.ViewModels.Questions;
using IQtidorly.Api.ViewModels.Quizzes;
using IQtidorly.Api.ViewModels.SubjectChapters;
using IQtidorly.Api.ViewModels.Subjects;
using IQtidorly.Api.ViewModels.Users;

namespace IQtidorly.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Users
            CreateMap<User, CreateUserViewModel>().ReverseMap();

            // AgeGroups
            CreateMap<AgeGroup, AgeGroupForCreateModel>().ReverseMap();
            CreateMap<AgeGroup, AgeGroupForUpdateModel>().ReverseMap();
            CreateMap<AgeGroup, AgeGroupForGetModel>().ReverseMap();

            // Books
            CreateMap<Book, BookForCreateModel>().ReverseMap();
            CreateMap<Book, BookForUpdateModel>().ReverseMap();
            CreateMap<Book, BookForGetModel>().ReverseMap();

            // BookAuthors
            CreateMap<BookAuthor, BookAuthorForCreateModel>().ReverseMap();
            CreateMap<BookAuthor, BookAuthorForGetModel>().ReverseMap();
            CreateMap<BookAuthor, BookAuthorForUpdateModel>().ReverseMap();

            // Subjects
            CreateMap<Subject, SubjectForCreateModel>().ReverseMap();
            CreateMap<Subject, SubjectForUpdateModel>().ReverseMap();
            CreateMap<Subject, SubjectForGetModel>().ReverseMap();

            // SubjectChapters
            CreateMap<SubjectChapter, SubjectChapterForCreateModel>().ReverseMap();
            CreateMap<SubjectChapter, SubjectChapterForUpdateModel>().ReverseMap();
            CreateMap<SubjectChapter, SubjectChapterForGetModel>().ReverseMap();

            // Questions
            CreateMap<QuestionForCreateModel, Question>()
                .ForMember(dest => dest.Options, opt => opt.Ignore());

            CreateMap<QuestionForUpdateModel, Question>()
                .ForMember(dest => dest.Options, opt => opt.Ignore());

            CreateMap<Question, QuestionForGetListModel>()
                .ForMember(p => p.SubjectName, opt => opt.MapFrom(src => src.SubjectChapter.Subject.Name))
                .ForMember(p => p.SubjectId, opt => opt.MapFrom(src => src.SubjectChapter.SubjectId))
                .ForMember(p => p.ChapterName, opt => opt.MapFrom(src => src.SubjectChapter.Name))
                .ForMember(p => p.AgeGroupName, opt => opt.MapFrom(src => src.AgeGroup.Name));

            CreateMap<Question, QuestionForGetModel>()
                .ForMember(p => p.SubjectName, opt => opt.MapFrom(src => src.SubjectChapter.Subject.Name))
                .ForMember(p => p.SubjectId, opt => opt.MapFrom(src => src.SubjectChapter.SubjectId))
                .ForMember(p => p.ChapterName, opt => opt.MapFrom(src => src.SubjectChapter.Name))
                .ForMember(p => p.AgeGroupName, opt => opt.MapFrom(src => src.AgeGroup.Name));

            // QuestionOptions
            CreateMap<QuestionOption, QuestionOptionForSaveModel>().ReverseMap();
            CreateMap<QuestionOption, QuestionOptionForGetModel>().ReverseMap();

            // Quizzes
            CreateMap<QuizForCreateModel, Quiz>()
                .ForMember(dest => dest.QuizQuestions, opt => opt.Ignore());

            CreateMap<QuizForUpdateModel, Quiz>()
                .ForMember(dest => dest.QuizQuestions, opt => opt.Ignore());

            CreateMap<Quiz, QuizForGetModel>().ReverseMap();

        }
    }
}
