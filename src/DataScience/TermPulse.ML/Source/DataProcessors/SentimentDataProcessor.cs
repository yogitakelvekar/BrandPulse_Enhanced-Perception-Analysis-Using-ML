using Microsoft.ML;
using Microsoft.ML.Transforms.Text;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermPulse.Application.Contracts.Features.DataScience.DataProcessors;

namespace TermPulse.ML.Source.DataProcessors
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
            
            if(transformedTexts.First() == null || transformedTexts.First().WordsWithoutStopwords == null)
            {
                Console.WriteLine($"{inputText} - transformedTexts null");
                return "[NoText]";
            }

            // Since there's only one input string, there will be only one preprocessed output
            return string.Join(" ", transformedTexts.First().WordsWithoutStopwords);
        }

        private ITransformer BuildPreProcessingPipeline()
        {
            var emptySamples = new List<TextData>();

            var emptyDataView = _mlContext.Data.LoadFromEnumerable(emptySamples);

            // Define the pipeline
            var pipeline = _mlContext.Transforms.CustomMapping<MapInput, MapOutput>(CustomMappings.HandleMissingText, contractName: null)
                .Append(_mlContext.Transforms.CustomMapping<MapInput, MapOutput>(CustomMappings.CleanupText, contractName: null))
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
            public static void HandleMissingText(MapInput input, MapOutput output)
            {
                if (string.IsNullOrEmpty(input.Text))
                {
                    output.CleanedText = "[NoText]"; // replace with any placeholder or default text
                    return;
                }
                output.CleanedText = input.Text;
            }
            public static void CleanupText(MapInput input, MapOutput output)
            {
                string text = input.Text;

                // Remove URLs
                // text = Regex.Replace(input.Text, @"http\S+", string.Empty);

                // Remove user mentions
                text = Regex.Replace(text, @"@\S+", string.Empty);

                // Remove hashtags (you can modify this to keep hashtags without '#' if needed)
                text = Regex.Replace(text, @"#\S+", string.Empty);

                // Remove emojis
                text = RemoveEmojis(text);

                if (string.IsNullOrWhiteSpace(text))
                {
                    text = "[NoText]";
                }

                // TODO: Add more custom cleanup tasks here as needed

                output.CleanedText = text;
            }

            private static string RemoveEmojis(string text)
            {
                // Emoji Unicode blocks
                var emojiPattern =
                    @"[\u263a-\u269c\u2600-\u26c3\u26e0-\u26ff\u2700-\u27bf\u2300-\u23ff\u2B05-\u2b55\u2934-\u2935\u3297-\u3299" +
                    @"\uD83C][\uDC00-\uDFFF]|[\uD83D][\uDC00-\uDFFF]|[\uD83E][\uDD10-\uDDFF]|[\uD83E][\uDC00-\uDCFF]|[\u2600-\u26FF]|" +
                    @"[\u2700-\u27BF]|\uD83D\uDE00";

                return Regex.Replace(text, emojiPattern, string.Empty);
            }
        }
    }
}
