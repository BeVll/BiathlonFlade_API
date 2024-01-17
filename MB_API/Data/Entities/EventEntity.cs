using System.ComponentModel.DataAnnotations.Schema;

namespace MB_API.Data.Entities
{
    public class EventEntity
    {
        public int Id { get; set; }
        public string EventName { get; set; } = "";

        [ForeignKey("EventType")]
        public int EventTypeId { get; set; }

        public bool TeamEvent { get; set; }

        [ForeignKey("Track")]
        public int TrackId { get; set; }

        public DateTime EventDate { get; set; }

        public virtual EventTypeEnitity EventType { get; set; }
        public virtual TrackEntity Track { get; set; }
    }
}
