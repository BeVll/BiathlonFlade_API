namespace MB_API.Data.Entities
{
    public class CheckPointEntity
    {
        public int Id { get; set; }
        public string CheckpointName { get; set; }
        public int EventId { get; set; }

        public virtual EventEntity Event { get; set; }
    }
}
