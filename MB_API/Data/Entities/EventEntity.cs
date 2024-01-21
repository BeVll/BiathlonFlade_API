using System.ComponentModel.DataAnnotations.Schema;

namespace MB_API.Data.Entities
{
    public class EventEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Track")]
        public int TrackId { get; set; }

        [ForeignKey("Competition")]
        public int CompetitionId { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public virtual TrackEntity Track { get; set; }
        public virtual CompetitionEntity Competition { get; set; }
        public virtual CountryEntity Country { get; set; }
    }
}
