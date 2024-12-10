using Claims;

namespace Builder
{
    public class Cover
    {
        private Claims.Cover ECover;

        public Cover()
        {
            this.ECover = new Claims.Cover();
            this.ECover.Id = Guid.NewGuid().ToString();
        }

        public static Cover Init()
        {
            return new Cover();
        }

        public Cover? DTO(Claims.Interchange.Cover Δ)
        {
            if (Δ is null)
            {
                return null;
            }

            return this;
        }


        public Cover? StartDate(DateTime Δ)
        {
            //1. Covers MUST NOT have a creation date in the past
            if (Δ < DateTime.UtcNow)
            {
              return null;
            }

            ECover.StartDate = Δ;

            return this;
        }


        public Cover? EndDate(DateTime Δ)
        {
          if (Δ < ECover.StartDate)
          {
            throw new Exception("Covers MUST NOT have a end date before the creation date");
          }

          if (Δ > ECover.StartDate.AddYears(1))
          {
            throw new Exception("Cover period MUST NOT exceed 1 year");
          }
          
          ECover.EndDate = Δ;

          return this;
        }

        public Cover? Type(CoverType Δ)
        {
            ECover.Type = Δ;
            return this;
        }

        public Cover? Premium()
        {
          ECover.Premium = Claims.Premium.ComputePremium(ECover.StartDate, ECover.EndDate, ECover.Type);
          return this;
        }

        public Claims.Cover Build => ECover;
    }
}
