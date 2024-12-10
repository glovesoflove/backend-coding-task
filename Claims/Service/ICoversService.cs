using Claims.Contrib;

namespace Claims.Service
{
    public interface ICoversService
    {
        Task<Cover?> CreateAsync(Interchange.Cover Δ);
        Task<Result> DeleteAsync();
        Task DeleteAsync(string id);
        Task<IEnumerable<Cover>> GetAsync();
        Task<IEnumerable<Cover>> GetAsync(string id);
    }
}
