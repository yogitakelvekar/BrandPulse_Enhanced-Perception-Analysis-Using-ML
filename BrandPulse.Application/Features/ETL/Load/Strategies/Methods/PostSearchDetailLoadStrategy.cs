using BrandPulse.Application.Contracts.Features.ETL.Load;
using BrandPulse.Application.Contracts.Infrastructure.Persistence;
using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Features.ETL.Load.Strategies.Methods
{
    public class PostSearchDetailLoadStrategy : IPostSearchDetailLoadStrategy
    {
        private readonly IPostSearchDetailRepository postSearchDetailRepository;

        public PostSearchDetailLoadStrategy(IPostSearchDetailRepository postSearchDetailRepository)
        {
            this.postSearchDetailRepository = postSearchDetailRepository;
        }

        public async Task<bool> LoadAsync(FinalTransformResult data)
        {
            var postSearchDetail = new PostSearchDetail()
            {
                SearchTermId = data.SearchTermId,
                SearchTerm = data.SearchTerm,
                SearchDateTime = data.SearchDateTime,
            };
            await postSearchDetailRepository.AddAsync(postSearchDetail);
            var result = await postSearchDetailRepository.SaveChangesAsync();
            return result;
        }
    }
}
