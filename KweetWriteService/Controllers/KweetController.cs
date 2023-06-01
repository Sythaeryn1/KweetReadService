using KweetService.RabbitMq;
using KweetWriteService.Models;
using KweetWriteService.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KweetWriteService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KweetController : ControllerBase
    {
        private readonly IKweetService _kweetService;
        private readonly IPublishEndpoint _publishEndpoint;

        public KweetController(IKweetService kweetService, IPublishEndpoint publishEndpoint)
        {
            _kweetService = kweetService;
            _publishEndpoint = publishEndpoint;
        }
        // GET: api/<KweetController>
        //[HttpGet]
        //public async Task<ActionResult<List<KweetDTO>> GetAllKweets()
        //{

        //}

        // POST api/<KweetController>
        [HttpPost]
        public async Task<ActionResult<KweetDTO>> CreateKweet([FromBody] Kweet kweet)
        {
            try
            {
                KweetDTO response = await _kweetService.CreateKweet(kweet);

                //kijk naar verbinding met rabbitmq daar gaat t mis
                await _publishEndpoint.Publish<KweetDTO>(new
                {
                    response.Id,
                    response.Text,
                    response.UserId,
                    response.EventType,
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // PUT api/<KweetController>/5
        [HttpPut]
        public async Task<ActionResult<KweetDTO>> UpdateKweet([FromBody] Kweet kweet)
        {
            try
            {
                KweetDTO response = await _kweetService.UpdateKweet(kweet);

                await _publishEndpoint.Publish<KweetDTO>(new
                {
                    response.Id,
                    response.Text,
                    response.UserId,
                    response.EventType
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // DELETE api/<KweetController>/5
        [HttpDelete]
        public async Task<ActionResult> Delete(Kweet kweet)
        {
            try
            {
                await _kweetService.DeleteKweet(kweet.Id);

                await _publishEndpoint.Publish<KweetDTO>(new
                {
                    kweet.Id,
                    kweet.Text,
                    kweet.UserId,
                    EventTypes.Deleted
                });
                return Ok();
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
