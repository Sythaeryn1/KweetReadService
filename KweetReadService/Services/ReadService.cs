using KweetReadService.Data;
using KweetReadService.Models;
using KweetService.RabbitMq;

namespace KweetReadService.Services
{
    public class ReadService : IReadService
    {
        private readonly IMongoRepository _mongoRepository;

        public ReadService(IMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task Write(KweetDTO dto)
        {
            if (dto.EventType == EventTypes.Created)
            {
                await WriteCreated(new Kweet { Id = dto.Id, Text = dto.Text, UserId = dto.UserId });
            }
            else if (dto.EventType == EventTypes.Updated)
            {
                await WriteUpdated(new Kweet { Id = dto.Id, Text = dto.Text, UserId = dto.UserId });
            }
            else if(dto.EventType == EventTypes.Deleted)
            {
                await WriteDeleted(dto.Id);
            }

        }
        private async Task WriteCreated(Kweet kweet)
        {
            try
            {
                await _mongoRepository.InsertKweet(kweet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task WriteUpdated(Kweet kweet)
        {
            try
            {
                await _mongoRepository.UpdateKweet(kweet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task WriteDeleted(long id)
        {
            try
            {
                await _mongoRepository.DeleteKweetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
