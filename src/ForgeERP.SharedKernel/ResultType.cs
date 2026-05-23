using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.SharedKernel
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public string Error { get;  }

        protected Result(bool isSuccess, string error)
        {
            if(!isSuccess && string.IsNullOrWhiteSpace(error))
            {
                throw new ArgumentException("Error message cannot be empty.", nameof(error));
            }
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return new(true, null);
        }

        public static Result Failure(string error)
        {
            return new(false, error);
        }
    }


    public class Result<TValue>: Result
    {
        private readonly TValue _value;

        public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("Invalid operation.");

        private Result(TValue value, bool isSuccess, string error) : base(isSuccess, error)
        {
            _value = value;
        }


        public static Result<TValue> Success(TValue value) => new(value, true, null);

        public static new Result<TValue> Failure(string error) => new(default, false, error);
    }
}
