using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using CaseStudyKampusLearnAPI.Models;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

namespace CaseStudyKampusLearnAPI.Repository
{
	public class JWTManagerRepository:IJWTManagerRepository
	{	
		private readonly KampusLearnContext repo;
		//private readonly JWTSettings jWTSettings;
		private readonly IConfiguration _configuration;
		public JWTManagerRepository(KampusLearnContext repo, IConfiguration _configuration)
		{
			this.repo = repo;
			this._configuration = _configuration;
		}


		public string LoginAdmin(string email,string password)
		{
			List<Admin> admin = repo.Admin.ToList();
			var user =  admin.Find(x=>x.Email==email && x.Password==password);

			if (user is null )
			{
				throw new ArgumentException($"Unable to authenticate user {email}");
			}

			var authClaims = new List<Claim>
		{
			new(ClaimTypes.Name, user.Name),
			new(ClaimTypes.Email, user.Email),
			new Claim("AdminId",user.AdminId.ToString()),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
		};

			var token = GetToken(authClaims);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public string LoginTrainer(string email, string password)
		{
			List<Trainer> trainer = repo.Trainer.ToList();
			var user = trainer.Find(x => x.Email == email && x.Password == password);

			if (user is null)
			{
				throw new ArgumentException($"Unable to authenticate user {email}");
			}

			var authClaims = new List<Claim>
		{
			new(ClaimTypes.Name, user.Name),
			new(ClaimTypes.Email, user.Email),
			new Claim("TrainerId",user.TrainerId.ToString()),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
		};

			var token = GetToken(authClaims);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public string LoginCandidate(string email, string password)
		{
			List<Candidate> candidate = repo.Candidate.ToList();
			var user = candidate.Find(x => x.Email == email && x.Password == password);

			if (user is null)
			{
				throw new ArgumentException($"Unable to authenticate user {email}");
			}

			var authClaims = new List<Claim>
		{
			new(ClaimTypes.Name, user.Name),
			new(ClaimTypes.Email, user.Email),
			new Claim("CandidateId",user.CandidateId.ToString()),
			new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
		};

			var token = GetToken(authClaims);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
		{
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

			var token = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				expires: DateTime.Now.AddHours(3),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

			return token;
		}

		//private string GetErrorsText(IEnumerable<IdentityError> errors)
		//{
		//	return string.Join(", ", errors.Select(error => error.Description).ToArray());
		//}


	
		//public string AuthenticateTrainer(string email, string password)
		//{
		//	List<Trainer> trainerRecords = repo.Trainer.ToList();
		//	var  trainer = trainerRecords.FirstOrDefault(x => x.Email == email && x.Password == password && x.IsActive == true);
		//	if (trainer == null)
		//	{
		//		return null;
		//	}

		//	// Else we generate JSON Web Token
		//	var tokenHandler = new JwtSecurityTokenHandler();
		//	var tokenKey = Encoding.UTF8.GetBytes(jWTSettings.Key);
		//	var tokenDescriptor = new SecurityTokenDescriptor
		//	{
		//		Subject = new ClaimsIdentity(new Claim[]
		//	  {
		//	 new Claim(ClaimTypes.Name, trainer.TrainerId.ToString()),
		//	  new Claim("TrainerId", trainer.TrainerId.ToString()),
		//	 new Claim(ClaimTypes.Version,"V3.1")
		//	  }),
		//		Expires = DateTime.UtcNow.AddMinutes(30),
		//		SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
		//	};
		//	var token = tokenHandler.CreateToken(tokenDescriptor);
		//	var GetToken = tokenHandler.WriteToken(token);


		//	return GetToken;
		//}

		//public string AuthenticateCandidate(string email, string password)
		//{
		//	List<Candidate> candidateRecords = repo.Candidate.ToList();
		//	var candidate = candidateRecords.FirstOrDefault(x => x.Email == email && x.Password == password && x.IsActive == true);
		//	if (candidate == null)
		//	{
		//		return null;
		//	}

		//	// Else we generate JSON Web Token
		//	var tokenHandler = new JwtSecurityTokenHandler();
		//	var tokenKey = Encoding.UTF8.GetBytes(jWTSettings.Key);
		//	var tokenDescriptor = new SecurityTokenDescriptor
		//	{
		//		Subject = new ClaimsIdentity(new Claim[]
		//	  {
		//	 new Claim(ClaimTypes.Name, candidate.CandidateId.ToString()),
		//	  new Claim("CandidateId", candidate.CandidateId.ToString()),
		//	 new Claim(ClaimTypes.Version,"V3.1")
		//	  }),
		//		Expires = DateTime.UtcNow.AddMinutes(30),
		//		SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
		//	};
		//	var token = tokenHandler.CreateToken(tokenDescriptor);
		//	var GetToken = tokenHandler.WriteToken(token);

		//	return GetToken;
		//}
	}
}
