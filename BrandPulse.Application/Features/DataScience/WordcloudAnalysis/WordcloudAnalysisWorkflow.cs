using BrandPulse.Application.Contracts.Features.DataScience.MLWorkflows;
using BrandPulse.Application.Contracts.Infrastructure.Persistence;
using BrandPulse.Domain.Entities;
using BrandPulse.ML.Source.DataProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.DataScience.WordcloudAnalysis
{
    public class WordcloudAnalysisWorkflow : IWordcloudAnalysisWorkflow
    {
        private readonly IPostDetailRepository postDetailRepository;
        private readonly IPostWordCloudDataRepository wordCloudDataRepository;
        private readonly IWordCloudDataProcessor wordCloudDataProcessor;
        private readonly IPostWordCloudAnalysisRepository postWordCloudAnalysisRepository;

        public WordcloudAnalysisWorkflow(IPostDetailRepository postDetailRepository,
            IPostWordCloudDataRepository wordCloudDataRepository, 
            IWordCloudDataProcessor wordCloudDataProcessor,
            IPostWordCloudAnalysisRepository postWordCloudAnalysisRepository)
        {
            this.postDetailRepository = postDetailRepository;
            this.wordCloudDataRepository = wordCloudDataRepository;
            this.wordCloudDataProcessor = wordCloudDataProcessor;
            this.postWordCloudAnalysisRepository = postWordCloudAnalysisRepository;
        }

        public async Task Run(string searchTermId)
        {
            var postDetails = await postDetailRepository.GetPostDetailBySearchId(searchTermId);
            var wordCloudData = await wordCloudDataRepository.GetPostContentByPostDetail(postDetails);
            var hashtagDirectory = wordCloudDataProcessor.ExtractAndCountHashtags(wordCloudData);
            var wordCloudAnalysisData = MapToWordCloudAnalysis(hashtagDirectory, searchTermId);
            await postWordCloudAnalysisRepository.BulkInsertAsync(wordCloudAnalysisData);
        }

        private List<PostWordCloudAnalysis> MapToWordCloudAnalysis(Dictionary<string, int> hashtagCounts, string searchTermId)
        {
            return hashtagCounts.Select(kvp => new PostWordCloudAnalysis
            {
                Id = Guid.NewGuid(),
                SearchTermId = searchTermId,
                Hashtag = kvp.Key,
                Count = kvp.Value
            }).ToList();
        }
    }
}
