using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data) : base(data, true)
        {
            // Aldığı datayı DataResult'a göndererek orada set ettirir.
            // Başarı durumunu(true) DataResult'a gönderir, DataResult ise Result'a göndererek orada set ettirir.
        }

        public SuccessDataResult(T data, string message) : base(data, true, message)
        {
            // Aldığı datayı DataResult'a göndererek orada set ettirir.
            // Başarı durumunu(true) ve Mesajını DataResult'a gönderirir, DataResult ise Result'a göndererek orada set ettirir.
        }

        public SuccessDataResult(string message) : base(default, true, message)
        {
            // Default data değerini(null) DataResult'a gönderir ve set ettirir.
            // Başarı durumunu(true) ve Mesajını DataResult'a gönderirir, DataResult ise Result'a göndererek orada set ettirir.
        }
        public SuccessDataResult() : base(default, true)
        {
            // Default data değerini(null) DataResult'a gönderir ve set ettirir.
            // Başarı durumunu(true) DataResult'a gönderir, DataResult ise Result'a göndererek orada set ettirir.
        }
    }
}
