using BrandPulse.Application.Models.ETL.Transform;
using BrandPulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandPulse.Application.Contracts.Features.ETL.Load
{
    public interface ILoadStrategy<TData, TResult>
        where TData : class
        where TResult : class
    {
        Task<IEnumerable<TResult>> LoadAsync(IEnumerable<TData> data);
    }
}
