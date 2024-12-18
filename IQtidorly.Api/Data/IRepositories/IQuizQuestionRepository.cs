using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.QuizQuestions;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IQuizQuestionRepository : IBaseRepository<QuizQuestion, ApplicationDbContext>
    {
    }
}
