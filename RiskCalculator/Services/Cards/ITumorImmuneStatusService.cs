using RiskCalculator.Models.Cards;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service interface for analyzing tumor immune status
/// </summary>
public interface ITumorImmuneStatusService
{
    /// <summary>
    /// Analyze tumor immune status based on genomic and clinical data
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Tumor immune status model</returns>
    Task<TumorImmuneStatusModel> AnalyzeTumorImmuneStatusAsync(Stream tsvFileStream, ClinicalData clinicalData);
} 