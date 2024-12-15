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
            IAgeGroupRepository ageGroupRepository)
        {
            UserRepository = userRepository;
            FileRepository = fileRepository;
            BookRepository = bookRepository;
            SubjectRepository = subjectRepository;
            BookAuthorRepository = bookAuthorRepository;
            SubjectChapterRepository = subjectChapterRepository;
            AgeGroupRepository = ageGroupRepository;
        }

        public IUserRepository UserRepository { get; }
        public IFileRepository FileRepository { get; }
        public IBookRepository BookRepository { get; }
        public ISubjectRepository SubjectRepository { get; }
        public IBookAuthorRepository BookAuthorRepository { get; }
        public ISubjectChapterRepository SubjectChapterRepository { get; }
        public IAgeGroupRepository AgeGroupRepository { get; }
    }
}
