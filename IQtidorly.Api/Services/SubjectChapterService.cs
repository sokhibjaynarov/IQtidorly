using AutoMapper;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Models.SubjectChapters;
using IQtidorly.Api.Services.Base;
using IQtidorly.Api.ViewModels.SubjectChapters;
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
    public class SubjectChapterService : BaseService, ISubjectChapterService
    {
        public SubjectChapterService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger<SubjectChapterService> logger) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
        }

        public async Task<Guid> CreateSubjectChapterAsync(SubjectChapterForCreateModel subjectChapterForCreateModel)
        {
            try
            {
                var existingSubjectChapter = await _unitOfWorkRepository.SubjectChapterRepository
                    .GetAll().FirstOrDefaultAsync(x => x.Name == subjectChapterForCreateModel.Name);

                if (existingSubjectChapter != null)
                {
                    throw new Exception("Allready exist");
                }

                var subject = await _unitOfWorkRepository.SubjectRepository
                    .GetAll().FirstOrDefaultAsync(x => x.SubjectId == subjectChapterForCreateModel.SubjectId);

                if (subject != null)
                {
                    throw new Exception("Subject not found");
                }

                var subjectChapter = _mapper.Map<SubjectChapter>(subjectChapterForCreateModel);

                subjectChapter = await _unitOfWorkRepository.SubjectChapterRepository.AddAsync(subjectChapter);

                if (await _unitOfWorkRepository.SubjectChapterRepository.SaveChangesAsync() > 0)
                {
                    return subjectChapter.SubjectChapterId;
                }

                throw new Exception("error creating subject chapter");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteSubjectChapterAsync(Guid subjectChapterId)
        {
            try
            {
                var subjectChapter = await _unitOfWorkRepository.SubjectChapterRepository
                    .GetAll().FirstOrDefaultAsync(x => x.SubjectChapterId == subjectChapterId);

                if (subjectChapter == null)
                {
                    throw new Exception("Subject chapter not found");
                }

                _unitOfWorkRepository.SubjectChapterRepository.Remove(subjectChapter);

                if (await _unitOfWorkRepository.SubjectChapterRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new Exception("error deleting subject chapter");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<SubjectChapterForGetModel> GetSubjectChapterByIdAsync(Guid subjectChapterId)
        {
            try
            {
                var subjectChapter = await _unitOfWorkRepository.SubjectChapterRepository.GetAsync(subjectChapterId);

                if (subjectChapter == null)
                {
                    throw new Exception("Subject chapter not found");
                }

                var subjectChapterViewModel = _mapper.Map<SubjectChapterForGetModel>(subjectChapter);

                return subjectChapterViewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<(List<SubjectChapterForGetModel> SubjectChapters, int Count)> GetSubjectChaptersAsPaginationAsync(int take, int skip)
        {
            try
            {
                var subjectChaptersQuery = _unitOfWorkRepository.SubjectChapterRepository.GetAll();

                var subjectChapters = await subjectChaptersQuery.Skip(skip).Take(take).ToListAsync();
                var count = await subjectChaptersQuery.CountAsync();

                if (!subjectChapters.Any())
                {
                    return (new List<SubjectChapterForGetModel>(), count);
                }

                var subjectChaptersViewModel = _mapper.Map<List<SubjectChapterForGetModel>>(subjectChapters);

                return (subjectChaptersViewModel, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateSubjectChapterAsync(SubjectChapterForUpdateModel subjectChapterForUpdateModel)
        {
            try
            {
                var existingSubjectChapter = await _unitOfWorkRepository.SubjectChapterRepository
                    .GetAll().FirstOrDefaultAsync(x => x.SubjectChapterId == subjectChapterForUpdateModel.SubjectChapterId);

                if (existingSubjectChapter == null)
                {
                    throw new Exception("Subject chapter not found");
                }

                var subject = await _unitOfWorkRepository.SubjectRepository
                    .GetAll().FirstOrDefaultAsync(x => x.SubjectId == subjectChapterForUpdateModel.SubjectId);

                if (subject == null)
                {
                    throw new Exception("Subject not found");
                }

                existingSubjectChapter = _mapper.Map(subjectChapterForUpdateModel, existingSubjectChapter);

                _unitOfWorkRepository.SubjectChapterRepository.Update(existingSubjectChapter);

                if (await _unitOfWorkRepository.SubjectChapterRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new Exception("error updating subject chapter");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
