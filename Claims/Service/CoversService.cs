using Microsoft.EntityFrameworkCore;

using Claims.Repository;
using Claims.Contrib;
using Claims.Auditing;

namespace Claims.Service
{
    public class CoversService : ICoversService
    {

        private readonly ClaimsContext _claimsContext;
        private readonly Auditer _auditer;

        public CoversService(
          ClaimsContext claimsContext,
          AuditContext auditContext
          )
        {
            _claimsContext = claimsContext;
            _auditer = new Auditer(auditContext);
        }


        
        public async Task<Cover?> CreateAsync(Interchange.Cover Δ)
        {

          //#NPE
          await _auditer.AuditCover(Δ.Id!, "POST");
          
            var cover = Builder.Cover.Init()
            .DTO(Δ)?
            .StartDate(Δ.StartDate)?
            .EndDate(Δ.EndDate)?
            .Type(Δ.Type)?
            .Premium()?
            .Build ?? null;

            if (cover is null)
            {
                //throw new Exception("Some operation failed when creating a Cover.");
                return null;
            }

            return await _claimsContext.AddItemAsync(cover);
        }

        public async Task<Result> DeleteAsync()
        {
            return await _claimsContext.DeleteCoversAsync();
        }

        public async Task<IEnumerable<Cover>> GetAsync()
        {
            var results = await _claimsContext.Covers.ToListAsync();
            return results;
        }

        public async Task<IEnumerable<Cover>> GetAsync(string id)
        {
            var results = await _claimsContext.Covers.Where(cover => cover.Id == id).ToListAsync();
            return results;
        }


        public async Task DeleteAsync(string id)
        {
            await _auditer.AuditCover(id, "DELETE");
            var cover = await _claimsContext.Covers.Where(cover => cover.Id == id).SingleOrDefaultAsync();
            if (cover is not null)
            {
                _claimsContext.Covers.Remove(cover);
                await _claimsContext.SaveChangesAsync();
            }
        }
    }
}
