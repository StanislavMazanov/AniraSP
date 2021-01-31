using System.Threading.Tasks;
using AniraSP.DAL.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AniraSP.DAL.Repository {
    public class OffersRepository {
        private readonly AniraSpDbContext _aniraSpDbContext;

        public OffersRepository(AniraSpDbContext aniraSpDbContext) {
            _aniraSpDbContext = aniraSpDbContext;
        }

        public async Task<Offer> Add(Offer offer) {
            EntityEntry<Offer> t = await _aniraSpDbContext.Offers.AddAsync(offer);
            await _aniraSpDbContext.SaveChangesAsync();
            return t.Entity;
        }
    }
}