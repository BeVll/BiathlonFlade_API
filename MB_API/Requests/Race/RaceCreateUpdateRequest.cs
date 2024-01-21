using MB_API.Data.Entities;

namespace MB_API.Requests.Event
{
    public class RaceCreateUpdateRequest
    {
        public string RaceName { get; set; }
        public int RaceTypeId { get; set; }
        public bool TeamRace { get; set; }
        public int TrackId { get; set; }
        public int EventId { get; set; }
        public DateTime RaceDate { get; set; }
    }
}
