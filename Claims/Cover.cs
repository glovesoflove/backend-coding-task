using MongoDB.Bson.Serialization.Attributes;

namespace Claims;

public class Cover
{
    [BsonId]
    public string? Id { get; set; }

    [BsonElement("startDate")]
    [BsonDateTimeOptions(DateOnly = true)]
    public DateTime StartDate { get; set; }

    [BsonElement("endDate")]
    [BsonDateTimeOptions(DateOnly = true)]
    public DateTime EndDate { get; set; }

    [BsonElement("claimType")]
    public CoverType Type { get; set; }

    [BsonElement("premium")]
    public decimal Premium { get; set; }

  public override string ToString()
  {
    return $"Id: {Id}, Start Date: {StartDate:yyyy-MM-dd}, End Date: {EndDate:yyyy-MM-dd}, Claim Type: {Type}, Premium: {Premium:C}";
  }
}

