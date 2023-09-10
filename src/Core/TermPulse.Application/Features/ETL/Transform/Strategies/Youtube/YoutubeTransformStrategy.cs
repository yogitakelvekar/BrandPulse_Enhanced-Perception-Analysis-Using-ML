using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies;
using TermPulse.Application.Contracts.Features.ETL.Transform.Strategies.Methods;
using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.SocialMedia.Youtube;

namespace TermPulse.Application.Features.ETL.Transform.Strategies.Reddit
{
    public class YoutubeTransformStrategy : ITransformStrategy
    {
        public IEnumerable<YouTubeVideo> Data { get; set; }

        private readonly IPostDataTransform<YouTubeVideo> postData;
        private readonly ISentimentDataTransform<YouTubeVideo> sentimentData;
        private readonly IWordCloudDataTransform<YouTubeVideo> wordCloudData;
        private readonly IInfluencerDataTransform<YouTubeVideo> influencerData;

        public YoutubeTransformStrategy(IPostDataTransform<YouTubeVideo> postData, ISentimentDataTransform<YouTubeVideo> sentimentData, 
            IWordCloudDataTransform<YouTubeVideo> wordCloudData, IInfluencerDataTransform<YouTubeVideo> influencerData)
        {
            this.postData = postData;
            this.sentimentData = sentimentData;
            this.wordCloudData = wordCloudData;
            this.influencerData = influencerData;
        }

        public async Task<FinalTransformResult> TransformAsync()
        {
            var postDataResult = await postData.TransformAsync(Data);
            var sentimentDataTask = sentimentData.TransformAsync(Data, postDataResult); 
            var wordCloudDataTask = wordCloudData.TransformAsync(Data, postDataResult);
            var influencerDataTask = influencerData.TransformAsync(Data, postDataResult);

            await Task.WhenAll(sentimentDataTask, wordCloudDataTask, influencerDataTask);

            var results = new FinalTransformResult();
            results.AddPostDataTransformResult(postDataResult);
            results.AddSentimentTransformResult(sentimentDataTask.Result);
            results.AddWordCloudTransformResult(wordCloudDataTask.Result);
            results.AddInfluencerTransformResult(influencerDataTask.Result);
            return results;
        }
    }
}
