using Claims;
using Claims.Repository;
using Claims.Contrib;

namespace Claims.Service
{
    public interface IClaimsService
    {
        Task<Result?> AddItemAsync(Interchange.Claim Δ);
        Task<Result> DeleteItemAsync(string id);
        Task<Result> DeleteAsync();
        Task<Claim> GetClaimAsync(string id);
        Task<IEnumerable<Claim>> GetClaimsAsync();
    }
}
