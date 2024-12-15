using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.IRepositories.Base;

namespace IQtidorly.Api.Data.Repositories.Base
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        public UnitOfWorkRepository(
            IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public IUserRepository UserRepository { get; }
        public IFileRepository FileRepository { get; }
    }
}
