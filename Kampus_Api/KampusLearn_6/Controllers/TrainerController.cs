﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using CaseStudyKampusLearnAPI.Models;
using CaseStudyKampusLearnAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;


namespace CaseStudyKampusLearnAPI.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class TrainerController : ControllerBase
	{
		private readonly KampusLearnContext repo;
		private readonly IJWTManagerRepository jWTManagerRepository;
		private readonly ILogger<Trainer> logger;
		public TrainerController(KampusLearnContext repo, IJWTManagerRepository jWTManagerRepository, ILogger<Trainer> logger)
		{
			this.repo = repo;
			this.jWTManagerRepository = jWTManagerRepository;
			this.logger = logger;
		}

		//Validating Trainer
		[AllowAnonymous]
		[HttpPost("ValidatingTrainer")]
		public IActionResult ValidatingTrainer([FromBody] Trainer trainerObj)
		{
			try
			{
				List<Trainer> trainer = repo.Trainer.ToList();
				Trainer id = trainer.Find(e => e.Email == trainerObj.Email && e.Password == trainerObj.Password);
				if (id != null)
				{
					var jwt = jWTManagerRepository.LoginTrainer(id.Email, id.Password);
					Log.Information("Trainer added successfully and jwt token created");
					return Ok(new { jwt, id });
				}
				else
				{
					Log.Warning("Trainer details not found");
					return StatusCode(404, "Incorrect email or password");
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

		[AllowAnonymous]
		//Getting Trainer Details
		[HttpGet("GetTrainer")]
		public IActionResult getTrainer()
		{
			try
			{
				List<Trainer> trainer = repo.Trainer.ToList();
				List<Admin> adminlist = repo.Admin.ToList();
				var admintrainer = from trainee in trainer
								   join admin in adminlist
								   on trainee.AdminId equals admin.AdminId
								   into admintrainerjoin
								   from admins in admintrainerjoin.DefaultIfEmpty()
								   select new { trainee, admins };

				if (trainer.Count != 0)
				{
					Log.Information("Trainers details are listed");
					return StatusCode(200, admintrainer.Where(status => status.trainee.IsActive == true));
				}
				else
				{
					Log.Warning("No trainer added");
					return StatusCode(204, "No content");
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
		//Getting Trainer by id
		[HttpGet("GetTrainerbyId")]
		public IActionResult GetTrainer()
		{
			try
			{
				int trainerId = Convert.ToInt32(HttpContext.User.FindFirstValue("trainerId"));
				Trainer trainer = repo.Trainer.Find(trainerId);
				if (trainer != null)
				{
					Log.Information("Trainers details are listed with id : " + trainerId);
					return StatusCode(200, trainer);
				}
				else
				{
					Log.Warning("No trainer found with this id");
					return StatusCode(404, "Trainer not found");
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

		//Update Trainer
		[HttpPut("UpdateTrainer")]
		public IActionResult UpdateTrainer([FromBody] Trainer trainerobj)
		{
			try
			{
				int trainerId = Convert.ToInt32(HttpContext.User.FindFirstValue("trainerId"));
				Trainer trainer = repo.Trainer.Find(trainerId);
				if (trainer != null)
				{
					trainer.Name = trainerobj.Name;
					trainer.Password = trainerobj.Password;
					trainer.Contact = trainerobj.Contact;
					trainer.Email = trainerobj.Email;
					trainer.Address = trainerobj.Address;
					trainer.YearOfExperience = trainerobj.YearOfExperience;
					trainer.Qualification = trainerobj.Qualification;
					trainer.CreatedAt = trainer.CreatedAt;
					trainer.IsActive = true;
					trainer.UpdatedAt = DateTime.Now;
					repo.SaveChanges();
					Log.Information("Trainers details updated successfully");
					return StatusCode(200, "Trainer updated successfully");
				}
				else
				{
					Log.Warning("Trainer details not found");
					return StatusCode(404, "Trainer not found");
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

		//Delete Trainer
		[HttpDelete("DeleteTrainer/{trainerId}")]
		public IActionResult DeleteTrainer(int trainerId)
		{
			try
			{
				//int trainerId = Convert.ToInt32(HttpContext.User.FindFirstValue("trainerId"));
				Trainer trainer = repo.Trainer.Find(trainerId);

				if (trainer != null)
				{
					trainer.IsActive = false;
					repo.SaveChanges();
					Log.Information("Trainer details deleted");
					return Ok("Trainer deleted successfully");
				}
				else
				{
					Log.Warning("no such Trainer available");
					return StatusCode(404, "Trainer not found");
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

		[AllowAnonymous]
		//Trainer who are not active
		[HttpGet("GetTrainerNotActive")]
		public IActionResult GetTrainerNotActive()
		{
			try
			{
				List<Trainer> trainer = repo.Trainer.ToList();
				if (trainer.Count != 0)
				{
					Log.Information("Trainer who are not active are listed");
					return StatusCode(200, trainer.Where(status => status.IsActive == false));
				}
				else
				{
					Log.Information("No trainer availble whor are not active");
					return StatusCode(404, "No not active trainers available");
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

	}
}
