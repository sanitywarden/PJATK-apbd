using kolokwium_1.Services;

namespace kolokwium_1.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ExampleController : ControllerBase
{
    private readonly IExampleService _exampleService;
    
    // Constructor needs to be public for dependency injection
    public ExampleController(IExampleService exampleService)
    {
        _exampleService = exampleService;
    }
    
    // Different methods (GET, POST, PUT) can be used with postman 
    
    // GET all
    // GET: api/example
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new[] { "value1", "value2" });
    }

    // GET by ID
    // parameter in HttpGet has to be the same name as the one in function
    // GET: api/example/<id>
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok($"GET You requested item {id}");
    }

    // POST to create a resource
    // POST: api/example
    [HttpPost]
    public IActionResult Create([FromBody] string value)
    {
        return Ok($"POST You requested item {value}");
    }

    // PUT to update a resource
    // PUT: api/example/<id>
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] string value)
    {
        return Ok("PUT something");
    }
}