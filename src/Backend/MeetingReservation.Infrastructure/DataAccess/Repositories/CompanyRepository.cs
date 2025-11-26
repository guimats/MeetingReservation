using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Repositories.Company;
using Microsoft.EntityFrameworkCore;

namespace MeetingReservation.Infrastructure.DataAccess.Repositories;

public class CompanyRepository : ICompanyWriteOnlyRepository, ICompanyReadOnlyRepository
{
	private readonly MeetingReservationDbContext _dbContext;

	public CompanyRepository(MeetingReservationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task Add(Company company) => await _dbContext.Companies.AddAsync(company);

	public async Task<Company?> GetById(long id) => await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);
}
