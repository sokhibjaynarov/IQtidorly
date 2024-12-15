using AutoMapper;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Data.Repositories;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Models.AgeGroups;
using IQtidorly.Api.Services.Base;
using IQtidorly.Api.ViewModels.AgeGroups;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQtidorly.Api.Services
{
    public class AgeGroupService : BaseService, IAgeGroupService
    {
        public AgeGroupService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger<AgeGroupRepository> logger) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
        }

        public async Task<Guid> CreateAgeGroupAsync(AgeGroupForCreateModel ageGroupForCreateModel)
        {
            try
            {
                var ageGroup = _mapper.Map<AgeGroup>(ageGroupForCreateModel);

                ageGroup = await _unitOfWorkRepository.AgeGroupRepository.AddAsync(ageGroup);

                return ageGroup.AgeGroupId;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteAgeGroupAsync(Guid ageGroupId)
        {
            try
            {
                var ageGroup = await _unitOfWorkRepository.AgeGroupRepository.GetAsync(ageGroupId);

                if (ageGroup == null)
                {
                    return false;
                }

                _unitOfWorkRepository.AgeGroupRepository.Remove(ageGroup);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public Task<AgeGroupForGetModel> GetAgeGroupByIdAsync(Guid ageGroupId)
        {
            throw new NotImplementedException();
        }

        public Task<(List<AgeGroupForGetModel> AgeGroups, int Count)> GetAllAgeGroupsAsPaginationAync(int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAgeGroupAsync(AgeGroupForUpdateModel ageGroupForUpdateModel)
        {
            throw new NotImplementedException();
        }
    }
}
