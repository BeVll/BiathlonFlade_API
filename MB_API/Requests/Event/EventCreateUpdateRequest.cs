using System.ComponentModel.DataAnnotations.Schema;

namespace MB_API.Data.Entities
{
    public class EventCreateUpdateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TrackId { get; set; }
        public int CompetitionId { get; set; }
        public int CountryId { get; set; }
    }
}
