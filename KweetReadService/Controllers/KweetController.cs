using KweetReadService.Data;
using KweetReadService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KweetReadService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KweetController : ControllerBase
    {
        private readonly IMongoRepository _repository;

        public KweetController(IMongoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<KweetController>
        [HttpGet]
        public async Task<ActionResult<List<Kweet>>> GetAll()
        {
            try
            {
                List<Kweet> res = new List<Kweet>();
                res = await _repository.GetAllKweets();
                if (res.Count == 0) throw new Exception();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }

        // GET api/<KweetController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kweet>> GetKweet(long id)
        {
            try
            {
                Kweet res = new Kweet();
                res = await _repository.GetKweetById(id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<List<Kweet>>> GetByUser(string name)
        {
            try
            {
                List<Kweet> res = new List<Kweet>();
                res = await _repository.GetKweetsByUserId(_repository.GetUser(name).Id);
                if (res.Count == 0) throw new Exception();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
