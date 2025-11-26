namespace MeetingReservation.Domain.Repositories.Company;

public interface ICompanyReadOnlyRepository
{
	public Task<Entities.Company?> GetById(long id); 
}
