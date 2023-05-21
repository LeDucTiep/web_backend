using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.DL.Entity
{
    public class Position : BaseEntity
    {
        public Guid PositionId { get; set; }
        public string? PositionName { get; set; }
    }
}
