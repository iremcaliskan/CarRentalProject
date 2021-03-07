using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool success, string message) : base(success, message) // Başarı durumunu ve mesajını Result'a(base'i) gönderip orada set edecek.
        {
            Data = data; // Aldığı datayı kendi içinde set edecek 
        }

        public DataResult(T data, bool success) : base(success) // Başarı durumunu Result'a(base'i) gönderip orada set edecek.
        {
            Data = data; // Aldığı datayı kendi içinde set edecek 
        }
       
        public T Data { get; }
    }
}
