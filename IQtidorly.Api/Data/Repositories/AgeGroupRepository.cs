using IQtidorly.Api.Data.IRepositories;
using IQtidorly.Api.Data.Repositories.Base;
using IQtidorly.Api.Helpers;
using IQtidorly.Api.Models.AgeGroups;

namespace IQtidorly.Api.Data.Repositories
{
    public class AgeGroupRepository : BaseRepository<AgeGroup, ApplicationDbContext>, IAgeGroupRepository
    {
        public AgeGroupRepository(ApplicationDbContext dbContext, IRequestLanguageHelper languageHelper) : base(dbContext, languageHelper)
        {
        }
    }
}
