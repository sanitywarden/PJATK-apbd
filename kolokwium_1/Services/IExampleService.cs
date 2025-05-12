using Microsoft.AspNetCore.Mvc;

namespace kolokwium_1.Services;

public interface IExampleService
{
    // Example get
    public Task<IActionResult> GetSomethingAsync();
}