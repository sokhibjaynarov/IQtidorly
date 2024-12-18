namespace IQtidorly.Api.Data.IRepositories.Base
{
    public interface IUnitOfWorkRepository
    {
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
