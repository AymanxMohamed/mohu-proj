using Core.Domain.ErrorHandling.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.Integration.Application.Kidana.Common.Dtos.Responses
{
    public class KidanaResponseBase<TData>
    {
        public string Status { get; init; } = null!; // "success" or "error"
        public string Message { get; init; } = null!;
        public TData? Data { get; init; }

        public ErrorOr<TData> EnsureSuccess() =>
            Status.Equals("success", StringComparison.OrdinalIgnoreCase) && Data != null
                ? Data
                : Error.Validation("KIDANA_ERROR", Message);
    }
}
