#pragma warning disable

namespace dot_net_task_form_generation.Models.DTOs
{
    #region ::DTO for Fetching AppInfo::
    public class FetchAppInfo
    {
        /// <summary>
        /// Gets or Sets Programs
        /// </summary>
        public List<Programs> Programs { get; set; }

        /// <summary>
        /// Gets or Sets Candidates
        /// </summary>
        public List<CandidatesInfo> CandidatesInfo { get; set; }

        /// <summary>
        /// Questions
        /// </summary>
        public List<Questions> Questions { get; set; }
    }
    #endregion
}
