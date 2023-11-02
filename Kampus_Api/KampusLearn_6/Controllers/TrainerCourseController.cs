 using CaseStudyKampusLearnAPI.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Serilog;

namespace CaseStudyKampusLearnAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TrainerCourseController : ControllerBase
	{

		private readonly KampusLearnContext repo;
		private readonly ILogger<TrainerCourse> logger;
		public TrainerCourseController(KampusLearnContext repo, ILogger<TrainerCourse> logger)
		{
			this.repo = repo;
			this.logger = logger;
		}

		//Getting Course by Trainer id
		[HttpGet("GetCourseOfTrainer/{trainerId}")]
		public IActionResult GetCourseOfTrainer(int trainerId)
		{
			try
			{
				List<TrainerCourse> trainercourse = repo.TrainerCourse.ToList();
				List<Trainer> trainer = repo.Trainer.ToList();
				List<Admin> admin = repo.Admin.ToList();
				List<Course> course = repo.Course.ToList();
				var id = trainercourse.FindAll(x => x.TrainerId == trainerId);
				if (id != null)
				{
					logger.LogInformation("Course added to trainers and the details are listed");
					return StatusCode(200, id);
				}
				else
				{
					logger.LogWarning("no trainer with courses found");
					return StatusCode(404, "Course not found");
				}
			}
			catch (NullReferenceException ex)
			{
				logger.LogWarning("Data not found" + ex.Message);
				return StatusCode(404, "Data Not Found");
			}
			catch (Exception ex)
			{
				logger.LogWarning("Contact to Admin.. Server Error" + ex.Message);
				return StatusCode(500, "Internal Server Error");
			}

		}
		//Mark Attendance
		[HttpPost("MarkAttendance/{courseId}/{trainerId}")]
		public IActionResult MarkAttendance(int courseId, int trainerId, [FromBody] Progress progress)
		{
			progress.CourseId = courseId;
			progress.TrainerId = trainerId;
			List<Progress> progressList = repo.Progress.ToList();
			List<Course> course = repo.Course.ToList();
			List<CandidateCourse> candicourse =repo.CandidateCourse.ToList();
			List<TrainerCourse> trainercourse = repo.TrainerCourse.ToList();
			var candicourseid = candicourse.FindAll(x => x.CourseId == courseId).ToList();
			var trainercourseid = trainercourse.FindAll(x => x.CourseId == courseId).ToList();
			var id = progressList.Find(x => x.CourseId == courseId && x.Attendance == progress.Attendance);
			var courseid = course.Find(x => x.CourseId == courseId);
			if (id == null)
			{
				repo.Progress.Add(progress);
				repo.SaveChanges();

				int count = (int)courseid.Progress;
				
				if (count == (courseid.DurationInHours - 1))
				{
					count += 1;
					//percentage = (count * 100) / courseid.DurationInHours;

				}
				else
				{
					count += 2;
					//percentage = (count * 100) / courseid.DurationInHours;
				}

				courseid.Progress = count;
				if (courseid.Progress == courseid.DurationInHours)
				{
					foreach (var item in candicourseid)
					{
						item.Status = "Completed";
					}
					foreach (var item in candicourseid)
					{
						item.Status = "Completed";
					}
					foreach (var item in trainercourseid)
					{
						item.Status = "Completed";
					}
					foreach (var item in trainercourseid)
					{
						item.Status = "Completed";
					}
				}
				repo.SaveChanges();
				return Created("Attendance marked", "Attendance marked");
			}
			else
			{
				return Created("Attendance already marked", "Attendance already marked");
			}
			
		}


		//Get participants by course
		[HttpGet("GetParticipants/{courseId}")]
		public IActionResult GetParticipantByCourse(int courseId)
		{
			try
			{
				List<Trainer> trainer = repo.Trainer.ToList();
				List<Candidate> candidate = repo.Candidate.ToList();
				List<Course> course = repo.Course.ToList();
				List<Payment> payment = repo.Payment.ToList();
				var participants = payment.FindAll(x => x.CourseId==courseId).ToList();

				if (participants != null)
				{
					logger.LogInformation("Participants details are listed");
					return StatusCode(200, participants);
				}
				else
				{
					logger.LogWarning("no particpants with courses found");
					return StatusCode(404, "particpants not found");
				}

			}
			catch(Exception ex)
			{
				logger.LogWarning("Contact to Admin.. Server Error" + ex.Message);
				return StatusCode(500, "Internal Server Error");
			}
		}

		//Delete
		[HttpDelete("DeleteCourseAndTrainer/{id}")]
		public IActionResult DeleteCourseAndTrainer(int id)
		{
			try
			{
				TrainerCourse trainerCourse= repo.TrainerCourse.Find(id);
				if (trainerCourse != null)
				{
					trainerCourse.IsActive = false;
					repo.SaveChanges();
					logger.LogInformation("Trainer and Course details are deletd successfully");
					return Ok("Trainer and Course deleted successfully");
				}
				else
				{
					logger.LogWarning("Details not found. Try again.");
					return StatusCode(404, "Course not found");
				}
			}
			catch (NullReferenceException ex)
			{
				logger.LogWarning("Data not found" + ex.Message);
				return StatusCode(404, "Data Not Found");
			}
			catch (Exception ex)
			{
				logger.LogWarning("Contact to Admin.. Server Error" + ex.Message);
				return StatusCode(500, "Internal Server Error");
			}

		}
		

	}
}
