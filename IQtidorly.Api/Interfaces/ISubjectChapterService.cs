using IQtidorly.Api.ViewModels.SubjectChapters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQtidorly.Api.Interfaces
{
    public interface ISubjectChapterService
    {
        Task<(List<SubjectChapterForGetModel> SubjectChapters, int Count)> GetSubjectChaptersAsPaginationAsync(int take, int skip);
        Task<SubjectChapterForGetModel> GetSubjectChapterByIdAsync(Guid subjectChapterId);
        Task<Guid> CreateSubjectChapterAsync(SubjectChapterForCreateModel subjectChapterForCreateModel);
        Task<bool> DeleteSubjectChapterAsync(Guid subjectChapterId);
        Task<bool> UpdateSubjectChapterAsync(SubjectChapterForUpdateModel subjectChapterForUpdateModel);
    }
}
