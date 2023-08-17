using BrandPulse.Application.Contracts.Features.DataScience;
using BrandPulse.Application.Contracts.Features.DataScience.MLWorkflows;
using BrandPulse.Application.Features.ETL;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.DataScience
{
    public class MLWorkflowManger : IMLWorkflowManger
    {
        private readonly ISentimentAnalysisWorkflow sentimentAnalysisWorkflow;
        private readonly IWordcloudAnalysisWorkflow wordcloudAnalysisWorkflow;
        private readonly ILogger<MLWorkflowManger> logger;

        public MLWorkflowManger(ISentimentAnalysisWorkflow sentimentAnalysisWorkflow, IWordcloudAnalysisWorkflow wordcloudAnalysisWorkflow,
            ILogger<MLWorkflowManger> logger)
        {
            this.sentimentAnalysisWorkflow = sentimentAnalysisWorkflow;
            this.wordcloudAnalysisWorkflow = wordcloudAnalysisWorkflow;
            this.logger = logger;
        }

        public async Task<bool> Run(string searchTermId)
        {
            bool result;
            try
            {
                var sentimentTask = sentimentAnalysisWorkflow.Run(searchTermId);
                var wordcloudTask = wordcloudAnalysisWorkflow.Run(searchTermId);
                await Task.WhenAll(sentimentTask, wordcloudTask);
                //await sentimentAnalysisWorkflow.Run(searchTermId);
                //await wordcloudAnalysisWorkflow.Run(searchTermId);
                result = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex.StackTrace);
                result = false;
            }
            return result;
        }
    }
}
