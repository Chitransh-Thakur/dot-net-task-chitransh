using dot_net_task_form_generation.Models.DTOs;
using dot_net_task_form_generation.Services.IServices;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable

namespace dot_net_task_form_generation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateApplyController : ControllerBase
    {
        #region ::CLASS LEVEL DECLARATIONS::
        private readonly IQuestionService _questionService;
        private readonly ICandidateInfoService _candidateInfoService;
        private readonly IProgramService _programService;
        #endregion

        #region ::CONSTRUCTOR::
        public CandidateApplyController(IQuestionService questionService, ICandidateInfoService candidateInfoService, IProgramService programService)
        {
            _programService = programService;
            _questionService = questionService;
            _candidateInfoService = candidateInfoService;
        }
        #endregion

        #region ::ACTION METHODS::
        /// <summary>
        /// Get all info to apply
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-app-info")]
        public async Task<IActionResult> FetchCandidateApplyAppInfo()
        {
            var programResponse = await _programService.GetAllProgramsAsync();
            if ((programResponse.isSuccess))
            {
                var candidateInfoResponse = await _candidateInfoService.GetAllCandidatesInfoAsync();
                if (candidateInfoResponse.isSuccess)
                {
                    var questionsResponse = await _questionService.GetAllQuestionsAsync();
                    if (questionsResponse.isSuccess)
                    {

                        FetchAppInfo fetchAppInfo = new()
                        {
                            Programs = programResponse.Data,
                            CandidatesInfo = candidateInfoResponse.Data,
                            Questions = questionsResponse.Data
                        };
                        return Ok(fetchAppInfo);
                    }
                    else
                    {
                        return BadRequest(questionsResponse);
                    }
                }
                else
                {
                    return BadRequest(candidateInfoResponse);
                }
            }
            else
            {
                return BadRequest(programResponse);
            }
        }

        /// <summary>
        /// Submit the application
        /// </summary>
        /// <param name="candidateApplyDto"></param>
        /// <returns></returns>
        [HttpPost("apply")]
        public async Task<IActionResult> CandidateApply(CandidateApplyDto candidateApplyDto)
        {
           var applyResponse = await _candidateInfoService.CandidateApplyAsync(candidateApplyDto);
           if (applyResponse.isSuccess)
           { 
             return Ok(applyResponse);
           }
           return BadRequest(applyResponse);
        }
        #endregion
    }
}
