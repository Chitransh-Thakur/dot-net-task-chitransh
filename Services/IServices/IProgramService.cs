using dot_net_task_form_generation.Models.DTOs;
using dot_net_task_form_generation.Models.Response;

namespace dot_net_task_form_generation.Services.IServices
{
    #region ::Interface Methods::
    public interface IProgramService
    {
        Task<RestResponse> GetAllProgramsAsync();
        Task<RestResponse> CreateProgramAsync(ProgramsDto programDto);
    }
    #endregion
}
