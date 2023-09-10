using Amazon.Runtime.Internal.Util;
using TermPulse.Application.Contracts.Features.ETL.Load;
using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace TermPulse.Application.Features.ETL.Load
{
    public class SearchDataLoad : ISearchDataLoad
    {
        private readonly ILoadStrategy<PostDetailTransformResult, PostDetail> postDetail;
        private readonly ILoadStrategy<WordCloudTransformResult, PostWordCloudData> wordCloud;
        private readonly ILoadStrategy<SentimentTransformResult, PostSentimentData> sentiment;
        private readonly ILoadStrategy<InfluencerTransformResult, PostInfluencerData> influencer;
        private readonly IPostSearchDetailLoadStrategy searchDetail;
        private readonly ILogger<SearchDataLoad> logger;

        public SearchDataLoad(ILoadStrategy<PostDetailTransformResult, PostDetail> postDetail,
            ILoadStrategy<WordCloudTransformResult, PostWordCloudData> wordCloud,
            ILoadStrategy<SentimentTransformResult, PostSentimentData> sentiment,
            ILoadStrategy<InfluencerTransformResult, PostInfluencerData> influencer,
            IPostSearchDetailLoadStrategy searchDetail, ILogger<SearchDataLoad> logger)
        {
            this.postDetail = postDetail;
            this.wordCloud = wordCloud;
            this.sentiment = sentiment;
            this.influencer = influencer;
            this.searchDetail = searchDetail;
            this.logger = logger;
        }

        public async Task<bool> LoadAsync(FinalTransformResult transformResult)
        {
            bool result;
            try
            {
                var searchDetailTask = searchDetail.LoadAsync(transformResult);
                var postDetailTask = postDetail.LoadAsync(transformResult.PostDataTransformResult);
                var wordCloudTask = wordCloud.LoadAsync(transformResult.WordCloudTransformResult);
                var sentimentTask = sentiment.LoadAsync(transformResult.SentimentTransformResult);
                var influencerTask = influencer.LoadAsync(transformResult.InfluencerTransformResult);
                Task.WaitAll(searchDetailTask, postDetailTask, wordCloudTask, sentimentTask, influencerTask);
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
