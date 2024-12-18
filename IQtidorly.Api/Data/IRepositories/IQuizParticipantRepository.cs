using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Models.QuizParticipants;

namespace IQtidorly.Api.Data.IRepositories
{
    public interface IQuizParticipantRepository : IBaseRepository<QuizParticipant, ApplicationDbContext>
    {
    }
}
