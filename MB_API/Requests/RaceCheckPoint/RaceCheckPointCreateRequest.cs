using MB_API.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MB_API.Requests.RaceCheckPoint
{
    public class RaceCheckPointCreateRequest
    {
        public string Name { get; set; }
        public int RaceId { get; set; }
        public int CheckPointId { get; set; }
        public int Lap { get; set; }
        public int Number { get; set; }
    }
}
