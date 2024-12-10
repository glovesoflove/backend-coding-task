
namespace Builder
{
    public class Claim
    {
        private Claims.Claim EClaim;

        public Claim()
        {
            this.EClaim = new Claims.Claim();
            this.EClaim.Id = Guid.NewGuid().ToString();
        }

        public static Claim Init()
        {
          return new Claim();
        }
        
        public Claim? DTO(Claims.Interchange.Claim Δ)
        {
            if (Δ is null)
                return null;

            return this;
        }

        public Claim? CoverId(string? Δ)
        {
            if (string.IsNullOrWhiteSpace(Δ))
              //throw new ArgumentException("Claim cover ID cannot be null, empty, or whitespace.", nameof(Δ));
                return null;

            
            EClaim.CoverId = Δ;

            return this;
        }

        public Claim? Created(DateTime Δ)
        {
            if (Δ < DateTime.UtcNow)
              //throw new ArgumentException("Claim creation date cannot be in the past.", nameof(Δ));
              return null;

            EClaim.Created = Δ;

            return this;
        }

        public Claim? Name(string? Δ)
        {
            //1. Claims MUST have a name or description
            if (string.IsNullOrWhiteSpace(Δ))
                return null;

            EClaim.Name = Δ;

            return this;
        }

        public Claim? Type(Claims.ClaimType Δ)
        {
            EClaim.Type = Δ;
            return this;
        }

        public Claim? DamageCost(decimal Δ)
        {
          if (Δ > 100000)
          {
            //Silent fail vs Exception vs Result Pattern
            //throw new ArgumentOutOfRangeException(nameof(Δ), Δ, "Damage cost cannot exceed 100,000.");
            return null;
          }

          EClaim.DamageCost = Δ;
             return this;            
        }

        public Claims.Claim Build => EClaim;
    }
}
