using dot_net_task_form_generation.Models.DTOs;
using dot_net_task_form_generation.Models.Response;

namespace dot_net_task_form_generation.Services.IServices
{
    #region ::Interface Methods::
    public interface ICandidateInfoService
    {
        Task<RestResponse> GetAllCandidatesInfoAsync();
        Task<RestResponse> CreateCandidateAsync(CandidatesInfoDto candidateDto);
        Task<RestResponse> CandidateApplyAsync(CandidateApplyDto candidateApplyDto);
    }
    #endregion
}
