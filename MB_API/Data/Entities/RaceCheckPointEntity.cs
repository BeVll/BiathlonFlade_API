using System.ComponentModel.DataAnnotations.Schema;

namespace MB_API.Data.Entities
{
    public class RaceCheckPointEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Race")]
        public int RaceId { get; set; }

        [ForeignKey("CheckPoint")]
        public int CheckPointId { get; set; }
        public int Lap {  get; set; }

        public int Number {  get; set; }
        public virtual RaceEntity Race { get; set; }
        public virtual CheckPointEntity CheckPoint { get; set; }
    }
}
