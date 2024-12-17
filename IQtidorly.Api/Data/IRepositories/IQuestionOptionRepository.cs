using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.QuestionOptions;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IQuestionOptionRepository : IBaseRepository<QuestionOption, ApplicationDbContext>
    {
    }
}
