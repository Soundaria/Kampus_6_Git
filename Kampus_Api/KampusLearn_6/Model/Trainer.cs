using System;
using System.Collections.Generic;
using System.ComponentModel;
using KampusLearn_6.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudyKampusLearnAPI.Models
{
    public partial class Trainer
    {
        public Trainer()
        {
            Progress = new HashSet<Progress>();
            Scheduling = new HashSet<Scheduling>();
            TrainerCourse = new HashSet<TrainerCourse>();
        }

        public int TrainerId { get; set; }
        public string? Name { get; set; }
        public string Password { get; set; }
        public string? Contact { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }

		[DefaultValue(true)]
		public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? AdminId { get; set; }
        public int? YearOfExperience { get; set; }
        public string? Qualification { get; set; }

		[ForeignKey("TrainerId")]
		public User? User { get; set; }
		public virtual Admin? Admin { get; set; }
        public virtual ICollection<Progress> Progress { get; set; }
        public virtual ICollection<Scheduling> Scheduling { get; set; }
        public virtual ICollection<TrainerCourse> TrainerCourse { get; set; }
    }
}
