namespace IQtidorly.Api.Data.IRepositories.Base
{
    public interface IUnitOfWorkRepository
    {
        public IUserRepository UserRepository { get; }
        public IFileRepository FileRepository { get; }
    }
}
