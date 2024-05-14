#pragma warning disable 
using dot_net_task_form_generation.Enums;

#pragma warning disable

namespace dot_net_task_form_generation.Models.DTOs
{
    #region ::DTO for Question::
    public class QuestionsDto
    {
        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        public QuestionTypes Type { get; set; }

        /// <summary>
        /// Gets or Sets QuestionText
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Gets or Sets Choices
        /// </summary>
        public List<string> Choices { get; set; }

        /// <summary>
        /// Gets or Sets EnableOtherOption
        /// </summary>
        public bool EnableOtherOption { get; set; }

    }
    #endregion
}
