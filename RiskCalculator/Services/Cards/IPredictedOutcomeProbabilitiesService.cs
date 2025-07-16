using RiskCalculator.Models.Cards;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service interface for predicting outcome probabilities
/// </summary>
public interface IPredictedOutcomeProbabilitiesService
{
    /// <summary>
    /// Get predicted outcome probabilities for a patient
    /// </summary>
    /// <param name="patientId">Patient identifier</param>
    /// <param name="riskScore">Current risk score</param>
    /// <returns>Predicted outcome probabilities model</returns>
    Task<PredictedOutcomeProbabilitiesModel> GetPredictedOutcomeProbabilitiesAsync(string patientId, double? riskScore);

    /// <summary>
    /// Predict outcome probabilities based on genomic and clinical data
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Predicted outcome probabilities model</returns>
    Task<PredictedOutcomeProbabilitiesModel> PredictOutcomeProbabilitiesAsync(Stream tsvFileStream, ClinicalData clinicalData);
} 