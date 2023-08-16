using BrandPulse.Application.Contracts.Features.DataScience;
using BrandPulse.Application.Contracts.Features.DataScience.DataProcessors;
using BrandPulse.Application.Contracts.Features.DataScience.MLWorkflows;
using BrandPulse.Application.Features.DataScience.SentimentAnalysis;
using BrandPulse.Application.Features.DataScience;
using BrandPulse.ML.Source.DataProcessors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandPulse.ML.MLModels.SentimentAnalysis;
using static BrandPulse_ML.SentimentAnalysisModel;
using Microsoft.Extensions.ML;

namespace BrandPulse.ML
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
            return services;
        }
            
    }
}
