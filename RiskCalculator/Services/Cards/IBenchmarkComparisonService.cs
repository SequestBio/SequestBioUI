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

    /// <summary>
    /// Calculate clinical factors only score
    /// </summary>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Clinical factors score</returns>
    Task<double> CalculateClinicalFactorsScoreAsync(ClinicalData clinicalData);

    /// <summary>
    /// Calculate traditional staging score
    /// </summary>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Traditional staging score</returns>
    Task<double> CalculateTraditionalStagingScoreAsync(ClinicalData clinicalData);

    /// <summary>
    /// Calculate molecular subtype score
    /// </summary>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Molecular subtype score</returns>
    Task<double> CalculateMolecularSubtypeScoreAsync(ClinicalData clinicalData);

    /// <summary>
    /// Calculate histologic grade score
    /// </summary>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Histologic grade score</returns>
    Task<double> CalculateHistologicGradeScoreAsync(ClinicalData clinicalData);

    /// <summary>
    /// Calculate Ki-67 proliferation index score
    /// </summary>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Ki-67 score</returns>
    Task<double> CalculateKi67ScoreAsync(ClinicalData clinicalData);
} 