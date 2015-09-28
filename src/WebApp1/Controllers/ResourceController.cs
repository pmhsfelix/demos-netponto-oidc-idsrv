using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace WebApp1.Controllers
{
    [HostAuthentication("Bearer")]
    public class ResourceController : ApiController
    {
        public ResourceModel Get()
        {
            var identity = User.Identity as ClaimsIdentity;
            return new ResourceModel
            {
                Claims = identity.Claims.Select(c => new ClaimModel
                {
                    Type = c.Type,
                    Value = c.Value,
                }),
            };
        }
    }

    public class ResourceModel
    {
        public IEnumerable<ClaimModel> Claims { get; set; } 
    }

    public class ClaimModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }

}
