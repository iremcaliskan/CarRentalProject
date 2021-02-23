using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; } // IResult'tan implement ettiği mesaj ve başarı durumu dışında birde datası olacak, generic olarak alacağı
    }
}
