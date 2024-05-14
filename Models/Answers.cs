#pragma warning disable

namespace dot_net_task_form_generation.Models
{
    #region ::ENTITY FOR STORING ANSWERS::
    public class Answers
    {
        /// <summary>
        /// Gets or Sets id
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// Gets or Sets CandidateId
        /// </summary>
        public Guid CandidateId { get; set; }

        /// <summary>
        /// Gets or Sets QuestionId
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Gets or Sets Answer
        /// </summary>
        public Guid Answer { get; set; }

    }
    #endregion
}
