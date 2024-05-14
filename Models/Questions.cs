#pragma warning disable 

using dot_net_task_form_generation.Enums;

namespace dot_net_task_form_generation.Models
{
    #region ::ENTITY FOR GETTING OR SETTING Questions::
    public class Questions : BaseEntity
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
