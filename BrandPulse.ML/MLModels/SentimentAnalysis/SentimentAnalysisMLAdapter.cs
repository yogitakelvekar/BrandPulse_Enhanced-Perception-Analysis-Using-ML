using BrandPulse_ML;
using Microsoft.Extensions.ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BrandPulse_ML.SentimentAnalysisModel;

namespace BrandPulse.ML.MLModels.SentimentAnalysis
{
    public class SentimentAnalysisMLAdapter : ISentimentAnalysisMLAdapter
    {
        private readonly PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool;

        public SentimentAnalysisMLAdapter(PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
        {
            this.predictionEnginePool = predictionEnginePool;
        }
        public string PredictSentiment(string sentence)
        {
            //predictionEnginePool.ModelInput()
            //{
            //    Selected_text = sentence,
            //};
            //var modelOutput = SentimentAnalysisModel.Predict(sampleData);
            var modelOutput = predictionEnginePool.Predict(modelName: "SentimentAnalysisModel", example: new ModelInput { Selected_text = sentence });
            return modelOutput.PredictedLabel;
        }
    }
}
