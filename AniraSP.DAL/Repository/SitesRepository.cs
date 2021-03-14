using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AniraSP.DAL.Domain;
using AniraSP.DAL.Domain.Sites;
using AniraSP.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AniraSP.DAL.Repository {
    public class SitesRepository : ISitesRepository {
        private readonly AniraSpDbContext _aniraSpDbContext;

        public SitesRepository(AniraSpDbContext aniraSpDbContext) {
            _aniraSpDbContext = aniraSpDbContext;
        }

        public async Task<Site> FindAsync(int id) {
            return await _aniraSpDbContext.Sites.FindAsync(id);
        }

        public Site Find(int id) {
            return _aniraSpDbContext.Sites.Find(id);
        }

        public async Task<IEnumerable<Site>> GetSites() {
            return await _aniraSpDbContext.Sites.ToListAsync();
        }

        public async Task SaveSiteAsync(Site site) {
            Site dbVal = await FindAsync(site.Id);
            if (dbVal != null) {
                _aniraSpDbContext.Entry(dbVal).State = EntityState.Detached;
            }

            _aniraSpDbContext.Entry(site).State = EntityState.Modified;
            await _aniraSpDbContext.SaveChangesAsync();
        }

        public async Task<Site> AddAsync(Site site) {
            await _aniraSpDbContext.Sites.AddAsync(site);
            await _aniraSpDbContext.SaveChangesAsync();
            return site;
        }
    }
}