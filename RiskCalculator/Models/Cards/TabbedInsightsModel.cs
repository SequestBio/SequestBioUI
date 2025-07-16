namespace RiskCalculator.Models.Cards;

/// <summary>
/// Model for the Tabbed Insights card data
/// </summary>
public class TabbedInsightsModel
{
    /// <summary>
    /// Pathway analysis data
    /// </summary>
    public PathwayAnalysisModel PathwayAnalysis { get; set; } = new();

    /// <summary>
    /// Chemotherapy response data
    /// </summary>
    public ChemoResponseModel ChemoResponse { get; set; } = new();

    /// <summary>
    /// Immunotherapy response data
    /// </summary>
    public ImmunotherapyModel Immunotherapy { get; set; } = new();

    /// <summary>
    /// In vitro assay data
    /// </summary>
    public InVitroAssayModel InVitroAssay { get; set; } = new();

    /// <summary>
    /// Calculation timestamp
    /// </summary>
    public DateTime CalculatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Whether all analyses were successful
    /// </summary>
    public bool IsAnalysisComplete { get; set; }
}

/// <summary>
/// Model for pathway analysis data
/// </summary>
public class PathwayAnalysisModel
{
    /// <summary>
    /// Pathway activity scores
    /// </summary>
    public Dictionary<string, double> PathwayScores { get; set; } = new();

    /// <summary>
    /// Enriched pathways
    /// </summary>
    public List<PathwayEnrichment> EnrichedPathways { get; set; } = new();

    /// <summary>
    /// Pathway interaction network data
    /// </summary>
    public string NetworkData { get; set; } = string.Empty;

    /// <summary>
    /// Key pathway findings summary
    /// </summary>
    public string PathwaySummary { get; set; } = string.Empty;

    /// <summary>
    /// Whether pathway analysis was successful
    /// </summary>
    public bool IsAnalysisComplete { get; set; }
}

/// <summary>
/// Model for chemotherapy response prediction
/// </summary>
public class ChemoResponseModel
{
    /// <summary>
    /// Chemotherapy response predictions
    /// </summary>
    public List<ChemoResponse> ResponsePredictions { get; set; } = new();

    /// <summary>
    /// Recommended chemotherapy agents
    /// </summary>
    public List<string> RecommendedAgents { get; set; } = new();

    /// <summary>
    /// Agents to avoid
    /// </summary>
    public List<string> AvoidAgents { get; set; } = new();

    /// <summary>
    /// Overall response probability
    /// </summary>
    public double OverallResponseProbability { get; set; }

    /// <summary>
    /// Relevant NCCN guidelines links
    /// </summary>
    public List<string> NCCNGuidelineLinks { get; set; } = new();

    /// <summary>
    /// Whether chemotherapy analysis was successful
    /// </summary>
    public bool IsAnalysisComplete { get; set; }
}

/// <summary>
/// Model for immunotherapy response prediction
/// </summary>
public class ImmunotherapyModel
{
    /// <summary>
    /// Predicted immunotherapy response probability
    /// </summary>
    public double ResponseProbability { get; set; }

    /// <summary>
    /// Response category (Likely Responder, Unlikely Responder, etc.)
    /// </summary>
    public string ResponseCategory { get; set; } = string.Empty;

    /// <summary>
    /// Key biomarkers for immunotherapy
    /// </summary>
    public Dictionary<string, string> KeyBiomarkers { get; set; } = new();

    /// <summary>
    /// Immune signature scores
    /// </summary>
    public Dictionary<string, double> ImmuneSignatures { get; set; } = new();

    /// <summary>
    /// Recommended immunotherapy agents
    /// </summary>
    public List<string> RecommendedAgents { get; set; } = new();

    /// <summary>
    /// Clinical trial recommendations
    /// </summary>
    public List<string> ClinicalTrialRecommendations { get; set; } = new();

    /// <summary>
    /// Whether immunotherapy analysis was successful
    /// </summary>
    public bool IsAnalysisComplete { get; set; }
}

/// <summary>
/// Model for in vitro assay data
/// </summary>
public class InVitroAssayModel
{
    /// <summary>
    /// Collagen profile data
    /// </summary>
    public Dictionary<string, string> CollagenProfile { get; set; } = new();

    /// <summary>
    /// MMP profile data
    /// </summary>
    public Dictionary<string, double> MMPProfile { get; set; } = new();

    /// <summary>
    /// Measured invasiveness score
    /// </summary>
    public double InvasivenessScore { get; set; }

    /// <summary>
    /// Migration assay results
    /// </summary>
    public double MigrationScore { get; set; }

    /// <summary>
    /// Proliferation assay results
    /// </summary>
    public double ProliferationScore { get; set; }

    /// <summary>
    /// Drug sensitivity assay results
    /// </summary>
    public Dictionary<string, double> DrugSensitivity { get; set; } = new();

    /// <summary>
    /// Assay methodology notes
    /// </summary>
    public string MethodologyNotes { get; set; } = string.Empty;

    /// <summary>
    /// Whether in vitro assay analysis was successful
    /// </summary>
    public bool IsAnalysisComplete { get; set; }
}

/// <summary>
/// Represents a pathway enrichment result
/// </summary>
public class PathwayEnrichment
{
    /// <summary>
    /// Pathway name
    /// </summary>
    public string PathwayName { get; set; } = string.Empty;

    /// <summary>
    /// Enrichment score
    /// </summary>
    public double EnrichmentScore { get; set; }

    /// <summary>
    /// P-value
    /// </summary>
    public double PValue { get; set; }

    /// <summary>
    /// Adjusted P-value
    /// </summary>
    public double AdjustedPValue { get; set; }

    /// <summary>
    /// Genes in pathway
    /// </summary>
    public List<string> GenesInPathway { get; set; } = new();
}

/// <summary>
/// Represents a chemotherapy response prediction
/// </summary>
public class ChemoResponse
{
    /// <summary>
    /// Chemotherapy agent name
    /// </summary>
    public string Agent { get; set; } = string.Empty;

    /// <summary>
    /// Predicted response level
    /// </summary>
    public string ResponseLevel { get; set; } = string.Empty;

    /// <summary>
    /// Confidence percentage
    /// </summary>
    public double Confidence { get; set; }

    /// <summary>
    /// Mechanism of action
    /// </summary>
    public string MechanismOfAction { get; set; } = string.Empty;

    /// <summary>
    /// Expected side effects
    /// </summary>
    public List<string> ExpectedSideEffects { get; set; } = new();
} 