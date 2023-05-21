﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.WebFresher2023.Demo.Common.MyException
{
    public class BaseException
    {
        public int ErrorCode { get; set; }
        public string? UserMessage { get; set; }
        public string? DevMessage { get; set; }
        public string? TraceId { get; set; }
        public string? MoreInfo { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
