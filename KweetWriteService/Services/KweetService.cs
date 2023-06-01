using KweetService.RabbitMq;
using KweetWriteService.Data;
using KweetWriteService.Models;
using Microsoft.EntityFrameworkCore;

namespace KweetWriteService.Services
{
    public class KweetService : IKweetService
    {
        private readonly KweetDBContext _dbContext;

        public KweetService(KweetDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<KweetDTO> CreateKweet(Kweet kweet)
        {
            var created = new KweetDTO();
            try
            {
                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Kweet> entry = _dbContext.Kweets.Add(kweet);
                await _dbContext.SaveChangesAsync();

                created.Id = entry.Property(e => e.Id).OriginalValue;
                created.Text = entry.Property(e => e.Text).OriginalValue;
                created.UserId = entry.Property(e => e.UserId).OriginalValue;
                created.EventType = EventTypes.Created;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return created;
        }

        public async Task<bool> DeleteKweet(long id)
        {
            try
            {
                _dbContext.Remove(_dbContext.Kweets.Single(k => k.Id == id));
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<KweetDTO> UpdateKweet(Kweet kweet)
        {
            try
            {
                Kweet? kweetBefore = _dbContext.Kweets.Find(kweet.Id);
                kweetBefore.Text = kweet.Text;

                _dbContext.Kweets.Update(kweetBefore);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return new KweetDTO { Id = kweet.Id, Text = kweet.Text, UserId = kweet.UserId, EventType = EventTypes.Updated };
        }
    }
}
