using BrandPulse.Application.Contracts.Features.DataScience.DataProcessors;
using BrandPulse.Application.Contracts.Features.DataScience.MLWorkflows;
using BrandPulse.Application.Contracts.Infrastructure.Persistence;
using BrandPulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.DataScience.SentimentAnalysis
{
    public class SentimentAnalysisWorkflow : ISentimentAnalysisWorkflow
    {
        private readonly IPostSentimentDataRepository postSentimentDataRepository;
        private readonly ISentimentDataProcessor sentimentDataProcessor;
        private readonly IPostSentimentAnalysisRepository sentimentAnalysisRepository;

        public SentimentAnalysisWorkflow(IPostSentimentDataRepository postSentimentDataRepository,
            ISentimentDataProcessor sentimentDataProcessor, IPostSentimentAnalysisRepository sentimentAnalysisRepository)
        {
            this.postSentimentDataRepository = postSentimentDataRepository;
            this.sentimentDataProcessor = sentimentDataProcessor;
            this.sentimentAnalysisRepository = sentimentAnalysisRepository;
        }

        public async Task Run(string searchTermId)
        {
            List<PostSentimentAnalysis> sentimentAnalysisList = new List<PostSentimentAnalysis>();
            var sentimentData = await postSentimentDataRepository.GetPostContentBySearchId(searchTermId);
            foreach (var sentiment in sentimentData)
            {
                var sentimentAnalysis = new PostSentimentAnalysis();
                sentimentAnalysis.Id = Guid.NewGuid();
                sentimentAnalysis.SentimentDataId = sentiment.Id;
                sentimentAnalysis.CleanedPostContent = sentimentDataProcessor.PreProcessSingleText(sentiment.PostContent);
                sentimentAnalysisList.Add(sentimentAnalysis);
            }
            await sentimentAnalysisRepository.BulkInsertAsync(sentimentAnalysisList);
        }
    }
}
