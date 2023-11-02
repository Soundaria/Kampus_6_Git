using System;
using System.Collections.Generic;

namespace CaseStudyKampusLearnAPI.Models
{
    public partial class Progress
    {
        public int Id { get; set; }
        public int? TrainerId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? Attendance { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Trainer? Trainer { get; set; }
    }
}
