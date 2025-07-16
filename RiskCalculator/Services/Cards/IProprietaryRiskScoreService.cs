using RiskCalculator.Models.Cards;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service interface for calculating proprietary risk scores
/// </summary>
public interface IProprietaryRiskScoreService
{
    /// <summary>
    /// Calculate the proprietary risk score based on TSV data and clinical information
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Proprietary risk score model</returns>
    Task<ProprietaryRiskScoreModel> CalculateScoreAsync(Stream tsvFileStream, ClinicalData clinicalData);

    /// <summary>
    /// Calculate the proprietary risk score based on TSV data only
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <returns>Proprietary risk score model</returns>
    Task<ProprietaryRiskScoreModel> CalculateScoreAsync(Stream tsvFileStream);
} 