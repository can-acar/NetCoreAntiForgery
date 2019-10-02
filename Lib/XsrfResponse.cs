using System;

namespace Lib
{
    public class XsrfResponse
    {
        public long Ticks { get; set; } = DateTime.Now.Ticks;
        public bool Status { get; set; }
        public string Token { get; set; }
        public string TokenName { get; set; }
    }
}