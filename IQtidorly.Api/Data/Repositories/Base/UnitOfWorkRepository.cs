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
            IQuizQuestionRepository quizQuestionRepository,
            IQuizRepository quizRepository,
            IQuizParticipantRepository quizParticipantRepository,
            IQuizSubmissionRepository quizSubmissionRepository,
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
            QuizRepository = quizRepository;
            QuizQuestionRepository = quizQuestionRepository;
            QuizParticipantRepository = quizParticipantRepository;
            QuizSubmissionRepository = quizSubmissionRepository;
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
        public IQuizRepository QuizRepository { get; }
        public IQuizQuestionRepository QuizQuestionRepository { get; }
        public IQuizParticipantRepository QuizParticipantRepository { get; }
        public IQuizSubmissionRepository QuizSubmissionRepository { get; }
    }
}
