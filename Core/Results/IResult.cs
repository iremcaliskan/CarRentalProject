using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public interface IResult
    {
        // void metotların döndürdüğü bir tip yoktur, void metotları başarı durumuna göre Mesaj Gönderebilen metotlara çevirdik.
        bool Success { get; } // get = read- only, sadece get'i verilen property Constructure içerisinde set edilebilir.
        string Message { get; }
    }
}
