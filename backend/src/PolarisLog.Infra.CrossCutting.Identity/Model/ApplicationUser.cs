using System;
using Microsoft.AspNetCore.Identity;

namespace PolarisLog.Infra.CrossCutting.Identity.Model
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Nome { get; set; }
    }
}