using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Grpc.Server
{
    public class GeneratorService : Generator.GeneratorBase
    {
        private readonly ILogger<GeneratorService> _logger;
        private readonly SimplePasswordGenerator.Generator _generator;

        public GeneratorService(ILogger<GeneratorService> logger)
        {
            _logger = logger;
            _generator = new SimplePasswordGenerator.Generator();
        }

        public override Task<GenerateReply> Generate(GenerateRequest request, ServerCallContext context)
        {
            var password = _generator.Generate(passwordLength: request.PasswordLength,
                                               casing: (SimplePasswordGenerator.Casing)request.Casing,
                                               useSpecials: request.UseSpecials,
                                               useNumerics: request.UseNumerics,
                                               filter: request.Filter);

            return Task.FromResult(new GenerateReply { GeneratedPassword = password });
        }

    }
}
