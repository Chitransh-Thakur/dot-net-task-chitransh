#pragma warning disable

namespace dot_net_task_form_generation.Models.DTOs
{
    #region ::DTO for Answer::
    public class AnswersDto
    {
        /// <summary>
        /// Gets or Sets QuestionId
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Gets or Sets ParagraphAnswer
        /// </summary>
        public string ParagraphAnswer{ get; set; }

        /// <summary>
        /// Gets or Sets YesNoAnswer
        /// </summary>
        public bool YesNoAnswer { get; set; }

        /// <summary>
        /// Gets or Sets DropdownAnswer
        /// </summary>
        public string DropdownAnswer { get; set; }

        /// <summary>
        /// Gets or Sets DropdownOtherAnswer
        /// </summary>
        public string DropdownOtherAnswer { get; set; }

        /// <summary>
        /// Gets or Sets MultipleChoiceAnswer
        /// </summary>
        public List<string> MultipleChoiceAnswer { get; set; }
        
        /// <summary>
        /// Gets or Sets MultipleChoiceAnswer
        /// </summary>
        public DateTime DateAnswer { get; set; }
        
        /// <summary>
        /// Gets or Sets NumberAnswer
        /// </summary>
        public int NumberAnswer { get; set; }
    }
    #endregion
}
