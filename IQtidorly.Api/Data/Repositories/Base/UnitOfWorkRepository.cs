using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.IRepositories.Base;

namespace IQtidorly.Api.Data.Repositories.Base
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        public UnitOfWorkRepository(
            IUserRepository userRepository,
            IFileRepository fileRepository,
            IBookAuthorRepository bookAuthorRepository,
            IBookRepository bookRepository,
            ISubjectChapterRepository subjectChapterRepository,
            ISubjectRepository subjectRepository,
            IAgeGroupRepository ageGroupRepository,
            IOlympiadQuestionRepository olympiadQuestionRepository,
            IOlympiadRepository olympiadRepository,
            IOlympiadResultRepository olympiadResultRepository,
            IOlympiadResultAnswerRepository olympiadResultAnswerRepository,
            IQuestionOptionRepository questionOptionRepository,
            IQuestionRepository questionRepository)
        {
            UserRepository = userRepository;
            FileRepository = fileRepository;
            BookRepository = bookRepository;
            SubjectRepository = subjectRepository;
            BookAuthorRepository = bookAuthorRepository;
            SubjectChapterRepository = subjectChapterRepository;
            AgeGroupRepository = ageGroupRepository;
            QuestionRepository = questionRepository;
            QuestionOptionRepository = questionOptionRepository;
            OlympiadRepository = olympiadRepository;
            OlympiadQuestionRepository = olympiadQuestionRepository;
            OlympiadResultRepository = olympiadResultRepository;
            OlympiadResultAnswerRepository = olympiadResultAnswerRepository;
        }

        public IUserRepository UserRepository { get; }
        public IFileRepository FileRepository { get; }
        public IBookRepository BookRepository { get; }
        public ISubjectRepository SubjectRepository { get; }
        public IBookAuthorRepository BookAuthorRepository { get; }
        public ISubjectChapterRepository SubjectChapterRepository { get; }
        public IAgeGroupRepository AgeGroupRepository { get; }
        public IQuestionRepository QuestionRepository { get; }
        public IQuestionOptionRepository QuestionOptionRepository { get; }
        public IOlympiadRepository OlympiadRepository { get; }
        public IOlympiadQuestionRepository OlympiadQuestionRepository { get; }
        public IOlympiadResultRepository OlympiadResultRepository { get; }
        public IOlympiadResultAnswerRepository OlympiadResultAnswerRepository { get; }
    }
}
