using IQtidorly.Api.ViewModels.Subjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQtidorly.Api.Interfaces
{
    public interface ISubjectService
    {
        Task<(List<SubjectForGetModel> Subjects, int Count)> GetSubjectsAsPaginationAsync(int take, int skip);
        Task<SubjectForGetModel> GetSubjectByIdAsync(Guid subjectId);
        Task<Guid> CreateSubjectAsync(SubjectForCreateModel subjectForCreateModel);
        Task<bool> DeleteSubjectAsync(Guid subjectId);
        Task<bool> UpdateSubjectAsync(SubjectForUpdateModel subjectForUpdateModel);
    }
}
