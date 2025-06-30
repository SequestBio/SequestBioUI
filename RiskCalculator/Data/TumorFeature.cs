namespace RiskCalculator.Data
{
    public class TumorFeature
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Pathway { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
        public string ExpressionLevel { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsPositiveMarker { get; set; }
    }
}