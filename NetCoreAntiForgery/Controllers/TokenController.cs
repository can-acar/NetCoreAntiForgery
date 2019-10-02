using Lib;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAntiForgery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : XsrfTokenController
    {
        public TokenController(IAntiforgery antiforgery) : base(antiforgery)
        {
        }
    }
}