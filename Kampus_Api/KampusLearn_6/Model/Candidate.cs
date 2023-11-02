using System;
using System.Collections.Generic;
using System.ComponentModel;
using KampusLearn_6.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseStudyKampusLearnAPI.Models
{
    public partial class Candidate
    {
        public Candidate()
        {
            CandidateCourse = new HashSet<CandidateCourse>();
            Payment = new HashSet<Payment>();
            TrainerCourse = new HashSet<TrainerCourse>();
        }

        public int CandidateId { get; set; }
        public string? Name { get; set; }
        public string Password { get; set; }
        public string? Contact { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }

		[DefaultValue(true)]
		public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

		[ForeignKey("CandidateId")]
		public User? User { get; set; }
		public virtual ICollection<CandidateCourse> CandidateCourse { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<TrainerCourse> TrainerCourse { get; set; }
    }
}
