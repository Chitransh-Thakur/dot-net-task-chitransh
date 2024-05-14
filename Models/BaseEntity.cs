namespace dot_net_task_form_generation.Models
{
    public class BaseEntity
    {
        /// <summary>
        /// Gets or Sets id
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// Gets or Sets PartitionKey
        /// </summary>
        public string PartitionKey { get; set; }

        /// <summary>
        /// Gets or Sets id
        /// </summary>
        public bool IsActive { get; set; }
    }
}
