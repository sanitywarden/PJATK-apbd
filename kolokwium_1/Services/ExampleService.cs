using Microsoft.AspNetCore.Mvc;

namespace kolokwium_1.Services;

public class ExampleService : IExampleService
{
    public ExampleService() {}
    
    public Task<IActionResult> GetSomethingAsync()
    {
        return Task.FromResult<IActionResult>(new OkObjectResult("Some return value"));
    }
}