using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message) : this(success) // Diğer yapıcı metodun success'ine yolla ve set ettir.
        { // Dry Principle
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}
