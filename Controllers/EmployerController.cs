using dot_net_task_form_generation.Models.DTOs;
using dot_net_task_form_generation.Services.IServices;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable

namespace dot_net_task_form_generation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        #region ::CLASS LEVEL DECLARATIONS::
        private readonly IQuestionService _questionService;
        private readonly ICandidateInfoService _candidateService;
        private readonly IProgramService _programService;
        #endregion

        #region ::CONSTRUCTOR::
        public EmployerController(IQuestionService questionService, ICandidateInfoService candidateService, IProgramService programService)
        {
            _programService = programService;
            _questionService = questionService;
            _candidateService = candidateService;
        }
        #endregion

        #region ::ACTION METHODS::
        /// <summary>
        /// Form Submission
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("employer-form-generate")]
        public async Task<IActionResult> EmployerFormGeneration(RootSubmissionDto dto)
        {
            try
            {
                var programInfoResponse = await _programService.CreateProgramAsync(dto.ProgramsDto);
                if (programInfoResponse.isSuccess)
                {
                    var candidateInfoResponse = await _candidateService.CreateCandidateAsync(dto.CandidatesInfoDto);
                    if (candidateInfoResponse.isSuccess)
                    {
                        var questionResponse = await _questionService.CreateQuestionAsync();
                        if (questionResponse.isSuccess)
                        {
                            return Ok("Form Submitted Successfully.");
                        }
                        else
                        {
                            return BadRequest(questionResponse);
                        }
                    }
                    else
                    {
                        return BadRequest(candidateInfoResponse);
                    }
                }
                else
                {
                    return BadRequest(programInfoResponse);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured " + ex);
            }
        }

        /// <summary>
        /// Add Questions to Cache
        /// </summary>
        /// <param name="questionsDto"></param>
        /// <returns></returns>

        [HttpPost("add-questions-to-cache")]
        public async Task<IActionResult> AddQuestionsToCache(QuestionsDto questionsDto)
        {
            var questionResponse = await _questionService.AddQuestionToCache(questionsDto);
            if (questionResponse.isSuccess)
                return Ok(questionResponse);
            return BadRequest(questionResponse);
        }


        /// <summary>
        /// Get Cache
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-cache")]
        public async Task<IActionResult> GetCache()
        {
            var cache = _questionService.GetQuestionsFromCache();
            return Ok(cache);
            
        }

        /// <summary>
        /// Update Question using Index
        /// </summary>
        /// <returns></returns>
        [HttpPut("update-question")]
        public async Task<IActionResult> UpdateQuestionToCache(int index, QuestionsDto questionsDto)
        {
            var updatedQuestionResponse = await _questionService.UpdateQuestionByIndex(index,questionsDto);
            if (updatedQuestionResponse.isSuccess)
            {
                return Ok(updatedQuestionResponse);
            }
            return BadRequest(updatedQuestionResponse);
        }

        /// <summary>
        /// Delete Question From Cache
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete-question-cache")]
        public async Task<IActionResult> DeleteQuestionFromCache(int index)
        {
            var deleteQuestionResponse = await _questionService.DeleteCacheDataByIndex(index);
            if (deleteQuestionResponse.isSuccess)
            { 
              return Ok(deleteQuestionResponse);
            }
            return BadRequest(deleteQuestionResponse);

        }
        #endregion
    }
}