using BrandPulse.Application.Contracts.Infrastructure.Persistence;
using BrandPulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.ML.Source.DataProcessors
{
    public class WordCloudDataProcessor : IWordCloudDataProcessor
    {
        public Dictionary<string, int> ExtractAndCountHashtags(IEnumerable<PostWordCloudData> data)
        {
            Dictionary<string, int> hashtagCounts = new Dictionary<string, int>();

            foreach (var record in data)
            {
                if (record?.Hashtags?.Any() ?? false)
                {
                    var hashtags = record.Hashtags.Split(',');

                    foreach (var hashtag in hashtags)
                    {
                        if (hashtagCounts.ContainsKey(hashtag))
                        {
                            hashtagCounts[hashtag]++;
                        }
                        else
                        {
                            hashtagCounts[hashtag] = 1;
                        }
                    }
                }
            }
            return hashtagCounts;
        }

    }
}
