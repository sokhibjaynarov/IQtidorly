using AutoMapper;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Data.Repositories;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Models.AgeGroups;
using IQtidorly.Api.Services.Base;
using IQtidorly.Api.ViewModels.AgeGroups;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var existingAgeGroup = await _unitOfWorkRepository.AgeGroupRepository
                    .GetAll().FirstOrDefaultAsync(x => x.Name == ageGroupForCreateModel.Name ||
                        (x.MaxAge == ageGroupForCreateModel.MaxAge && x.MinAge == ageGroupForCreateModel.MinAge));

                if (existingAgeGroup != null)
                {
                    throw new Exception("Allready exist");
                }

                var ageGroup = _mapper.Map<AgeGroup>(ageGroupForCreateModel);

                ageGroup = await _unitOfWorkRepository.AgeGroupRepository.AddAsync(ageGroup);

                if (await _unitOfWorkRepository.AgeGroupRepository.SaveChangesAsync() > 0)
                {
                    return ageGroup.AgeGroupId;
                }

                throw new Exception("error creating age group");
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
                    throw new Exception("error not found");
                }

                _unitOfWorkRepository.AgeGroupRepository.Remove(ageGroup);

                if (await _unitOfWorkRepository.AgeGroupRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<AgeGroupForGetModel> GetAgeGroupByIdAsync(Guid ageGroupId)
        {
            try
            {
                var ageGroup = await _unitOfWorkRepository.AgeGroupRepository.GetAsync(ageGroupId);

                if (ageGroup == null)
                {
                    throw new Exception("error not found");
                }

                var ageGroupViewModel = _mapper.Map<AgeGroupForGetModel>(ageGroup);

                return ageGroupViewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<(List<AgeGroupForGetModel> AgeGroups, int Count)> GetAllAgeGroupsAsPaginationAsync(int take, int skip)
        {
            try
            {
                var ageGroupsQuery = _unitOfWorkRepository.AgeGroupRepository.GetAll();

                var ageGroups = await ageGroupsQuery.Skip(skip).Take(take).ToListAsync();
                var count = await ageGroupsQuery.CountAsync();

                if (!ageGroups.Any())
                {
                    return (new List<AgeGroupForGetModel>(), count);
                }

                var ageGroupViewModel = _mapper.Map<List<AgeGroupForGetModel>>(ageGroups);

                return (ageGroupViewModel, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateAgeGroupAsync(AgeGroupForUpdateModel ageGroupForUpdateModel)
        {
            try
            {
                var ageGroup = await _unitOfWorkRepository.AgeGroupRepository.GetAsync(ageGroupForUpdateModel.AgeGroupId);

                if (ageGroup == null)
                {
                    throw new Exception("error not found");
                }

                ageGroup = _mapper.Map(ageGroupForUpdateModel, ageGroup);

                _unitOfWorkRepository.AgeGroupRepository.Update(ageGroup);

                if (await _unitOfWorkRepository.AgeGroupRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
