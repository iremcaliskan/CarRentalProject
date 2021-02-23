using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data) : base(data, false)
        {
            // Aldığı datayı DataResult'a göndererek orada set ettirir.
            // Başarı durumunu(false) DataResult'a gönderir, DataResult ise Result'a göndererek orada set ettirir.
        }

        public ErrorDataResult(T data, string message) : base(data, false, message)
        {
            // Aldığı datayı DataResult'a göndererek orada set ettirir.
            // Başarı durumunu(false) ve Mesajını DataResult'a gönderir, DataResult ise Result'a göndererek orada set ettirir.
        }

        public ErrorDataResult(string message) : base(default, false, message)
        {
            // Default data değerini(null) DataResult'a gönderir ve set ettirir.
            // Başarı durumunu(false) ve Mesajını DataResult'a gönderirir, DataResult ise Result'a göndererek orada set ettirir.
        }
        public ErrorDataResult() : base(default, false)
        {
            // Default data değerini(null) DataResult'a gönderir ve set ettirir.
            // Başarı durumunu(false) DataResult'a gönderir, DataResult ise Result'a göndererek orada set ettirir.
        }
    }
}
