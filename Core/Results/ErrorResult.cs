using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult() : base(false) // bool success 'i param olarak vermeyiz, zaten atandı
        {
            // Mesaj göndermeden, Base'e(Result'a) başarı durumunu false verip gönderir, set ettirir.

        }

        public ErrorResult(string message) : base(false, message) // bool success 'i param olarak vermeyiz, zaten atandı
        {
            // Base'e(Result'a) başarı durumunu false ve mesajı gönderip set ettirir.
        }
    }
}
