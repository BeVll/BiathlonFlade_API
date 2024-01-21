namespace MB_API.Requests.Competition
{
    public class CompetitionCreateUpdateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Season { get; set; }
    }
}
