using System.ComponentModel.DataAnnotations.Schema;

namespace MB_API.Models.CheckPoint
{
    public class CheckPointResultModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Lap {  get; set; }

        [ForeignKey("CheckPointType")]
        public int CheckPointTypeId { get; set; }
    }
}
