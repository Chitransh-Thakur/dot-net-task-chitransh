#pragma warning disable

namespace dot_net_task_form_generation.Models
{
    #region ::ENTITY FOR GETTING OR SETTING Candidates::
    public class CandidatesInfo : BaseEntity
    {
        /// <summary>
        /// Gets or Sets FirstNameEnabled
        /// </summary>
        public bool FirstNameEnabled { get; set; }

        /// <summary>
        /// Gets or Sets LastNameEnabled
        /// </summary>
        public bool LastNameEnabled { get; set; }

        /// <summary>
        /// Gets or Sets EmailEnabled
        /// </summary>
        public bool EmailEnabled { get; set; }

        /// <summary>
        /// Gets or Sets PhoneNumberEnabled
        /// </summary>
        public bool PhoneNumberEnabled { get; set; }

        /// <summary>
        /// Gets or Sets NationalityEnabled
        /// </summary>
        public bool NationalityEnabled { get; set; }

        /// <summary>
        /// Gets or Sets CurrentResidenceEnabled
        /// </summary>
        public bool CurrentResidenceEnabled { get; set; }

        /// <summary>
        /// Gets or Sets IDNumberEnabled
        /// </summary>
        public bool IDNumberEnabled { get; set; }

        /// <summary>
        /// Gets or Sets DOBEnabled
        /// </summary>
        public bool DOBEnabled { get; set; }

        /// <summary>
        /// Gets or Sets GenderEnabled
        /// </summary>
        public bool GenderEnabled { get; set; }
    }
    #endregion
}
