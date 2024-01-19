namespace MB_API.Requests.Result
{
    public class ResultCreateUpdateRequest
    {
        public int RaceId { get; set; }
        public string UserName { get; set; }
        public int CheckpointId { get; set; }
        public long? ResultValue { get; set; }
        public bool IsDNF { get; set; }
        public int StartNumber { get; set; }
        public bool IsTeamResult { get; set; }
        public bool IsFinish { get; set; }
        public int Lap { get; set; }
        public int? TeamId { get; set; }
        public int? StageNumber { get; set; }
    }
}
