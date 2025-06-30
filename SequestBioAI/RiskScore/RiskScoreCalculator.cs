using System.Globalization;
using System.Text;
using SequestBioAI.Data;

namespace SequestBioAI.RiskScore;

public static class RiskScoreCalculator
{
    // Public method UI will call — accepts a file stream
    public static async Task<int> CalculateRiskCategory(Stream tsvFileStream)
    {
        using var reader = new StreamReader(tsvFileStream, Encoding.UTF8, leaveOpen: true);
        var patient = await ParsePatientDataFromTsv(reader);
        return CalculateRiskCategory(patient);
    }

    // Internal method that performs risk logic
    private static int CalculateRiskCategory(PatientData data)
    {
        double riskScore = 0;
        double maxScore = 0;

        // High-risk markers contribute more
        var highRiskMarkers = new[] { "MMP9", "MYC", "CD44", "TP53", "BCL2" };

        foreach (var feature in data.TumorFeatures)
        {
            if (!feature.IsPositiveMarker)
                continue;

            if (highRiskMarkers.Contains(feature.Name, StringComparer.OrdinalIgnoreCase))
                maxScore += 0.002; // High-risk gene
            else
                maxScore += 0.001; // Generic positive gene
            if (feature.IsPositiveMarker)
            {
                riskScore += highRiskMarkers.Contains(feature.Name, StringComparer.OrdinalIgnoreCase) ? 0.002 : 0.001;
            }
        }

        // Clinical factors
        maxScore += 0.002 + 0.0025 + 0.003;

        if (data.SII > 0.8) riskScore += 0.002;
        if (data.Ki67 > 60) riskScore += 0.0025;
        if (data.TP53Status?.ToLowerInvariant() == "mut") riskScore += 0.003;

        if (maxScore == 0) return 0;

        // normalize between 0 and 100
        double normalized = (double)riskScore; // / (maxScore) * 100;
        return (int)Math.Round(normalized);    }


    // Parses a TSV file into PatientData
    private static async Task<PatientData> ParsePatientDataFromTsv(StreamReader reader)
    {
        var patient = new PatientData { TumorFeatures = new List<TumorFeature>() };

        bool isHeaderSkipped = false;
        string line;
        int lineNum = 0;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            lineNum++;

            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                continue;

            if (!isHeaderSkipped)
            {
                Console.WriteLine("Header skipped.");
                isHeaderSkipped = true;
                continue;
            }

            var columns = line.Split('\t');
            if (columns.Length < 9)
            {
                continue;
            }

            var geneName = columns[1].Trim();
            var tpm = columns[6].Trim();

            if (!double.TryParse(tpm, NumberStyles.Any, CultureInfo.InvariantCulture, out var expressionLevel))
            {
                continue;
            }

            Console.WriteLine($"Parsed {geneName} with TPM {expressionLevel}");
            patient.TumorFeatures.Add(new TumorFeature
            {
                Name = geneName,
                ExpressionLevel = expressionLevel >= 1.0 ? "Positive" : "Negative",
                IsPositiveMarker = expressionLevel >= 1.0
            });
        }


        return patient;
    }
}
