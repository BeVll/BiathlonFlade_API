using System.ComponentModel.DataAnnotations.Schema;

namespace MB_API.Data.Entities
{
    public class CheckPointEntity
    {
        public int Id { get; set; }
        public string CheckPointName { get; set; }
        public int TrackId { get; set; }

        [ForeignKey("CheckPointType")]
        public int CheckPointTypeId { get; set; }
        public virtual CheckPointType CheckPointType { get; set; }
    }
}
