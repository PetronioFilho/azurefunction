using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Company.Function
{
    public class FunctionsCelsiusParaFahrenheit
    {
        private readonly ILogger<FunctionsCelsiusParaFahrenheit> _logger;

        public FunctionsCelsiusParaFahrenheit(ILogger<FunctionsCelsiusParaFahrenheit> log)
        {
            _logger = log;
        }

        //Nome que vai aparecer no Swagger
        [FunctionName("ConverterCelsiusParaFahrenheit")]
        //Label da função - Conversão
        [OpenApiOperation(operationId: "Run", tags: new[] { "Conversão" })]
        //Nome dos parâmetros 
        [OpenApiParameter(name: "celsius", In = ParameterLocation.Path, Required = true, Type = typeof(double), Description = "O valor em  **Celsius** para conversão em Celsius")]
        //Retorno
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "O valor em Celsius")]
        //Parâmetros que serão recebidos
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ConverterCelsiusParaFahrenheit/{celsius}")] HttpRequest req, double celsius)
        {
            _logger.LogInformation($"Parâmetro recebido :{celsius}", celsius);

            var valorEmFahrenheit = ((celsius * 9) / 5) + 32;

            string responseMessage = $"O valor em Celsius {celsius} em Fahrenheit é: {valorEmFahrenheit}";

            return new OkObjectResult(responseMessage);
        }
    }
}

