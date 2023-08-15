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

        public MLWorkflowManger(ISentimentAnalysisWorkflow sentimentAnalysisWorkflow)
        {
            this.sentimentAnalysisWorkflow = sentimentAnalysisWorkflow;
        }

        public async Task<bool> Run(string searchTermId)
        {
            bool result;
            try
            {
                await sentimentAnalysisWorkflow.Run(searchTermId);
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
