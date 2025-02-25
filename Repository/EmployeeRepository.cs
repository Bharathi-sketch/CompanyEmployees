using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public IEnumerable<Employee> GetEmployees(Guid CompanyId, bool trackChanges) =>
            FindByCondition(e => e.CompanyID.Equals(CompanyId), trackChanges)
            .OrderBy(e => e.Name).ToList();
    }
}
