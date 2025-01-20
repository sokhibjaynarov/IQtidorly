using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Helpers;
using IQtidorly.Api.Models.QuizParticipants;

namespace IQtidorly.Api.Data.Repositories
{
    public class QuizParticipantRepository : BaseRepository<QuizParticipant, ApplicationDbContext>, IQuizParticipantRepository
    {
        public QuizParticipantRepository(ApplicationDbContext dbContext, IRequestLanguageHelper languageHelper) : base(dbContext, languageHelper)
        {
        }
    }
}
