using System;
using System.Collections.Generic;

namespace CaseStudyKampusLearnAPI.Models
{
    public partial class Scheduling
    {
        public int Id { get; set; }
        public int? TrainerId { get; set; }
        public int? CourseId { get; set; }
        public string? Timing { get; set; }
        public string? WeekDay { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Trainer? Trainer { get; set; }
    }
}
