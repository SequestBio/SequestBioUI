namespace SequestBioAI.DataProcessing
{
    public class DataPreprocessor
    {
        public List<PreprocessedSample> PreprocessData(List<RawSample> rawSamples)
        {
            if (rawSamples == null || rawSamples.Count == 0)
                throw new ArgumentException("Raw samples cannot be null or empty");

            var processedSamples = new List<PreprocessedSample>();

            foreach (var sample in rawSamples)
            {
                if (sample.Features == null || sample.Features.Count == 0)
                    continue;

                var cleanedFeatures = sample.Features
                    .Where(kv => !float.IsNaN(kv.Value) && !float.IsInfinity(kv.Value))
                    .ToDictionary(kv => kv.Key, kv => NormalizeFeature(kv.Value));

                processedSamples.Add(new PreprocessedSample
                {
                    Label = sample.Label,
                    Features = cleanedFeatures
                });
            }

            return processedSamples;
        }

        private float NormalizeFeature(float value)
        {
            // Simple normalization between 0 and 1, adjust logic based on real data ranges
            return Math.Clamp(value, 0f, 1f);
        }
    }

    public class RawSample
    {
        public bool Label { get; set; }
        public Dictionary<string, float> Features { get; set; }
    }

    public class PreprocessedSample
    {
        public bool Label { get; set; }
        public Dictionary<string, float> Features { get; set; }
    }
}