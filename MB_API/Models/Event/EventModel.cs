using MB_API.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MB_API.Models.Event
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        public TrackEntity Track { get; set; }
        public CompetitionEntity Competition { get; set; }
        public CountryEntity Country { get; set; }
        public List<RaceEntity> Races { get; set; } = new List<RaceEntity>();
    }
}
