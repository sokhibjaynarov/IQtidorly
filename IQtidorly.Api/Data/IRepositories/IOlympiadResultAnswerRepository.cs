using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.OlympiadResultAnswers;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IOlympiadResultAnswerRepository : IBaseRepository<OlympiadResultAnswer, ApplicationDbContext>
    {
    }
}
