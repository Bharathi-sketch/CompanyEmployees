using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    public class EmployeeController: ControllerBase
    {
        private readonly IServiceManager _service;
        public EmployeeController(IServiceManager service)
        {
            _service = service;
        }
    }
}
