using dot_net_task_form_generation.Models;
using dot_net_task_form_generation.Models.DTOs;
using dot_net_task_form_generation.Models.Response;

namespace dot_net_task_form_generation.Services.IServices
{
    #region ::Interface Methods::
    public interface IQuestionService
    {
        Task<RestResponse> GetAllQuestionsAsync();
        Task<RestResponse> AddQuestionToCache(QuestionsDto questionsDto);
        Task<RestResponse> CreateQuestionAsync();
        List<Questions> GetQuestionsFromCache();
        Task<RestResponse> DeleteCacheDataByIndex(int index);
        Task<RestResponse> UpdateQuestionByIndex(int index, QuestionsDto updatedQuestion);
    }
    #endregion
}
