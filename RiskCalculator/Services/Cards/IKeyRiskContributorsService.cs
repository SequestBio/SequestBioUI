using RiskCalculator.Models.Cards;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service interface for key risk contributors analysis
/// </summary>
public interface IKeyRiskContributorsService
{
    /// <summary>
    /// Get key risk contributors data for a patient
    /// </summary>
    /// <param name="patientId">Patient identifier</param>
    /// <param name="riskScore">Current risk score</param>
    /// <returns>Key risk contributors model</returns>
    Task<KeyRiskContributorsModel> GetKeyRiskContributorsAsync(string patientId, double? riskScore);

    /// <summary>
    /// Analyze key risk contributors from TSV data and clinical information
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Key risk contributors model</returns>
    Task<KeyRiskContributorsModel> AnalyzeKeyRiskContributorsAsync(Stream tsvFileStream, ClinicalData clinicalData);

    /// <summary>
    /// Identify top contributing factors
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="topCount">Number of top contributors to return</param>
    /// <returns>List of top risk contributors</returns>
    Task<List<RiskContributor>> IdentifyTopContributorsAsync(Stream tsvFileStream, int topCount = 5);

    /// <summary>
    /// Separate risk factors from protective factors
    /// </summary>
    /// <param name="contributors">All contributors</param>
    /// <returns>Tuple of risk factors and protective factors</returns>
    Task<(List<RiskContributor> RiskFactors, List<RiskContributor> ProtectiveFactors)> SeparateRiskAndProtectiveFactorsAsync(List<RiskContributor> contributors);

    /// <summary>
    /// Generate contributor summary text
    /// </summary>
    /// <param name="contributors">Risk contributors</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Summary text</returns>
    Task<string> GenerateContributorSummaryAsync(List<RiskContributor> contributors, ClinicalData clinicalData);
} 