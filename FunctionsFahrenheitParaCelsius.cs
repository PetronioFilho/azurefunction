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
    public class FunctionsFahrenheitParaCelsius
    {
        private readonly ILogger<FunctionsFahrenheitParaCelsius> _logger;

        public FunctionsFahrenheitParaCelsius(ILogger<FunctionsFahrenheitParaCelsius> log)
        {
            _logger = log;
        }

        //Nome que vai aparecer no Swagger
        [FunctionName("ConverterFahrenheitParaCelsius")]
        //Label da função - Conversão
        [OpenApiOperation(operationId: "Run", tags: new[] { "Conversão" })]
        //Nome dos parâmetros 
        [OpenApiParameter(name: "fahrenheit", In = ParameterLocation.Path, Required = true, Type = typeof(double), Description = "O valor em  **Fahrenheit** para conversão em Celsius")]
        //Retorno
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "O valor em Celsius")]
        //Parâmetros que serão recebidos
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ConverterFahrenheitParaCelsius/{fahrenheit}")] HttpRequest req, double fahrenheit)
        {
            _logger.LogInformation($"Parâmetro recebido :{fahrenheit}", fahrenheit);

            var valorEmCelsius = (fahrenheit - 32) * 5 / 9;

            string responseMessage = $"O valor em Fahrenheit {fahrenheit} em Celsius é: {valorEmCelsius}";

            return new OkObjectResult(responseMessage);
        }
    }
}

