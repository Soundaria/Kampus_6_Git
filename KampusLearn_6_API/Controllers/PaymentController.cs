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
	public class PaymentController : ControllerBase
	{
		private readonly ILogger<Payment> logger;
		private readonly KampusLearnContext repo;
		public PaymentController(KampusLearnContext repo, ILogger<Payment> logger)
		{
			this.repo = repo;
			this.logger = logger;
		}

		
		//Add Payment
		[HttpPost("AddPayment/{candidateId}")]
		public IActionResult AddPayment([FromBody] Payment payment,int candidateId)
		{
			try
			{
				List<Course> course = repo.Course.ToList();
				var courseId = course.Find(x => x.CourseId == payment.CourseId);
				var start = courseId.Startdate;
				if (DateTime.Compare((DateTime)start, DateTime.Now) <= 0)
				{
					List<Scheduling> scheduling = repo.Scheduling.ToList();
					
					List<Trainer> trainerList = repo.Trainer.ToList();
					var trainerSpecified =trainerList.Where(x=> x.Qualification.ToLower().Contains(courseId.CourseCategory.ToLower())).ToList();
					var trainerScheduled = scheduling.Where(x => x.CourseId == courseId.CourseId && x.Timing.ToLower() == courseId.Time.ToLower()).ToList();

					 void SchedulingTrainer(Trainer trainer)
					 {
						payment.IsActive = true;
						payment.CandidateId = candidateId;
						payment.TrainerId = trainer.TrainerId;
						payment.CreatedAt = DateTime.Now;
						repo.Payment.Add(payment);
						repo.SaveChanges();

						List<CandidateCourse> candidateCourse = repo.CandidateCourse.ToList();
						var id = candidateCourse.Find(x => x.CandidateId == candidateId && x.CourseId == payment.CourseId);
						id.IsPaymentDone = true;
						id.Status = "Active";
						repo.SaveChanges();

						Scheduling schedule = new Scheduling();
						schedule.CourseId = courseId.CourseId;
						schedule.Timing = courseId.Time;
						schedule.TrainerId = trainer.TrainerId;
						schedule.WeekDay = courseId.Weekday;
						repo.Scheduling.Add(schedule);
						repo.SaveChanges();

						TrainerCourse list = new TrainerCourse();
						list.TrainerId = trainer.TrainerId;
						list.CourseId = courseId.CourseId;
						list.CandidateId =payment.CandidateId;
						repo.TrainerCourse.Add(list);
						repo.SaveChanges();
						Log.Information("Payment Added details");
					 }

					if (trainerScheduled.Count==0)
					{	
						Trainer trainerSelected=null;
						int count = 0;
						int countScheduled = 0;
						foreach (var item in trainerSpecified)
						{
							var trainerInSchedule = scheduling.Find(x => x.TrainerId == item.TrainerId );
							if (trainerInSchedule == null)
							{
								trainerSelected = item;
								break ;
							}
							else
							{
								count += 1;
							}
						}
						if (count == trainerSpecified.Count)
						{
							foreach (var item in trainerSpecified)
							{
								var trainerInSchedule = scheduling.Find(x => x.TrainerId == item.TrainerId);
								if (trainerInSchedule != null && trainerInSchedule.Timing!= courseId.Time && trainerInSchedule.WeekDay!=courseId.Weekday)
								{
									trainerSelected = item;
									break;
								}
								else
								{
									countScheduled += 1;
								}
							}
						}
						if(countScheduled==trainerSpecified.Count)
						{
							return Created("Payment not done", new {});
						}
						else
						{
							SchedulingTrainer(trainerSelected);
							return Created("Payment added successfully",new {courseId.CourseName,trainerSelected.Name,courseId.Price,courseId.Time,courseId.Weekday,payment.IsActive,payment.PaymentId } );
						}
					}
					else 
					{
						Scheduling trainerMapped = trainerScheduled.Find(x => x.CourseId == courseId.CourseId && x.Timing == courseId.Time);
						Trainer trainer = trainerList.Find(x => x.TrainerId == trainerMapped.TrainerId);
						payment.IsActive = true;
						payment.CandidateId = candidateId;
						payment.TrainerId = trainer.TrainerId;
						payment.CreatedAt = DateTime.Now;
						repo.Payment.Add(payment);
						repo.SaveChanges();

						List<CandidateCourse> candidateCourse = repo.CandidateCourse.ToList();
						var id = candidateCourse.Find(x => x.CandidateId == candidateId && x.CourseId == payment.CourseId);
						id.IsPaymentDone = true;
						id.Status = "Active";
						repo.SaveChanges();
						Log.Information("Payment Added details");
						return Created("Payment added successfully", new { courseId.CourseName, trainer.Name, courseId.Price, courseId.Time, courseId.Weekday, payment.IsActive, payment.PaymentId });
					}
			
						

				
				}
				else if(DateTime.Compare((DateTime)start, DateTime.Now)> 0)
				{
					return StatusCode(200, "Registration date is not today");
				}
				else 
				{
					return StatusCode(200, "Registration date expired");
				}
			}
			catch (NullReferenceException ex)
			{
				Log.Warning("Data not found" + ex.Message);
				return StatusCode(404, "Data Not Found");
			}
			catch (Exception ex)
			{
				Log.Warning("Contact to Admin.. Server Error" + ex.Message);
				return StatusCode(500,ex);
			}
		}

		//Getting Payment Details

		[HttpGet("GetPayment")]
		public IActionResult GetPayment()
		{
			try
			{
				List<Candidate> candidate = repo.Candidate.ToList();
				List<Course> course = repo.Course.ToList();
 				List<Payment> payment = repo.Payment.ToList();
				if (payment.Count != 0)
				{
					Log.Information("Payment Added details are listed");
					return StatusCode(200, payment.Where(status => status.IsActive == true));
				}
				else
				{
					Log.Information("No Payment details are there in the listed");
					return StatusCode(204, "No Contetnt");
				}
			}
			catch (NullReferenceException ex)
			{
				Log.Warning("Data not found" + ex.Message);
				return StatusCode(404, "Data Not Found");
			}
			catch (Exception ex)
			{
				Log.Warning("Contact to Admin.. Server Error" + ex.Message);
				return StatusCode(500, "Internal Server Error");
			}
		}


		//Payment which are not active
		[HttpGet("GetPaymentNotActive")]
		public IActionResult GetPaymentNotActive()
		{
			try
			{
				List<Payment> payment = repo.Payment.ToList();
				//List<Candidate> candidate = repo.Candidate.ToList();
				//var candidateid = candidate.FindAll(x => x.IsActive == false);
				//foreach(var item in candidateid)
				//{
				//	var paymentid = repo.Payment.Find(item.CandidateId);
				//	if (paymentid != null)
				//	{
				//		paymentid.IsActive = false;
				//	}
				//}
				repo.SaveChanges();
				Log.Information("Payments for whcih course are completed");
				return StatusCode(200, payment.Where(status => status.IsActive == false));
			}
			catch (NullReferenceException ex)
			{
				Log.Warning("Data not found" + ex.Message);
				return StatusCode(404, "Data Not Found");
			}
			catch (Exception ex)
			{
				Log.Warning("Contact to Admin.. Server Error" + ex.Message);
				return StatusCode(500, "Internal Server Error");
			}
		}
	}
}
