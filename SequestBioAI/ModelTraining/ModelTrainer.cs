using Microsoft.ML;
using Microsoft.ML.Data;

namespace SequestBioAI.ModelTraining
{
    public class ModelTrainer
    {
        private readonly MLContext _mlContext;

        public ModelTrainer()
        {
            _mlContext = new MLContext();
        }

        public ITransformer TrainModel(List<SampleData> trainingData, List<string> selectedFeatures)
        {
            if (trainingData == null || trainingData.Count == 0)
                throw new ArgumentException("Training data cannot be null or empty");

            if (selectedFeatures == null || selectedFeatures.Count == 0)
                throw new ArgumentException("Selected features cannot be null or empty");

            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            var pipeline = _mlContext.Transforms.Concatenate("Features", selectedFeatures.ToArray())
                .Append(_mlContext.BinaryClassification.Trainers.FastForest(labelColumnName: "Label", featureColumnName: "Features"));

            var model = pipeline.Fit(dataView);

            return model;
        }

        public float EvaluateModel(ITransformer model, List<SampleData> testData)
        {
            var dataView = _mlContext.Data.LoadFromEnumerable(testData);
            var predictions = model.Transform(dataView);
            var metrics = _mlContext.BinaryClassification.Evaluate(predictions, labelColumnName: "Label");

            return (float)metrics.AreaUnderRocCurve;
        }
    }

    public class SampleData
    {
        [LoadColumn(0)] public bool Label { get; set; }
        // Dynamic features will be mapped automatically by ML.NET pipeline
        [LoadColumn(1)] public float Feature1 { get; set; }
        [LoadColumn(2)] public float Feature2 { get; set; }
    }
}