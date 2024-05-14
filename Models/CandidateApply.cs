using dot_net_task_form_generation.Models.DTOs;

#pragma warning disable

namespace dot_net_task_form_generation.Models
{
    #region ::ENTITY FOR STORING CANDIDATE DATA::
    public class CandidateApply : BaseEntity
    {
        public PersonalInfo CandidatePersonalInfo { get; set; }
        public List<AnswersDto> AnswerDtoList { get; set; }
    }
    #endregion
}
