using BrandPulse.Application.Contracts.Features.ETL.Extract;
using BrandPulse.Application.Contracts.Features.ETL.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL
{
    public class ETLWorkflowManager
    {
        private readonly ISearchDataExtract dataExtract;
        private readonly ISearchDataTransform dataTransform;

        public ETLWorkflowManager(ISearchDataExtract dataExtract, ISearchDataTransform dataTransform)
        {
            this.dataExtract = dataExtract;
            this.dataTransform = dataTransform;
        }

        public async Task<bool> Run(string searchId)
        {
            var data = await dataExtract.ExtractAsync(searchId);
            var transformResult = await dataTransform.TransformAsync(data);
            return true;
        }
    }
}
