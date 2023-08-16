using BrandPulse.Application.Contracts.Features.DataScience;
using BrandPulse.Application.Contracts.Features.DataScience.MLWorkflows;
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

        public MLWorkflowManger(ISentimentAnalysisWorkflow sentimentAnalysisWorkflow, IWordcloudAnalysisWorkflow wordcloudAnalysisWorkflow)
        {
            this.sentimentAnalysisWorkflow = sentimentAnalysisWorkflow;
            this.wordcloudAnalysisWorkflow = wordcloudAnalysisWorkflow;
        }

        public async Task<bool> Run(string searchTermId)
        {
            bool result;
            try
            {
                await sentimentAnalysisWorkflow.Run(searchTermId);
                await wordcloudAnalysisWorkflow.Run(searchTermId);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
