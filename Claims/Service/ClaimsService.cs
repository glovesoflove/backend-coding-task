using Claims;
using Claims.Repository;
using Claims.Contrib;
using Claims.Auditing;

namespace Claims.Service
{
  public class ClaimsService : IClaimsService
    {
        private readonly ClaimsContext _claimsContext;
        private readonly Auditer _auditer;
        private readonly ICoversService _coversService;

        public ClaimsService(
          ClaimsContext claimsContext,
          AuditContext auditContext,
          ICoversService coversService
          )
        {
            _claimsContext = claimsContext;
            _auditer = new Auditer(auditContext);
            _coversService = coversService;
        }
        
        public async Task<Result?> AddItemAsync(Interchange.Claim Δ)
        {
          //1. Precheck Claim creation date is within the cover period. #Guard on NPE.
          var cover = (await _coversService.GetAsync(Δ.CoverId!)).FirstOrDefault();

          if(cover is null)
            throw new ArgumentException("Cover does not exist.", nameof(Δ));
            
          if (Δ.Created < cover.StartDate || Δ.Created > cover.EndDate)
            throw new ArgumentException("Claim creation date must be within the cover period.", nameof(Δ));
            
          //2. Create and store the Claim
          var claim = Builder.Claim.Init()
          .DTO(Δ)?
          .CoverId(Δ.CoverId)?
          .Created(Δ.Created)?
          .Name(Δ.Name)?
          .Type(Δ.Type)?
          .DamageCost(Δ.DamageCost)?
          .Build ?? null;

          if(claim is null)
          {
            //throw new Exception("Some operation failed when creating a Claim.");
            return null;
          }
          
          return await _claimsContext.AddItemAsync(claim);
        }
        
        public async Task<Result> DeleteItemAsync(string id)
        {
          await _auditer.AuditClaim(id, "DELETE");
          return await _claimsContext.DeleteItemAsync(id);
        }

        public async Task<Claim> GetClaimAsync(string id)
        {
          return await _claimsContext.GetClaimAsync(id);
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync()
        {
          return await _claimsContext.GetClaimsAsync();
        }
        
        public async Task<Result> DeleteAsync()
        {
          return await _claimsContext.DeleteAsync();
        }
    }
}
