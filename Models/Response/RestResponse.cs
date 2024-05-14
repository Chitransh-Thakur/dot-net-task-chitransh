namespace dot_net_task_form_generation.Models.Response
{
    #region ::DTO for returning response::
    public class RestResponse
    {
        /// <summary>
        /// Gets or Sets Success Value
        /// </summary>
        public bool isSuccess { get; set; }

        /// <summary>
        /// Gets or Sets Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or Sets DTO
        /// </summary>
        public dynamic? Data { get; set; }
    }
    #endregion
}
