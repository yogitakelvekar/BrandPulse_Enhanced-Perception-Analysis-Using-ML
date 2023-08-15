using Microsoft.ML;
using Microsoft.ML.Transforms.Text;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandPulse.Application.Contracts.Features.DataScience.DataProcessors;

namespace BrandPulse.ML.Source.DataProcessors
{
    public class SentimentDataProcessor : ISentimentDataProcessor
    {
        private readonly MLContext _mlContext;
        private ITransformer _preProcessingPipeline;

        public SentimentDataProcessor()
        {
            _mlContext = new MLContext();
            _preProcessingPipeline = BuildPreProcessingPipeline();
        }

        public string PreProcessSingleText(string inputText)
        {
            var samples = new List<TextData>
            {
                new TextData { Text = inputText }
            };

            var dataView = _mlContext.Data.LoadFromEnumerable(samples);
            var transformedData = _preProcessingPipeline.Transform(dataView);

            var transformedTexts = _mlContext.Data.CreateEnumerable<TransformedTextData>(transformedData, reuseRowObject: false);

            // Since there's only one input string, there will be only one preprocessed output
            return string.Join(" ", transformedTexts.First().WordsWithoutStopwords);
        }

        public List<string> PreProcessPostContent(List<string> postContent)
        {
            var samples = new List<TextData>();

            foreach (var content in postContent)
            {
                samples.Add(new TextData { Text = content });
            }

            var dataView = _mlContext.Data.LoadFromEnumerable(samples);
            var transformedData = _preProcessingPipeline.Transform(dataView);

            var preProcessedTexts = new List<string>();
            var transformedTexts = _mlContext.Data.CreateEnumerable<TransformedTextData>(transformedData, reuseRowObject: false);

            foreach (var transformedText in transformedTexts)
            {
                preProcessedTexts.Add(string.Join(" ", transformedText.WordsWithoutStopwords));
            }

            return preProcessedTexts;
        }

        private ITransformer BuildPreProcessingPipeline()
        {
            var emptySamples = new List<TextData>();

            var emptyDataView = _mlContext.Data.LoadFromEnumerable(emptySamples);

            // Define the pipeline
            var pipeline = _mlContext.Transforms.CustomMapping<MapInput, MapOutput>(CustomMappings.CleanupText, contractName: null)
                .Append(_mlContext.Transforms.Text.NormalizeText("NormalizedText", "CleanedText", keepDiacritics: false, keepPunctuations: false, keepNumbers: true))
                .Append(_mlContext.Transforms.Text.TokenizeIntoWords("Words", "NormalizedText"))
                .Append(_mlContext.Transforms.Text.RemoveDefaultStopWords("WordsWithoutStopwords", "Words"));

            return pipeline.Fit(emptyDataView);
        }

        public class TextData
        {
            public string Text { get; set; }
        }

        public class TransformedTextData
        {
            public string[] WordsWithoutStopwords { get; set; }
        }

        public class MapInput
        {
            public string Text { get; set; }
        }

        public class MapOutput
        {
            public string CleanedText { get; set; }
        }

        public static class CustomMappings
        {
            public static void CleanupText(MapInput input, MapOutput output)
            {
                // Remove URLs
                var text = Regex.Replace(input.Text, @"http\S+", string.Empty);

                // Remove user mentions
                text = Regex.Replace(text, @"@\S+", string.Empty);

                // Remove hashtags (you can modify this to keep hashtags without '#' if needed)
                text = Regex.Replace(text, @"#\S+", string.Empty);

                // TODO: Add more custom cleanup tasks here as needed

                output.CleanedText = text;
            }
        }
    }
}
