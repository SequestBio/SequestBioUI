namespace SequestBioQuantum.FeatureSelection
{
    public class QuantumFeatureSelector
    {
        public List<string> SelectFeatures(List<string> features, int maxFeatures)
        {
            if (features == null || features.Count == 0)
                throw new ArgumentException("Feature list cannot be null or empty");

            if (maxFeatures <= 0)
                throw new ArgumentException("maxFeatures must be greater than zero");

            List<string> selectedFeatures = new List<string>();

            using (var qsim = new QuantumSimulator())
            {
                // Placeholder: Replace with real QAOA call later
                var result = QaoaStub.Run(qsim, features.Count, maxFeatures).Result;

                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i])
                        selectedFeatures.Add(features[i]);
                }
            }

            return selectedFeatures;
        }
    }
}