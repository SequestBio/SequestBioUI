using RiskCalculator.Models.Cards;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service interface for analyzing tumor immune status (Hot vs Cold)
/// </summary>
public interface ITumorImmuneStatusService
{
    /// <summary>
    /// Analyze tumor immune status from TSV data and clinical information
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Tumor immune status model</returns>
    Task<TumorImmuneStatusModel> AnalyzeTumorImmuneStatusAsync(Stream tsvFileStream, ClinicalData clinicalData);

    /// <summary>
    /// Calculate hot/cold tumor score
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <returns>Hot/cold score (0-100)</returns>
    Task<int> CalculateHotColdScoreAsync(Stream tsvFileStream);

    /// <summary>
    /// Calculate T-cell infiltration level
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <returns>T-cell infiltration level</returns>
    Task<double> CalculateTCellInfiltrationAsync(Stream tsvFileStream);

    /// <summary>
    /// Calculate PD-L1 expression level
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <returns>PD-L1 expression level</returns>
    Task<double> CalculatePDL1ExpressionAsync(Stream tsvFileStream);

    /// <summary>
    /// Calculate interferon gamma signature
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <returns>Interferon gamma signature score</returns>
    Task<double> CalculateInterferonGammaSignatureAsync(Stream tsvFileStream);
} 