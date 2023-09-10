using TermPulse.Application.Models.ETL.Transform;
using TermPulse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermPulse.Application.Contracts.Features.ETL.Load
{
    public interface ILoadStrategy<TData, TResult>
        where TData : class
        where TResult : class
    {
        Task<IEnumerable<TResult>> LoadAsync(IEnumerable<TData> data);
    }
}
