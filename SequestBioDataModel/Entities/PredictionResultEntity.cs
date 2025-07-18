namespace SequestBioDataModel.Entities;

public class PredictionResultEntity
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public int Score { get; set; }
    public string RiskCategory { get; set; } = string.Empty;
    public string Recommendation { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}