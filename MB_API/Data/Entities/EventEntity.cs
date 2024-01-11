namespace MB_API.Data.Entities
{
    public class EventEntity
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventTypeId { get; set; }
        public virtual EventTypeEnitity EventType { get; set; }
        public bool TeamEvent { get; set; }
        public int TrackId { get; set; }
        public DateTime EventDate { get; set; }
        public virtual TrackEntity Track { get; set; }
    }
}
