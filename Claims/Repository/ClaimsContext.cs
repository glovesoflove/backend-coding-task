using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

using Claims.Contrib;

namespace Claims.Repository
{
    public class ClaimsContext : DbContext
    {

        private DbSet<Claim> Claims { get; init; }
        public DbSet<Cover> Covers { get; init; }

        public ClaimsContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Claim>().ToCollection("claims");
            modelBuilder.Entity<Cover>().ToCollection("covers");
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync()
        {
            return await Claims.ToListAsync();
        }

        //Can methods return null in the Coding Guidelines?
        public async Task<Claim> GetClaimAsync(string id)
        {

            var foo = await Claims
                .Where(claim => claim.Id == id)
                .SingleOrDefaultAsync();

            if (foo is null)
                return new Claim();

            return foo;
        }

        public async Task<Result> AddItemAsync(Claim item)
        {
            try
            {
                Claims.Add(item);
                int result = await SaveChangesAsync();
                return (result >= 1) ? Result.Fail<Claim>("SaveChangesAsync returned null.") : Result.Ok(result);
            }
            catch (Exception e)
            {
                return Result.Fail<Claim>("Error while saving " + e.Message);
            }

        }

        public async Task<Cover> AddItemAsync(Cover Δ)
        {
            try
            {
                Covers.Add(Δ);
                await SaveChangesAsync();

                return Δ;
            }
            catch (Exception e)
            {
              throw new Exception("An error occurred while adding a cover.", e);
            }
        }
        
        //Move this first fragment to the Service Layer
        public async Task<Result> DeleteItemAsync(string id)
        {
            try
            {
                //1. Retrieve Claim
                var claim = await GetClaimAsync(id);

                //2. If Claim does not exist, notify user
                if(claim is null)
                    return Result.Fail<Claim>("No claim with id: <id>");

                //3. Remove Claim 
                Claims.Remove(claim);

                //4. Guard
                int result = await SaveChangesAsync();
                return (result >= 1) ? Result.Fail<Claim>("SaveChangesAsync returned null or error.") : Result.Ok(result);

            }
            catch (Exception e)
            {
                return Result.Fail<Claim>("Error while saving " + e.Message);
            }
        }

        public async Task<Result> DeleteAsync()
        {
            try
            {
                Claims.RemoveRange(Claims);
                int result = await SaveChangesAsync();
                return (result >= 1) ? Result.Fail<Claim>("RemoveRange returned null.") : Result.Ok(result);
            }
            catch (Exception e)
            {
                return Result.Fail<Claim>("Error while saving " + e.Message);
            }
        }
        
        public async Task<Result> DeleteCoversAsync()
        {
            try
            {
                Covers.RemoveRange(Covers);
                int result = await SaveChangesAsync();
                return (result >= 1) ? Result.Fail<Claim>("RemoveRange returned null.") : Result.Ok(result);
            }
            catch (Exception e)
            {
                return Result.Fail<Claim>("Error while saving " + e.Message);
            }
        }
    }
}
