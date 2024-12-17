using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.OlympiadQuestions;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IOlympiadQuestionRepository : IBaseRepository<OlympiadQuestion, ApplicationDbContext>
    {
    }
}
