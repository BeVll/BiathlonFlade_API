using System.ComponentModel.DataAnnotations.Schema;

namespace MB_API.Requests.CheckPoint
{
    public class CheckPointCreateUpdateRequest
    {
        public string Name { get; set; }
        public int TrackId { get; set; }
        public int CheckPointTypeId { get; set; }
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double Z1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public double Z2 { get; set; }
    }
}
