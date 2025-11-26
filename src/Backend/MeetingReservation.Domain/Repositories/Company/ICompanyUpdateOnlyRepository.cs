namespace MeetingReservation.Domain.Repositories.Company;

public interface ICompanyUpdateOnlyRepository
{
	public Task<Entities.Company?> GetById(long id);

	public void Update(Entities.Company company);
}
