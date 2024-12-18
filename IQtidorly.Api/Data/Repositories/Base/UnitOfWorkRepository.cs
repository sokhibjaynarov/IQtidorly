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
            IQuizQuestionRepository olympiadQuestionRepository,
            IQuizRepository olympiadRepository,
            IQuizParticipantRepository olympiadResultRepository,
            IQuizSubmissionRepository olympiadResultAnswerRepository,
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
        public IQuizRepository OlympiadRepository { get; }
        public IQuizQuestionRepository OlympiadQuestionRepository { get; }
        public IQuizParticipantRepository OlympiadResultRepository { get; }
        public IQuizSubmissionRepository OlympiadResultAnswerRepository { get; }
    }
}
