using RiskCalculator.Models.Cards;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Cards
{
    public interface IPredictedOutcomeProbabilitiesService
    {
        /// <summary>
        /// Generates predicted outcome probabilities for a patient
        /// </summary>
        /// <param name="patientId">Patient identifier</param>
        /// <param name="riskScore">Current risk score</param>
        /// <returns>Predicted outcome probabilities model</returns>
        Task<PredictedOutcomeProbabilitiesModel> GetPredictedOutcomeProbabilitiesAsync(string patientId, double? riskScore);
        
        /// <summary>
        /// Calculates Relapse-Free Survival (RFS) curve
        /// </summary>
        /// <param name="tsvFileStream">RNAseq TSV file stream</param>
        /// <param name="clinicalData">Patient clinical data</param>
        /// <returns>RFS data points</returns>
        Task<List<SurvivalPoint>> CalculateRecurrenceFreeSurvivalAsync(Stream tsvFileStream, ClinicalData clinicalData);
        
        /// <summary>
        /// Calculates Metastasis-Free Survival (MFS) curve
        /// </summary>
        /// <param name="tsvFileStream">RNAseq TSV file stream</param>
        /// <param name="clinicalData">Patient clinical data</param>
        /// <returns>MFS data points</returns>
        Task<List<SurvivalPoint>> CalculateMetastasisFreeSurvivalAsync(Stream tsvFileStream, ClinicalData clinicalData);
        
        /// <summary>
        /// Calculates Overall Survival (OS) curve
        /// </summary>
        /// <param name="tsvFileStream">RNAseq TSV file stream</param>
        /// <param name="clinicalData">Patient clinical data</param>
        /// <returns>OS data points</returns>
        Task<List<SurvivalPoint>> CalculateOverallSurvivalAsync(Stream tsvFileStream, ClinicalData clinicalData);
        
        /// <summary>
        /// Calculates Disease-Free Survival (DFS) curve
        /// </summary>
        /// <param name="tsvFileStream">RNAseq TSV file stream</param>
        /// <param name="clinicalData">Patient clinical data</param>
        /// <returns>DFS data points</returns>
        Task<List<SurvivalPoint>> CalculateDiseaseFreeSurvivalAsync(Stream tsvFileStream, ClinicalData clinicalData);
    }
} 