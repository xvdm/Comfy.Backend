using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comfy.Domain.Base
{
    public abstract class Auditable
    {
        public DateTime? CreatedAt { get; set; }
    }
}
