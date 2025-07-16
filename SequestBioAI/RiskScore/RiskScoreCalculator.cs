using System.Globalization;
using System.Text;
using SequestBioAI.Data;

namespace SequestBioAI.RiskScore;

/// <summary>
/// Comprehensive risk score calculator that provides analysis for all card types
/// Scientists: This is the main calculation engine - modify algorithms here
/// </summary>
public static class RiskScoreCalculator
{
    /* ------------------------------------------------------------------
       CONFIGURABLE WEIGHTS  – genes with published association to
       breast‑cancer aggressiveness / metastasis.  Positive value ⇒
       raises risk; negative value ⇒ protective.  Magnitudes tuned to
       keep raw scores in ~0‑0.1 range so 0‑100 normalisation is stable.
       ------------------------------------------------------------------*/

    private static readonly Dictionary<string, double> GeneWeights = new(StringComparer.OrdinalIgnoreCase)
    {
        // Matrix‑remodelling & invasion
        {"MMP9",    0.0030},
        {"MMP2",    0.0020},
        {"MMP14",   0.0020},
        {"MMP11",   0.0018},
        {"LOX",     0.0025},

        // EMT transcription factors & markers
        {"TWIST1",  0.0025},
        {"SNAI1",   0.0020},
        {"SNAI2",   0.0020}, // SLUG
        {"VIM",     0.0020}, // Vimentin
        {"CDH2",    0.0020}, // N‑cadherin
        {"FSCN1",   0.0020},

        // Oncogenic signalling / proliferation
        {"MYC",     0.0030},
        {"TP53",    0.0040},
        {"ERBB2",   0.0030},
        {"PIK3CA",  0.0025},
        {"CCND1",   0.0020},
        {"AURKA",   0.0020},
        {"KRAS",    0.0025},

        // Angiogenesis & hypoxia response
        {"VEGFA",   0.0020},
        {"ANGPTL4", 0.0020},
        {"POSTN",   0.0020},
        {"CXCL12",  0.0018},
        {"CXCR4",   0.0020},

        // Immune‑evasion / checkpoints
        {"PDL1",    0.0015},

        // Drug resistance / stemness
        {"ABCB1",   0.0020},
        {"ALDH1A1", 0.0018},
        {"BIRC5",   0.0018}, // Survivin

        // Extracellular matrix rigidity – collagen I high expression
        {"COL1A1",  0.0020},

        // Protective / favourable‑prognosis genes
        {"CXCL9",  -0.0010},
        {"CXCL10", -0.0010},
        {"CD8",    -0.0015},
        {"ESR1",   -0.0015},
        {"PGR",    -0.0010},
        {"GATA3",  -0.0010},
        {"FOXA1",  -0.0010},
        {"CDH1",   -0.0020}, // E‑cadherin
        {"ABCA3",  -0.0010},
        {"CCL22",  -0.0010},
        {"FOXJ1",  -0.0010}
    };

    /* ------------------------------------------------------------------
       Clinical / functional factor weights
       ------------------------------------------------------------------*/
    private const double W_SII_High  = 0.0020;  // SequestOne Invasion Index >0.8
    private const double W_KI67_High = 0.0025;  // Ki‑67 >60 %
    private const double W_TP53_Mut  = 0.0030;  // TP53 mutation (if not captured by expression)

    private const double ExpressionPosThreshold = 1.0; // TPM cut‑off

    /* ------------------------------------------------------------------
       COMPREHENSIVE ANALYSIS RESULT
       ------------------------------------------------------------------*/
    public class ComprehensiveAnalysisResult
    {
        public int RiskScore { get; set; }
        public string RiskCategory { get; set; } = string.Empty;
        public int Confidence { get; set; }
        public List<RiskContributor> TopContributors { get; set; } = new();
        public List<RiskContributor> AllContributors { get; set; } = new();
        
        // Tumor microenvironment metrics
        public int GenomicInstability { get; set; }
        public int TILLevel { get; set; }
        public double MutationBurden { get; set; }
        public double ImmuneInfiltration { get; set; }
        public double CellularHeterogeneity { get; set; }
        public double StromalContent { get; set; }
        
        // Immune status metrics
        public int TumorHotColdScore { get; set; }
        public string ImmuneStatus { get; set; } = string.Empty;
        public double TCellInfiltration { get; set; }
        public double BCellInfiltration { get; set; }
        public double NKCellInfiltration { get; set; }
        public double MacrophageInfiltration { get; set; }
        public double PDL1Expression { get; set; }
        public double InterferonGammaSignature { get; set; }
        
        // Survival predictions
        public double FiveYearSurvival { get; set; }
        public double TenYearSurvival { get; set; }
        public double MedianSurvivalMonths { get; set; }
        
        // Pathway analysis
        public Dictionary<string, double> PathwayScores { get; set; } = new();
        public List<PathwayEnrichment> EnrichedPathways { get; set; } = new();
        
        // Treatment response predictions
        public List<ChemoResponse> ChemoResponses { get; set; } = new();
        public double ImmunotherapyResponseProbability { get; set; }
        public string ImmunotherapyResponseCategory { get; set; } = string.Empty;
        
        // Performance metrics for benchmarking
        public Dictionary<string, double> PerformanceMetrics { get; set; } = new();
        
        // Raw patient data for reference
        public PatientData PatientData { get; set; } = new();
    }

    /* ------------------------------------------------------------------
       PUBLIC ENTRY POINTS
       ------------------------------------------------------------------*/
    public static async Task<int> CalculateRiskCategory(Stream tsvFileStream)
    {
        using var reader = new StreamReader(tsvFileStream, Encoding.UTF8, leaveOpen: true);
        var patient = await ParsePatientDataFromTsv(reader);
        return CalculateRiskCategory(patient);
    }

    public static async Task<(int score, List<RiskContributor> topContributors, List<RiskContributor> allContributors)> CalculateRiskWithContributors(Stream tsvFileStream)
    {
        using var reader = new StreamReader(tsvFileStream, Encoding.UTF8, leaveOpen: true);
        var patient = await ParsePatientDataFromTsv(reader);
        var score = CalculateRiskCategory(patient);
        var topContributors = GetTopContributors(patient);
        var allContributors = GetAllContributors(patient);
        return (score, topContributors, allContributors);
    }

    /// <summary>
    /// Comprehensive analysis method for all card calculations
    /// Scientists: This is the main entry point for all analysis
    /// </summary>
    public static async Task<ComprehensiveAnalysisResult> PerformComprehensiveAnalysis(Stream tsvFileStream)
    {
        using var reader = new StreamReader(tsvFileStream, Encoding.UTF8, leaveOpen: true);
        var patient = await ParsePatientDataFromTsv(reader);
        
        var result = new ComprehensiveAnalysisResult
        {
            PatientData = patient
        };
        
        // Core risk calculation
        result.RiskScore = CalculateRiskCategory(patient);
        result.RiskCategory = GetRiskCategory(result.RiskScore);
        result.Confidence = CalculateConfidence(result.RiskScore, patient);
        result.TopContributors = GetTopContributors(patient);
        result.AllContributors = GetAllContributors(patient);
        
        // Tumor microenvironment analysis
        CalculateTumorMicroenvironment(result, patient);
        
        // Immune status analysis
        CalculateImmuneStatus(result, patient);
        
        // Survival predictions
        CalculateSurvivalPredictions(result, patient);
        
        // Pathway analysis
        CalculatePathwayAnalysis(result, patient);
        
        // Treatment response predictions
        CalculateTreatmentResponses(result, patient);
        
        // Performance metrics
        CalculatePerformanceMetrics(result, patient);
        
        return result;
    }

    /* ------------------------------------------------------------------
       CORE CALCULATION METHODS
       ------------------------------------------------------------------*/
    private static int CalculateRiskCategory(PatientData data)
    {
        double risk   = 0.0;
        double denom  = 0.0;

        // Gene‑level loops
        foreach (var f in data.TumorFeatures)
        {
            if (!f.IsPositiveMarker) continue;
            double w = GeneWeights.TryGetValue(f.Name, out var wt) ? wt : 0.0015; // default small risk
            risk  += w;
            denom += Math.Abs(w);
        }

        // Clinical contributions
        if (data.SII > 0.8)                      { risk += W_SII_High;  denom += W_SII_High;  }
        if (data.Ki67 > 60)                      { risk += W_KI67_High; denom += W_KI67_High; }
        if (string.Equals(data.TP53Status, "mut", StringComparison.OrdinalIgnoreCase))
                                                 { risk += W_TP53_Mut;  denom += W_TP53_Mut;  }

        if (denom == 0) return 0;

        var normalised = (risk / denom) * 100.0;
        normalised = Math.Clamp(normalised, 0, 100);
        return (int)Math.Round(normalised);
    }

    private static string GetRiskCategory(int score) => score switch
    {
        > 66 => "High Risk",
        > 33 => "Moderate Risk",
        _ => "Low Risk"
    };

    private static int CalculateConfidence(int score, PatientData patient)
    {
        // Scientists: Enhance this confidence calculation
        int baseConfidence = 70;
        int contributorBonus = Math.Min(patient.TumorFeatures.Count * 2, 20);
        int scoreAdjustment = score switch
        {
            < 5 or > 95 => -5,
            < 10 or > 90 => -3,
            _ => 0
        };
        
        int confidence = baseConfidence + contributorBonus + scoreAdjustment;
        return Math.Clamp(confidence, 0, 100);
    }

    /* ------------------------------------------------------------------
       TUMOR MICROENVIRONMENT ANALYSIS
       ------------------------------------------------------------------*/
    private static void CalculateTumorMicroenvironment(ComprehensiveAnalysisResult result, PatientData patient)
    {
        // Scientists: Replace with real TME calculations
        var random = new Random();
        
        result.GenomicInstability = random.Next(20, 80);
        result.TILLevel = random.Next(25, 75);
        result.MutationBurden = result.GenomicInstability * 0.1;
        result.ImmuneInfiltration = result.TILLevel * 0.01;
        result.CellularHeterogeneity = 0.75;
        result.StromalContent = 45.0;
    }

    /* ------------------------------------------------------------------
       IMMUNE STATUS ANALYSIS
       ------------------------------------------------------------------*/
    private static void CalculateImmuneStatus(ComprehensiveAnalysisResult result, PatientData patient)
    {
        // Scientists: Replace with real immune status calculations
        var random = new Random();
        
        result.TumorHotColdScore = random.Next(30, 85);
        result.ImmuneStatus = result.TumorHotColdScore > 70 ? "Hot" : 
                             result.TumorHotColdScore > 30 ? "Moderate" : "Cold";
        result.TCellInfiltration = result.TumorHotColdScore * 0.01;
        result.BCellInfiltration = result.TumorHotColdScore * 0.005;
        result.NKCellInfiltration = result.TumorHotColdScore * 0.003;
        result.MacrophageInfiltration = result.TumorHotColdScore * 0.008;
        result.PDL1Expression = result.TumorHotColdScore * 0.5;
        result.InterferonGammaSignature = result.TumorHotColdScore * 0.3;
    }

    /* ------------------------------------------------------------------
       SURVIVAL PREDICTIONS
       ------------------------------------------------------------------*/
    private static void CalculateSurvivalPredictions(ComprehensiveAnalysisResult result, PatientData patient)
    {
        // Scientists: Replace with real survival models
        var riskMultiplier = result.RiskScore / 100.0;
        var baseSurvival = 0.85 - (riskMultiplier * 0.3);
        
        result.FiveYearSurvival = baseSurvival;
        result.TenYearSurvival = baseSurvival * 0.8;
        result.MedianSurvivalMonths = baseSurvival * 120;
    }

    /* ------------------------------------------------------------------
       PATHWAY ANALYSIS
       ------------------------------------------------------------------*/
    private static void CalculatePathwayAnalysis(ComprehensiveAnalysisResult result, PatientData patient)
    {
        // Scientists: Replace with real pathway analysis
        result.PathwayScores = new Dictionary<string, double>
        {
            { "Proliferation", 0.85 },
            { "Invasion", 0.65 },
            { "Angiogenesis", 0.55 },
            { "Metastasis", 0.45 }
        };

        result.EnrichedPathways = new List<PathwayEnrichment>
        {
            new PathwayEnrichment { PathwayName = "Cell Cycle", EnrichmentScore = 2.5, PValue = 0.001 },
            new PathwayEnrichment { PathwayName = "DNA Repair", EnrichmentScore = 2.1, PValue = 0.005 },
            new PathwayEnrichment { PathwayName = "Apoptosis", EnrichmentScore = 1.8, PValue = 0.01 }
        };
    }

    /* ------------------------------------------------------------------
       TREATMENT RESPONSE PREDICTIONS
       ------------------------------------------------------------------*/
    private static void CalculateTreatmentResponses(ComprehensiveAnalysisResult result, PatientData patient)
    {
        // Scientists: Replace with real treatment response models
        result.ChemoResponses = new List<ChemoResponse>
        {
            new ChemoResponse { Agent = "Doxorubicin", ResponseLevel = "High", Confidence = 0.85 },
            new ChemoResponse { Agent = "Paclitaxel", ResponseLevel = "Moderate", Confidence = 0.72 },
            new ChemoResponse { Agent = "Carboplatin", ResponseLevel = "Low", Confidence = 0.45 }
        };

        result.ImmunotherapyResponseProbability = result.TumorHotColdScore / 100.0;
        result.ImmunotherapyResponseCategory = result.TumorHotColdScore > 50 ? "Likely Responder" : "Unlikely Responder";
    }

    /* ------------------------------------------------------------------
       PERFORMANCE METRICS
       ------------------------------------------------------------------*/
    private static void CalculatePerformanceMetrics(ComprehensiveAnalysisResult result, PatientData patient)
    {
        // Scientists: Replace with real performance metrics
        result.PerformanceMetrics = new Dictionary<string, double>
        {
            { "AUC", 0.85 },
            { "Sensitivity", 0.88 },
            { "Specificity", 0.82 },
            { "PPV", 0.79 },
            { "NPV", 0.91 }
        };
    }

    /* ------------------------------------------------------------------
       CONTRIBUTOR ANALYSIS
       ------------------------------------------------------------------*/
    private static List<RiskContributor> GetAllContributors(PatientData data)
    {
        var contributors = new List<RiskContributor>();
        
        // Calculate total weight for normalization
        double totalWeight = 0.0;
        
        // Process genes with known weights
        foreach (var feature in data.TumorFeatures)
        {
            if (GeneWeights.TryGetValue(feature.Name, out var weight))
            {
                // For positive markers, use the weight; for negative markers, use the negative weight
                var effectiveWeight = feature.IsPositiveMarker ? weight : -weight;
                totalWeight += Math.Abs(effectiveWeight);
                
                // Parse TPM value from ExpressionLevel (stored during parsing)
                double tpmValue = 0.0;
                if (feature.IsPositiveMarker && double.TryParse(feature.ExpressionLevel.Replace("Positive", "").Trim(), out var tpm))
                {
                    tpmValue = tpm;
                }
                
                contributors.Add(new RiskContributor
                {
                    Name = feature.Name,
                    Contribution = effectiveWeight,
                    Impact = effectiveWeight > 0 ? "High Risk" : "Protective",
                    ExpressionLevel = tpmValue
                });
            }
        }
        
        // Calculate contribution percentages
        if (totalWeight > 0)
        {
            foreach (var contributor in contributors)
            {
                contributor.Contribution = (contributor.Contribution / totalWeight) * 100.0;
            }
        }
        
        // Sort by absolute contribution (all contributors)
        return contributors
            .OrderByDescending(c => Math.Abs(c.Contribution))
            .ToList();
    }

    private static List<RiskContributor> GetTopContributors(PatientData data)
    {
        return GetAllContributors(data).Take(5).ToList();
    }

    /* ------------------------------------------------------------------
       TSV PARSER – expects 2nd column = gene symbol, 7th column = TPM
       Enhanced to store TPM values
       ------------------------------------------------------------------*/
    private static async Task<PatientData> ParsePatientDataFromTsv(StreamReader reader)
    {
        var p = new PatientData { TumorFeatures = new List<TumorFeature>() };
        bool headerSeen = false;
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#')) continue;
            if (!headerSeen) { headerSeen = true; continue; }
            var c = line.Split('\t');
            if (c.Length < 7) continue;
            if (!double.TryParse(c[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var tpm)) continue;
            
            // Store TPM value in ExpressionLevel for later use
            p.TumorFeatures.Add(new TumorFeature
            {
                Name             = c[1].Trim(),
                ExpressionLevel  = tpm >= ExpressionPosThreshold ? $"Positive {tpm}" : "Negative",
                IsPositiveMarker = tpm >= ExpressionPosThreshold
            });
        }
        return p;
    }
}

/* ------------------------------------------------------------------
   SUPPORTING CLASSES FOR COMPREHENSIVE ANALYSIS
   ------------------------------------------------------------------*/

/// <summary>
/// Pathway enrichment data
/// </summary>
public class PathwayEnrichment
{
    public string PathwayName { get; set; } = string.Empty;
    public double EnrichmentScore { get; set; }
    public double PValue { get; set; }
}

/// <summary>
/// Chemotherapy response prediction
/// </summary>
public class ChemoResponse
{
    public string Agent { get; set; } = string.Empty;
    public string ResponseLevel { get; set; } = string.Empty;
    public double Confidence { get; set; }
}