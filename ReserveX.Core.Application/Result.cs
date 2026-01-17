using ReserveX.Core.Domain.Common.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveX.Core.Application
{
    public class Result< T>
    {
        public bool IsSuccess { get; }
        public T ?Value { get; }
        public string ?Error { get; }
        public ErrorType? ErrorType { get; }


        public Result(T ?value, bool isSuccess, string? error, ErrorType? errorType)
        {
            Value = value; 
            IsSuccess = isSuccess; 
            Error = error; 
            ErrorType = errorType;
        }

        public static Result<T> Success(T value) =>
            new Result<T>(value: value, isSuccess: true, errorType: null, error: null);

        public static Result<T> Failure(string error, ErrorType errorType) =>
          new Result<T>(value: default, isSuccess: false, errorType: errorType, error: error);
    }

   
}
