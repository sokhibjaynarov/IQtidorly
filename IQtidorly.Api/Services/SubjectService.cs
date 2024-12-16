using AutoMapper;
using IQtidorly.Api.Data.IRepositories.Base;
using IQtidorly.Api.Interfaces;
using IQtidorly.Api.Models.Subjects;
using IQtidorly.Api.Services.Base;
using IQtidorly.Api.ViewModels.Subjects;
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
    public class SubjectService : BaseService, ISubjectService
    {
        public SubjectService(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IConfiguration configuration,
            IUnitOfWorkRepository unitOfWorkRepository,
            ILogger<SubjectService> logger) : base(httpContextAccessor, mapper, configuration, unitOfWorkRepository, logger)
        {
        }

        public async Task<Guid> CreateSubjectAsync(SubjectForCreateModel subjectForCreateModel)
        {
            try
            {
                var existingSubject = await _unitOfWorkRepository.SubjectRepository
                    .GetAll().FirstOrDefaultAsync(x => x.Name == subjectForCreateModel.Name);

                if (existingSubject != null)
                {
                    throw new Exception("Allready exist");
                }

                var subject = _mapper.Map<Subject>(subjectForCreateModel);

                subject = await _unitOfWorkRepository.SubjectRepository.AddAsync(subject);

                if (await _unitOfWorkRepository.SubjectRepository.SaveChangesAsync() > 0)
                {
                    return subject.SubjectId;
                }

                throw new Exception("error creating subject");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteSubjectAsync(Guid subjectId)
        {
            try
            {
                var subject = await _unitOfWorkRepository.SubjectRepository
                    .GetAll().Include(p => p.SubjectChapters).FirstOrDefaultAsync(x => x.SubjectId == subjectId);

                if (subject == null)
                {
                    throw new Exception("Subject not found");
                }

                if (subject.SubjectChapters.Any())
                {
                    throw new Exception("Subject has chapters");
                }

                _unitOfWorkRepository.SubjectRepository.Remove(subject);

                if (await _unitOfWorkRepository.SubjectRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new Exception("error deleting subject");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<SubjectForGetModel> GetSubjectByIdAsync(Guid subjectId)
        {
            try
            {
                var subject = await _unitOfWorkRepository.SubjectRepository
                    .GetAll().FirstOrDefaultAsync(x => x.SubjectId == subjectId);

                if (subject == null)
                {
                    throw new Exception("Subject not found");
                }

                var subjectViewModel = _mapper.Map<SubjectForGetModel>(subject);

                return subjectViewModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<(List<SubjectForGetModel> Subjects, int Count)> GetSubjectsAsPaginationAsync(int take, int skip)
        {
            try
            {
                var subjectsQuery = _unitOfWorkRepository.SubjectRepository.GetAll();

                var subjects = await subjectsQuery.Skip(skip).Take(take).ToListAsync();
                var count = await subjectsQuery.CountAsync();

                if (!subjects.Any())
                {
                    return (new List<SubjectForGetModel>(), count);
                }

                var subjectsViewModel = _mapper.Map<List<SubjectForGetModel>>(subjects);

                return (subjectsViewModel, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateSubjectAsync(SubjectForUpdateModel subjectForUpdateModel)
        {
            try
            {
                var existingSubject = await _unitOfWorkRepository.SubjectRepository
                    .GetAll().FirstOrDefaultAsync(x => x.SubjectId == subjectForUpdateModel.SubjectId);

                if (existingSubject == null)
                {
                    throw new Exception("Subject not found");
                }

                existingSubject = _mapper.Map(subjectForUpdateModel, existingSubject);

                _unitOfWorkRepository.SubjectRepository.Update(existingSubject);

                if (await _unitOfWorkRepository.SubjectRepository.SaveChangesAsync() > 0)
                {
                    return true;
                }

                throw new Exception("error updating subject");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
