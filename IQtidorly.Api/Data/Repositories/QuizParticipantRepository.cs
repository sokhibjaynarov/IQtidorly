using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Models.QuizParticipants;

namespace IQtidorly.Api.Data.Repositories
{
    public class QuizParticipantRepository : BaseRepository<QuizParticipant, ApplicationDbContext>, IQuizParticipantRepository
    {
        public QuizParticipantRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
