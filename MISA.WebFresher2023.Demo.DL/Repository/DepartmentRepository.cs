using Microsoft.Extensions.Configuration;
using MISA.WebFresher2023.Demo.DL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
