using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth2.Models.Result
{
    public class ResultLoginDto: ResultDto
    {
        public string Token { get; set; }
    }
}
