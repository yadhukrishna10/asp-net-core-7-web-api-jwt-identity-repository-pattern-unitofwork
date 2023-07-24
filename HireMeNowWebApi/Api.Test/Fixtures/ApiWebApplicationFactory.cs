using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Test.Fixtures
{
    internal class ApiWebApplicationFactory :WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
           
            return base.CreateHost(builder);
        }
    }
}
