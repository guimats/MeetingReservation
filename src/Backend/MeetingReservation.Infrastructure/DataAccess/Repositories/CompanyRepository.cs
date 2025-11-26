using MeetingReservation.Domain.Entities;
using MeetingReservation.Domain.Repositories.Company;
using Microsoft.EntityFrameworkCore;

namespace MeetingReservation.Infrastructure.DataAccess.Repositories;

public class CompanyRepository : ICompanyWriteOnlyRepository, ICompanyReadOnlyRepository, ICompanyUpdateOnlyRepository
{
	private readonly MeetingReservationDbContext _dbContext;

	public CompanyRepository(MeetingReservationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task Add(Company company) => await _dbContext.Companies.AddAsync(company);

	async Task<Company?> ICompanyReadOnlyRepository.GetById(long id) => await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);

	async Task<Company?> ICompanyUpdateOnlyRepository.GetById(long id) => await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);

	public void Update(Company company) => _dbContext.Companies.Update(company);

}
