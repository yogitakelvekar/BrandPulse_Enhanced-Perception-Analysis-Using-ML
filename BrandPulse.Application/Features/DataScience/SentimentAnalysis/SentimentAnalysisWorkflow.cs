using BrandPulse.Application.Contracts.Features.DataScience.DataProcessors;
using BrandPulse.Application.Contracts.Features.DataScience.MLWorkflows;
using BrandPulse.Application.Contracts.Infrastructure.Persistence;
using BrandPulse.Domain.Entities;
using BrandPulse.ML.MLModels.SentimentAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.DataScience.SentimentAnalysis
{
    public class SentimentAnalysisWorkflow : ISentimentAnalysisWorkflow
    {
        private readonly IPostDetailRepository postDetailRepository;
        private readonly IPostSentimentDataRepository postSentimentDataRepository;
        private readonly ISentimentDataProcessor sentimentDataProcessor;
        private readonly IPostSentimentAnalysisRepository sentimentAnalysisRepository;
        private readonly ISentimentAnalysisMLAdapter sentimentAnalysisML;

        public SentimentAnalysisWorkflow(IPostDetailRepository postDetailRepository,
            IPostSentimentDataRepository postSentimentDataRepository,
            ISentimentDataProcessor sentimentDataProcessor, 
            IPostSentimentAnalysisRepository sentimentAnalysisRepository,
            ISentimentAnalysisMLAdapter sentimentAnalysisML)
        {
            this.postDetailRepository = postDetailRepository;
            this.postSentimentDataRepository = postSentimentDataRepository;
            this.sentimentDataProcessor = sentimentDataProcessor;
            this.sentimentAnalysisRepository = sentimentAnalysisRepository;
            this.sentimentAnalysisML = sentimentAnalysisML;
        }

        public async Task Run(string searchTermId)
        {
            List<PostSentimentAnalysis> sentimentAnalysisList = new List<PostSentimentAnalysis>();
            var postDetails = await postDetailRepository.GetPostDetailBySearchId(searchTermId);
            var sentimentData = await postSentimentDataRepository.GetPostContentByPostDetail(postDetails);
            foreach (var sentiment in sentimentData)
            {
                var sentimentAnalysis = new PostSentimentAnalysis();
                sentimentAnalysis.Id = Guid.NewGuid();
                sentimentAnalysis.SentimentDataId = sentiment.Id;
                sentimentAnalysis.CleanedPostContent = sentimentDataProcessor.PreProcessSingleText(sentiment.PostContent);
                if (sentimentAnalysis.CleanedPostContent != "notext" || sentimentAnalysis.CleanedPostContent != "[NoText]")
                {
                    sentimentAnalysis.Sentiment = sentimentAnalysisML.PredictSentiment(sentimentAnalysis.CleanedPostContent);
                    sentimentAnalysisList.Add(sentimentAnalysis);
                }
            }
            await sentimentAnalysisRepository.BulkInsertAsync(sentimentAnalysisList);
        }
    }
}
