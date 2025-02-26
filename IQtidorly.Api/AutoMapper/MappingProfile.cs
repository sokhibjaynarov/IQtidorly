using AutoMapper;
using IQtidorly.Api.Middlewares;
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
using Microsoft.AspNetCore.Http;

namespace IQtidorly.Api.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile(IHttpContextAccessor httpContextAccessor)
        {
            // Users
            CreateMap<User, CreateUserViewModel>().ReverseMap();
            CreateMap<User, UserInfoViewModel>()
                .ForMember(p => p.UserId, opt => opt.MapFrom(src => src.Id));

            // AgeGroups
            CreateMap<AgeGroupForCreateModel, AgeGroup>();

            CreateMap<AgeGroup, AgeGroupForUpdateModel>().ReverseMap();
            CreateMap<AgeGroup, AgeGroupForGetModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(new TranslationResolver<AgeGroup>(httpContextAccessor, nameof(AgeGroup.Name))));

            // Books
            CreateMap<Book, BookForCreateModel>().ReverseMap();
            CreateMap<Book, BookForUpdateModel>().ReverseMap();
            CreateMap<Book, BookForGetModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(new TranslationResolver<Book>(httpContextAccessor, nameof(Book.Title))))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(new TranslationResolver<Book>(httpContextAccessor, nameof(Book.Description))));

            // BookAuthors
            CreateMap<BookAuthor, BookAuthorForCreateModel>().ReverseMap();
            CreateMap<BookAuthor, BookAuthorForUpdateModel>().ReverseMap();
            CreateMap<BookAuthor, BookAuthorForGetModel>()
                .ForMember(dest => dest.FirsName, opt => opt.MapFrom(new TranslationResolver<BookAuthor>(httpContextAccessor, nameof(BookAuthor.FirstName))))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(new TranslationResolver<BookAuthor>(httpContextAccessor, nameof(BookAuthor.LastName))));


            // Subjects
            CreateMap<Subject, SubjectForCreateModel>().ReverseMap();
            CreateMap<Subject, SubjectForUpdateModel>().ReverseMap();
            CreateMap<Subject, SubjectForGetModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(new TranslationResolver<Subject>(httpContextAccessor, nameof(Subject.Name))));

            // SubjectChapters
            CreateMap<SubjectChapter, SubjectChapterForCreateModel>().ReverseMap();
            CreateMap<SubjectChapter, SubjectChapterForUpdateModel>().ReverseMap();
            CreateMap<SubjectChapter, SubjectChapterForGetModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(new TranslationResolver<SubjectChapter>(httpContextAccessor, nameof(SubjectChapter.Name))));

            // Questions
            CreateMap<QuestionForCreateModel, Question>()
                .ForMember(dest => dest.Options, opt => opt.Ignore());

            CreateMap<QuestionForUpdateModel, Question>()
                .ForMember(dest => dest.Options, opt => opt.Ignore());

            CreateMap<Question, QuestionForGetListModel>()
                .ForMember(p => p.SubjectName, opt => opt.MapFrom(src => src.SubjectChapter.Subject.Name))
                .ForMember(p => p.SubjectId, opt => opt.MapFrom(src => src.SubjectChapter.SubjectId))
                .ForMember(p => p.ChapterName, opt => opt.MapFrom(src => src.SubjectChapter.Name))
                .ForMember(p => p.AgeGroupName, opt => opt.MapFrom(src => src.AgeGroup.Name))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(new TranslationResolver<Question>(httpContextAccessor, nameof(Question.Content))));

            CreateMap<Question, QuestionForGetModel>()
                .ForMember(p => p.SubjectName, opt => opt.MapFrom(src => src.SubjectChapter.Subject.Name))
                .ForMember(p => p.SubjectId, opt => opt.MapFrom(src => src.SubjectChapter.SubjectId))
                .ForMember(p => p.ChapterName, opt => opt.MapFrom(src => src.SubjectChapter.Name))
                .ForMember(p => p.AgeGroupName, opt => opt.MapFrom(src => src.AgeGroup.Name))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(new TranslationResolver<Question>(httpContextAccessor, nameof(Question.Content))));

            // QuestionOptions
            CreateMap<QuestionOption, QuestionOptionForSaveModel>().ReverseMap();
            CreateMap<QuestionOption, QuestionOptionForGetModel>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(new TranslationResolver<QuestionOption>(httpContextAccessor, nameof(QuestionOption.Content))));

            // Quizzes
            CreateMap<QuizForCreateModel, Quiz>()
                .ForMember(dest => dest.QuizQuestions, opt => opt.Ignore());

            CreateMap<QuizForUpdateModel, Quiz>()
                .ForMember(dest => dest.QuizQuestions, opt => opt.Ignore());

            CreateMap<Quiz, QuizForGetModel>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(new TranslationResolver<Quiz>(httpContextAccessor, nameof(Quiz.Title))))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(new TranslationResolver<Quiz>(httpContextAccessor, nameof(Quiz.Description))));
        }
    }
}
