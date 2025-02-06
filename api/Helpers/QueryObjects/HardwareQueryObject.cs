using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers.QueryObjects
{
    public class HardwareQueryObject
    {
        public int? hardwarestatusid { get; set; }
        public int? typeid { get; set; }
        public bool IsDecsending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}