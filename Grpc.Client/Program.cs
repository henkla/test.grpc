using Grpc.Net.Client;
using Grpc.Server;
using System;
using System.Threading.Tasks;

namespace Grpc.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            // generatePassword
            var generateRequest = new GenerateRequest
            {
                PasswordLength = 12,
                Casing = Casing.Mixed,
                UseSpecials = false,
                UseNumerics = false
            };
            var passwordGeneratorClient = new Generator.GeneratorClient(channel);
            var generateReply = await passwordGeneratorClient.GenerateAsync(generateRequest);

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                generateReply = await passwordGeneratorClient.GenerateAsync(generateRequest);
                Console.Clear();
                Console.WriteLine($"Generated password is:\n{generateReply.GeneratedPassword}");
            }
        }
    }
}
