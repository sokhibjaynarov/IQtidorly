using IQtidorly.Api.ViewModels.AgeGroups;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQtidorly.Api.Interfaces
{
    public interface IAgeGroupService
    {
        Task<(List<AgeGroupForGetModel> AgeGroups, int Count)> GetAllAgeGroupsAsPaginationAsync(int take, int skip);
        Task<AgeGroupForGetModel> GetAgeGroupByIdAsync(Guid ageGroupId);
        Task<Guid> CreateAgeGroupAsync(AgeGroupForCreateModel ageGroupForCreateModel);
        Task<bool> DeleteAgeGroupAsync(Guid ageGroupId);
        Task<bool> UpdateAgeGroupAsync(AgeGroupForUpdateModel ageGroupForUpdateModel);
    }
}
