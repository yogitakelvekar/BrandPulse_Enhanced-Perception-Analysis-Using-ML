using BrandPulse.Application.Contracts.Features.ETL.Load;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.Entities;

namespace BrandPulse.Application.Features.ETL.Load
{
    public class SearchDataLoad : ISearchDataLoad
    {
        private readonly ILoadStrategy<PostDetailTransformResult, PostDetail> postDetail;
        private readonly ILoadStrategy<WordCloudTransformResult, PostWordCloudData> wordCloud;
        private readonly ILoadStrategy<SentimentTransformResult, PostSentimentData> sentiment;
        private readonly ILoadStrategy<InfluencerTransformResult, PostInfluencerData> influencer;
        private readonly IPostSearchDetailLoadStrategy searchDetail;

        public SearchDataLoad(ILoadStrategy<PostDetailTransformResult, PostDetail> postDetail,
            ILoadStrategy<WordCloudTransformResult, PostWordCloudData> wordCloud,
            ILoadStrategy<SentimentTransformResult, PostSentimentData> sentiment,
            ILoadStrategy<InfluencerTransformResult, PostInfluencerData> influencer,
            IPostSearchDetailLoadStrategy searchDetail)
        {
            this.postDetail = postDetail;
            this.wordCloud = wordCloud;
            this.sentiment = sentiment;
            this.influencer = influencer;
            this.searchDetail = searchDetail;
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
                // You might want to log the exception here for debugging purposes.
                result = false;
            }
            return result;
        }
    }
}
