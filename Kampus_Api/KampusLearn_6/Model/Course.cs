﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CaseStudyKampusLearnAPI.Models
{
    public partial class Course
    {
        public Course()
        {
            CandidateCourse = new HashSet<CandidateCourse>();
            Payment = new HashSet<Payment>();
            ProgressNavigation = new HashSet<Progress>();
            Scheduling = new HashSet<Scheduling>();
            TrainerCourse = new HashSet<TrainerCourse>();
        }

        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? CourseCategory { get; set; }
        public int? Price { get; set; }

        [DefaultValue(true)]
        public bool? IsActive { get; set; }
        public int? AdminId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int DurationInHours { get; set; }
        public int? Totalparticipant { get; set; }
        public string? Time { get; set; }
        public string? Weekday { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public int? Progress { get; set; }

        public virtual Admin? Admin { get; set; }
        public virtual ICollection<CandidateCourse> CandidateCourse { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<Progress> ProgressNavigation { get; set; }
        public virtual ICollection<Scheduling> Scheduling { get; set; }
        public virtual ICollection<TrainerCourse> TrainerCourse { get; set; }
    }
}
