using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Repositories.Company;

namespace MeetingReservation.Infrastructure.DataAccess.Repositories;

public class CompanyRepository : ICompanyWriteOnlyRepository
{
	private readonly MeetingReservationDbContext _dbContext;

	public CompanyRepository(MeetingReservationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task Add(Company company) => await _dbContext.AddAsync(company);
}
