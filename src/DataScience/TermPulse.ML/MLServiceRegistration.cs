using TermPulse.Application.Contracts.Features.DataScience;
using TermPulse.Application.Contracts.Features.DataScience.DataProcessors;
using TermPulse.Application.Contracts.Features.DataScience.MLWorkflows;
using TermPulse.Application.Features.DataScience.SentimentAnalysis;
using TermPulse.Application.Features.DataScience;
using TermPulse.ML.Source.DataProcessors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermPulse.ML.MLModels.SentimentAnalysis;
using static BrandPulse_ML.SentimentAnalysisModel;
using Microsoft.Extensions.ML;
using TermPulse.Application.Features.DataScience.WordcloudAnalysis;

namespace TermPulse.ML
{
    public static class MLServiceRegistration
    {
        public static IServiceCollection AddMLServices(this IServiceCollection services, IConfiguration configuration)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var modelPath = Path.Combine(basePath, "MLModels", "SentimentAnalysis", "SentimentAnalysisModel.zip");

            services.AddPredictionEnginePool<ModelInput, ModelOutput>()
                .FromFile(modelName: "SentimentAnalysisModel", filePath: modelPath, watchForChanges: false);
            services.AddScoped<IMLWorkflowManger, MLWorkflowManger>();
            services.AddScoped<ISentimentAnalysisWorkflow, SentimentAnalysisWorkflow>();
            services.AddScoped<ISentimentAnalysisMLAdapter,  SentimentAnalysisMLAdapter>();
            services.AddScoped<ISentimentDataProcessor, SentimentDataProcessor>();
            services.AddScoped<IWordcloudAnalysisWorkflow, WordcloudAnalysisWorkflow>();
            services.AddScoped<IWordCloudDataProcessor, WordCloudDataProcessor>();
            return services;
        }
            
    }
}
