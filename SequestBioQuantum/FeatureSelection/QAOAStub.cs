namespace SequestBioQuantum.FeatureSelection;

public static class QaoaStub
{
    public static async Task<bool[]> Run(int featureCount, int maxFeatures)
    {
        bool[] selection = new bool[featureCount];
        Random rnd = new Random();

        HashSet<int> selectedIndices = new();

        while (selectedIndices.Count < Math.Min(maxFeatures, featureCount))
        {
            int index = rnd.Next(featureCount);
            selectedIndices.Add(index);
        }

        foreach (var idx in selectedIndices)
            selection[idx] = true;

        await System.Threading.Tasks.Task.Delay(10); // Simulate async work
        return selection;
    }
}