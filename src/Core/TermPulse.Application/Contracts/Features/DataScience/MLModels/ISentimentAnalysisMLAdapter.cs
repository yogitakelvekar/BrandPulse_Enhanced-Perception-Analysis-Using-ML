namespace TermPulse.ML.MLModels.SentimentAnalysis
{
    public interface ISentimentAnalysisMLAdapter
    {
        string PredictSentiment(string sentence);
    }
}