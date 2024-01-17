namespace MB_API.Requests.Track
{
    public class TrackCreateUpdateRequest
    {
        public string TrackName { get; set; }
        public int CountryId { get; set; }
        public string TrackDetails { get; set; }
    }
}
