using MB_API.Data.Entities.Identity;

namespace MB_API.Data.Entities
{
    public class ResultEntity
    {
        public int Id { get; set; }
        public int RaceId { get; set; }
        public int PlayerId { get; set; }
        public int RaceCheckpointId { get; set; }
        public long? ResultValue { get; set; }
        public bool IsDNF { get; set; }
        public int StartNumber { get; set; }
        public bool IsTeamResult { get; set; }
        public bool IsFinish { get; set; }
        public int Lap {  get; set; }
        public int? TeamId { get; set; }
        public int? StageNumber { get; set; }

        public virtual RaceEntity Race { get; set; }
        public virtual UserEntity Player { get; set; }
        public virtual RaceCheckPointEntity RaceCheckpoint { get; set; }
    }
}
