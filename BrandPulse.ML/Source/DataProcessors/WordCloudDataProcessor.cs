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
                        var lowerCaseHashtag = hashtag.ToLower();
                        if (hashtagCounts.ContainsKey(lowerCaseHashtag))
                        {
                            hashtagCounts[lowerCaseHashtag]++;
                        }
                        else
                        {
                            hashtagCounts[lowerCaseHashtag] = 1;
                        }
                    }
                }
            }
            return hashtagCounts;
        }

    }
}
