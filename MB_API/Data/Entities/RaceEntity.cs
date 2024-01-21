using System.ComponentModel.DataAnnotations.Schema;

namespace MB_API.Data.Entities
{
    public class RaceEntity
    {
        public int Id { get; set; }
        public string RaceName { get; set; } = "";

        [ForeignKey("EventType")]
        public int RaceTypeId { get; set; }

        public bool TeamRace { get; set; }

        [ForeignKey("Track")]
        public int TrackId { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        public DateTime RaceDate { get; set; }

        public virtual RaceTypeEnitity RaceType { get; set; }
        public virtual TrackEntity Track { get; set; }
        public virtual EventEntity Event { get; set; }
    }
}
