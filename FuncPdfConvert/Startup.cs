using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FuncPdfConvert.Startup))]
namespace FuncPdfConvert
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            _ = builder.Services.AddOptions<Options>().Configure<IConfiguration>((sett, conf) =>
            {
                conf.GetSection("ADConfig").Bind(sett);
            });
        }
    }
}
