using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.Quizzes;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IQuizRepository : IBaseRepository<Quiz, ApplicationDbContext>
    {
    }
}
