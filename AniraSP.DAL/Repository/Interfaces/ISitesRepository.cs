using System.Collections.Generic;
using System.Threading.Tasks;
using AniraSP.DAL.Domain;
using AniraSP.DAL.Domain.Sites;

namespace AniraSP.DAL.Repository.Interfaces {
    public interface ISitesRepository {
        Task<Site> FindAsync(int id);
        Site Find(int id);
        Task<IEnumerable<Site>> GetSites();
        Task SaveSiteAsync(Site siteDb);
        Task<Site> AddAsync(Site siteDb);
    }
}