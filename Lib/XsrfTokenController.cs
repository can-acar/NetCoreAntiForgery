using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lib
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/xsrf")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class XsrfTokenController : ControllerBase
    {
        private readonly IAntiforgery Antiforgery;

        public XsrfTokenController(IAntiforgery antiforgery)
        {
            Antiforgery = antiforgery;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<XsrfResponse>> Get()
        {
            var Tokens = await Task.FromResult(Antiforgery.GetAndStoreTokens(HttpContext));


            return Ok(new XsrfResponse
            {
                Ticks = DateTime.Now.Ticks,
                Status = true,
                Token = Tokens.RequestToken,
                TokenName = Tokens.HeaderName
            });
        }
    }
}