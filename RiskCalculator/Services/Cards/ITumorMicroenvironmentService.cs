using RiskCalculator.Models.Cards;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service interface for analyzing tumor microenvironment
/// </summary>
public interface ITumorMicroenvironmentService
{
    /// <summary>
    /// Analyze tumor microenvironment based on genomic and clinical data
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Tumor microenvironment model</returns>
    Task<TumorMicroenvironmentModel> AnalyzeTumorMicroenvironmentAsync(Stream tsvFileStream, ClinicalData clinicalData);
} 