using Microsoft.AspNetCore.Mvc;
using PersonService.Repositories;

namespace PersonService;

[ApiController]
[Route("api/people")]
public class PersonController : ControllerBase
{
    readonly IRepository _repository;

    public PersonController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_repository.GetPeople());
    }
    
}