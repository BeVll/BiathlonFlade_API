﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MB_API.Data.Entities
{
    public class RaceEntity
    {
        public int Id { get; set; }
        public string EventName { get; set; } = "";

        [ForeignKey("EventType")]
        public int EventTypeId { get; set; }

        public bool TeamEvent { get; set; }

        [ForeignKey("Track")]
        public int TrackId { get; set; }

        public DateTime EventDate { get; set; }

        public virtual RaceTypeEnitity EventType { get; set; }
        public virtual TrackEntity Track { get; set; }
    }
}