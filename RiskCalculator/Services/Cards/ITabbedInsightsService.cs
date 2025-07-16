using RiskCalculator.Models.Cards;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service interface for tabbed insights analysis
/// </summary>
public interface ITabbedInsightsService
{
    /// <summary>
    /// Get tabbed insights data for a patient
    /// </summary>
    /// <param name="patientId">Patient identifier</param>
    /// <param name="riskScore">Current risk score</param>
    /// <returns>Tabbed insights model</returns>
    Task<TabbedInsightsModel> GetTabbedInsightsAsync(string patientId, double? riskScore);

    /// <summary>
    /// Generate comprehensive tabbed insights analysis
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Tabbed insights model</returns>
    Task<TabbedInsightsModel> GenerateTabbedInsightsAsync(Stream tsvFileStream, ClinicalData clinicalData);

    /// <summary>
    /// Perform pathway analysis
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Pathway analysis model</returns>
    Task<PathwayAnalysisModel> PerformPathwayAnalysisAsync(Stream tsvFileStream, ClinicalData clinicalData);

    /// <summary>
    /// Predict chemotherapy response
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Chemotherapy response model</returns>
    Task<ChemoResponseModel> PredictChemoResponseAsync(Stream tsvFileStream, ClinicalData clinicalData);

    /// <summary>
    /// Predict immunotherapy response
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Immunotherapy response model</returns>
    Task<ImmunotherapyModel> PredictImmunotherapyResponseAsync(Stream tsvFileStream, ClinicalData clinicalData);

    /// <summary>
    /// Perform in vitro assay analysis
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>In vitro assay model</returns>
    Task<InVitroAssayModel> PerformInVitroAssayAnalysisAsync(Stream tsvFileStream, ClinicalData clinicalData);
} 