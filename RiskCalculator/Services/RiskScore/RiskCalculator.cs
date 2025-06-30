using SequestBio.ScoreComponent.Patient.Data;
using SequestBio.ScoreComponent.Services.RiskScore;

namespace SequestBio.SequestBio.ScoreComponent.Services.RiskScore;

public static class RiskCalculator
{
    public static string CalculateRiskCategory(PatientData data)
    {
        int riskScore = 0;

        var highRiskMarkers = new[] { "MMP9", "MYC", "CD44", "TP53", "BCL2" };

        foreach (var marker in highRiskMarkers)
        {
            if (data.TumorFeatures.Any(f => f.Name == marker && f.IsPositiveMarker && f.ExpressionLevel == "Positive"))
            {
                riskScore += 2;
            }
        }

        if (data.SII > 0.8) riskScore++;
        if (data.Ki67 > 60) riskScore += 2;
        if (data.TP53Status.ToLower() == "mut") riskScore += 3;

        return riskScore switch
        {
            >= 10 => "High Risk",
            >= 6 => "Moderate Risk",
            _ => "Low Risk"
        };
    }
}