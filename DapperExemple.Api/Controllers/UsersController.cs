using DapperExemple.Data.Repository;
using DapperExemple.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace DapperExemple.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync() =>
            Ok(await _repository.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync([BindRequired] int id) =>
            Ok(await _repository.GetAsync(id));

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] User request)
        {
            var result = await _repository.AddAsync(request);

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync([BindRequired] int id, [FromBody] User request)
        {
            var result = await _repository.UpdateAsync(request);

            return Ok(result);
        }
    }
}

//var propertiesList = new List<string>();

//foreach (PropertyInfo prop in request.GetType().GetProperties())
//{
//    var notMapped = typeof(User).GetProperty(prop.Name).GetCustomAttributes(typeof(NotMappedAttribute), false);
//    if (notMapped.Length == 0)
//        propertiesList.Add(prop.Name);
//}

//var final = propertiesList;