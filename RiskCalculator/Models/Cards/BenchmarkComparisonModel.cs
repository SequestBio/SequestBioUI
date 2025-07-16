namespace RiskCalculator.Models.Cards;

/// <summary>
/// Model for the Benchmark Comparison card data
/// </summary>
public class BenchmarkComparisonModel
{
    /// <summary>
    /// Benchmark comparison data points
    /// </summary>
    public List<BenchmarkPoint> BenchmarkData { get; set; } = new();

    /// <summary>
    /// Current Sequestone score
    /// </summary>
    public double SequestoneScore { get; set; }

    /// <summary>
    /// Clinical factors only score
    /// </summary>
    public double ClinicalFactorsScore { get; set; }

    /// <summary>
    /// Traditional staging score
    /// </summary>
    public double StagingScore { get; set; }

    /// <summary>
    /// Molecular subtype score
    /// </summary>
    public double MolecularSubtypeScore { get; set; }

    /// <summary>
    /// Histologic grade score
    /// </summary>
    public double HistologicGradeScore { get; set; }

    /// <summary>
    /// Ki-67 proliferation index score
    /// </summary>
    public double Ki67Score { get; set; }

    /// <summary>
    /// Comparison summary
    /// </summary>
    public string ComparisonSummary { get; set; } = string.Empty;

    /// <summary>
    /// Relative performance metrics
    /// </summary>
    public Dictionary<string, double> PerformanceMetrics { get; set; } = new();

    /// <summary>
    /// Calculation timestamp
    /// </summary>
    public DateTime CalculatedAt { get; set; } = DateTime.Now;

    /// <summary>
    /// Whether the comparison analysis was successful
    /// </summary>
    public bool IsAnalysisComplete { get; set; }
}

/// <summary>
/// Represents a benchmark comparison data point
/// </summary>
public class BenchmarkPoint
{
    /// <summary>
    /// Label for the benchmark (e.g., "Sequestone Score", "Clinical Factors Only")
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Value for the benchmark
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Performance metric (e.g., AUC, sensitivity, specificity)
    /// </summary>
    public string Metric { get; set; } = string.Empty;

    /// <summary>
    /// Color for visualization
    /// </summary>
    public string Color { get; set; } = string.Empty;
} 