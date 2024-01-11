using System.Diagnostics.Metrics;

namespace MB_API.Data.Entities
{
    public class TrackEntity
    {
        public int Id { get; set; }
        public string TrackName { get; set; }
        public int CountryId { get; set; }

        // Additional track details
        public string TrackDetails { get; set; }

        public virtual CountryEntity Country { get; set; }
    }
}
