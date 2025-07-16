using RiskCalculator.Models.Cards;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service interface for benchmark comparison analysis
/// </summary>
public interface IBenchmarkComparisonService
{
    /// <summary>
    /// Get benchmark comparison data for a patient
    /// </summary>
    /// <param name="patientId">Patient identifier</param>
    /// <param name="riskScore">Current risk score</param>
    /// <returns>Benchmark comparison model</returns>
    Task<BenchmarkComparisonModel> GetBenchmarkComparisonAsync(string patientId, double? riskScore);

    /// <summary>
    /// Perform benchmark comparison analysis
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Benchmark comparison model</returns>
    Task<BenchmarkComparisonModel> PerformBenchmarkComparisonAsync(Stream tsvFileStream, ClinicalData clinicalData);
} 