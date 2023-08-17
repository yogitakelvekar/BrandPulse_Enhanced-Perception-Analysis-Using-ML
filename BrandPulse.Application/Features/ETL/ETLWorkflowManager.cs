using Amazon.Runtime.Internal.Util;
using BrandPulse.Application.Contracts.Features.ETL;
using BrandPulse.Application.Contracts.Features.ETL.Extract;
using BrandPulse.Application.Contracts.Features.ETL.Load;
using BrandPulse.Application.Contracts.Features.ETL.Transform;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL
{
    public class ETLWorkflowManager : IETLWorkflowManager
    {
        private readonly ISearchDataExtract dataExtract;
        private readonly ISearchDataTransform dataTransform;
        private readonly ISearchDataLoad dataLoad;
        private readonly ILogger<ETLWorkflowManager> logger;

        public ETLWorkflowManager(ISearchDataExtract dataExtract, ISearchDataTransform dataTransform, ISearchDataLoad dataLoad,
            ILogger<ETLWorkflowManager> logger)
        {
            this.dataExtract = dataExtract;
            this.dataTransform = dataTransform;
            this.dataLoad = dataLoad;
            this.logger = logger;
        }

        public async Task<bool> Run(string searchId)
        {
            bool result;
            try
            {
                var data = await dataExtract.ExtractAsync(searchId);
                var transformResult = await dataTransform.TransformAsync(data);
                await dataLoad.LoadAsync(transformResult);
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
