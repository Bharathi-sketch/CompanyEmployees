using Shared.DataTransferObjects;


namespace Service.Contracts
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
        CompanyDto GetCompany(Guid CompanyId, bool trackChanges);
    }
}
