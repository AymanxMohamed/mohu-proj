﻿using MOHU.ExternalIntegration.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MOHU.ExternalIntegration.Contracts.Dto.Common
{
    public class ResponseMessage<T>
    {
        public Status Status { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Result { get; set; }
        public static ResponseMessage<T> Failure(string message, T? data = default)
        {
            return new ResponseMessage<T> { Status = Status.Failure, Result = data };
        }
        public static ResponseMessage<T> Success(T data)
        {
            return new ResponseMessage<T> { Status = Status.Success, StatusCode = (int)HttpStatusCode.OK, Result = data };

        }
    }
}