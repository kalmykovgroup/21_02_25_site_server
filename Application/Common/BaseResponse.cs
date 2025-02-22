using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class BaseResponse
    {
        public bool Success { get; set; } = false;
        public string? Error { get; set; } = null;
    }
}
