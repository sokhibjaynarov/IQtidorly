using IQtidorly.Api.ViewModels.Quizzes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQtidorly.Api.Interfaces
{
    public interface IQuizService
    {
        Task<Guid> CreateQuizAsync(QuizForCreateModel viewModel);
        Task<QuizForGetModel> GetQuizByIdAsync(Guid quizId);
        Task<(List<QuizForGetModel> Quizzes, int Count)> GetAllQuizzesAsPaginationAsync(int take, int skip);
        Task<bool> UpdateQuizAsync(QuizForUpdateModel viewModel);
        Task<bool> DeleteQuizAsync(Guid quizId);
    }
}
