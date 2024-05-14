#pragma warning disable

namespace dot_net_task_form_generation.Models.DTOs
{
    #region ::DTO for CandidateApply::
    public class CandidateApplyDto
    {
        public PersonalInfo CandidatePersonalInfo { get; set; }
        public List<AnswersDto> AnswerDtoList { get; set; }
    }
    #endregion
}
