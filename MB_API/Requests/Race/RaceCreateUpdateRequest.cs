using MB_API.Data.Entities;

namespace MB_API.Requests.Event
{
    public class RaceCreateUpdateRequest
    {
        public string EventName { get; set; }
        public int EventTypeId { get; set; }
        public bool TeamEvent { get; set; }
        public int TrackId { get; set; }
        public DateTime EventDate { get; set; }
    }
}
