using RiskCalculator.Models.Cards;
using SequestBioAI.Data;
using SequestBioAI.RiskScore;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service for analyzing key risk contributors (Scientists: Modify risk contributor analysis here)
/// </summary>
public class KeyRiskContributorsService : IKeyRiskContributorsService
{
    public Task<KeyRiskContributorsModel> GetKeyRiskContributorsAsync(string patientId, double? riskScore)
    {
        // TODO: Scientists - Implement this method to fetch or calculate key risk contributors by patientId and riskScore
        // This method is not currently used by the application but may be needed for future database-based lookups
        return Task.FromResult(new KeyRiskContributorsModel
        {
            TopContributors = new List<RiskContributor>(),
            AllContributors = new List<RiskContributor>(),
            RiskFactors = new List<RiskContributor>(),
            ProtectiveFactors = new List<RiskContributor>(),
            ContributorSummary = "Analysis not available",
            TotalFactorsAnalyzed = 0,
            IsAnalysisComplete = false,
            CalculatedAt = DateTime.Now
        });
    }

    public async Task<KeyRiskContributorsModel> AnalyzeKeyRiskContributorsAsync(Stream tsvFileStream, ClinicalData clinicalData)
    {
        try
        {
            // Use existing RiskScoreCalculator to get contributors
            var (score, topContributors, allContributors) = await RiskScoreCalculator.CalculateRiskWithContributors(tsvFileStream);
            
            // Categorize contributors
            var (riskFactors, protectiveFactors) = await SeparateRiskAndProtectiveFactorsAsync(allContributors);
            
            // Generate summary
            var contributorSummary = await GenerateContributorSummaryAsync(topContributors, clinicalData);

            return new KeyRiskContributorsModel
            {
                TopContributors = topContributors,
                AllContributors = allContributors,
                RiskFactors = riskFactors,
                ProtectiveFactors = protectiveFactors,
                ContributorSummary = contributorSummary,
                TotalFactorsAnalyzed = allContributors.Count,
                IsAnalysisComplete = true,
                CalculatedAt = DateTime.Now
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error analyzing risk contributors: {ex.Message}");
            
            return new KeyRiskContributorsModel
            {
                TopContributors = new List<RiskContributor>(),
                AllContributors = new List<RiskContributor>(),
                RiskFactors = new List<RiskContributor>(),
                ProtectiveFactors = new List<RiskContributor>(),
                ContributorSummary = "Analysis failed",
                TotalFactorsAnalyzed = 0,
                IsAnalysisComplete = false,
                CalculatedAt = DateTime.Now
            };
        }
    }

    public async Task<List<RiskContributor>> IdentifyTopContributorsAsync(Stream tsvFileStream, int topCount = 5)
    {
        var (score, topContributors, allContributors) = await RiskScoreCalculator.CalculateRiskWithContributors(tsvFileStream);
        return topContributors.Take(topCount).ToList();
    }

    public async Task<List<RiskContributor>> GetAllContributorsAsync(Stream tsvFileStream)
    {
        var (score, topContributors, allContributors) = await RiskScoreCalculator.CalculateRiskWithContributors(tsvFileStream);
        return allContributors;
    }

    public async Task<(List<RiskContributor> RiskFactors, List<RiskContributor> ProtectiveFactors)> SeparateRiskAndProtectiveFactorsAsync(List<RiskContributor> contributors)
    {
        // Scientists: Enhance this method to improve categorization logic
        var riskFactors = contributors
            .Where(c => c.Impact == "High Risk" || c.Contribution > 0)
            .OrderByDescending(c => c.Contribution)
            .ToList();

        var protectiveFactors = contributors
            .Where(c => c.Impact == "Protective" || c.Contribution < 0)
            .OrderBy(c => c.Contribution) // Ascending for negative values
            .ToList();

        return await Task.FromResult((riskFactors, protectiveFactors));
    }

    /// <summary>
    /// Generate a summary of key contributors
    /// Scientists: Modify this method to enhance summary generation
    /// </summary>
    public async Task<string> GenerateContributorSummaryAsync(List<RiskContributor> contributors, ClinicalData clinicalData)
    {
        var summary = new List<string>();

        if (contributors.Any())
        {
            var topContributor = contributors.First();
            summary.Add($"Top contributor: {topContributor.Name} ({topContributor.Contribution:F1}% impact)");
        }

        // Balance assessment
        double totalRisk = contributors.Sum(rf => rf.Contribution);
        double totalProtective = Math.Abs(contributors.Sum(pf => pf.Contribution));

        if (totalRisk > totalProtective)
        {
            summary.Add($"Risk-dominant profile: {contributors.Count} risk factors outweigh {contributors.Count} protective factors");
        }
        else if (totalProtective > totalRisk)
        {
            summary.Add($"Protective-dominant profile: {contributors.Count} protective factors outweigh {contributors.Count} risk factors");
        }
        else
        {
            summary.Add($"Balanced profile: {contributors.Count} risk factors and {contributors.Count} protective factors are roughly balanced");
        }

        return string.Join(". ", summary);
    }
} 