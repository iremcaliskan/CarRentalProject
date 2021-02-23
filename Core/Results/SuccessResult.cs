using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult() : base(true) // bool success 'i param olarak vermeyiz, zaten atandı
        {
            // Mesaj göndermeden, base'e Result'a başarı durumunu true gönderip set ettirir.
        }

        public SuccessResult(string message) : base(true, message) // bool success 'i param olarak vermeyiz, zaten atandı
        {
            // Base'e (Result'a) başarı durumunu true ve mesajı gönderip set ettirir.
        }
    }
}
