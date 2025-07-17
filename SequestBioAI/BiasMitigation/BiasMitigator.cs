namespace SequestBioAI.BiasMitigation
{
    public class BiasMitigator
    {
        public List<SampleDataWithDemographics> ApplyReweighting(List<SampleDataWithDemographics> data)
        {
            if (data == null || data.Count == 0)
                throw new ArgumentException("Input data cannot be null or empty");

            var groupCounts = data.GroupBy(d => d.Group)
                .ToDictionary(g => g.Key, g => g.Count());

            var total = data.Count;

            foreach (var sample in data)
            {
                sample.Weight = total / (double)groupCounts[sample.Group];
            }

            return data;
        }
    }

    public class SampleDataWithDemographics
    {
        public bool Label { get; set; }
        public string Group { get; set; } // e.g., demographic group like "African American"
        public Dictionary<string, float> Features { get; set; }
        public double Weight { get; set; }
    }
}