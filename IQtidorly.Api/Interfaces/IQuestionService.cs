using IQtidorly.Api.ViewModels.Questions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQtidorly.Api.Interfaces
{
    public interface IQuestionService
    {
        Task<(List<QuestionForGetModel> Questions, int Count)> GetAllQuestionsAsPaginationAsync(int take, int skip);
        Task<Guid> CreateQuestionAsync(QuestionForCreateModel questionForCreateModel);
    }
}
