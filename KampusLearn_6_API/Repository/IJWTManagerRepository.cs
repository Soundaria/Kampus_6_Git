using CaseStudyKampusLearnAPI.Models;

namespace CaseStudyKampusLearnAPI.Repository
{
	public interface IJWTManagerRepository
	{


		string LoginAdmin(string email,string password);
		string LoginTrainer(string email, string password);
		string LoginCandidate(string email, string password);

	}
}
