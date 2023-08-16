namespace BrandPulse.ML.MLModels.SentimentAnalysis
{
    public interface ISentimentAnalysisMLAdapter
    {
        string PredictSentiment(string sentence);
    }
}