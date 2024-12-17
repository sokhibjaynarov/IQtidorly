using AutoMapper;
using IQtidorly.Api.Models.AgeGroups;
using IQtidorly.Api.Models.BookAuthors;
using IQtidorly.Api.Models.Books;
using IQtidorly.Api.Models.QuestionOptions;
using IQtidorly.Api.Models.Questions;
using IQtidorly.Api.Models.SubjectChapters;
using IQtidorly.Api.Models.Subjects;
using IQtidorly.Api.Models.Users;
using IQtidorly.Api.ViewModels.AgeGroups;
using IQtidorly.Api.ViewModels.BookAuthors;
using IQtidorly.Api.ViewModels.Books;
using IQtidorly.Api.ViewModels.QuestionOptions;
using IQtidorly.Api.ViewModels.Questions;
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
            CreateMap<Question, QuestionForCreateModel>().ReverseMap();
            CreateMap<Question, QuestionForGetModel>().ReverseMap();

            // QuestionOptions
            CreateMap<QuestionOption, QuestionOptionForSaveModel>().ReverseMap();
        }
    }
}
