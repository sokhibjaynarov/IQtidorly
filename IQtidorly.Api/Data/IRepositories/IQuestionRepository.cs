using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.Questions;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IQuestionRepository : IBaseRepository<Question, ApplicationDbContext>
    {
    }
}
