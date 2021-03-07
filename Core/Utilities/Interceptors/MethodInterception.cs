using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public class MethodInterception : MethodInterceptionBaseAttribute
    {
        // Tüm metotlarımızın çatısı
        // Boş metotlar:
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)
        { // Aldığı metot için araya girerek aşağıdaki kurallardan geçer.
            // Add, GetAll, Get vs. hepsi aşağıdaki kurallardan geçerek çalışacak!
            // Her yerde try catch yazılmayıp, tek bir noktada Base yazılıp kullanılır.
            var isSuccess = true;
            OnBefore(invocation); // Metot öncesi çalıştırma, başında, genellikle kullanılan
            try
            { 
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e); // Hata aldığında çalıştırma, hatada, genellikle kullanılan
                throw;
            }
            finally
            { // ister hata alsın almasın final çalışır, else gibi
                if (isSuccess)
                {
                    OnSuccess(invocation); // Başarılı olursa çalıştırma, 
                }
            }
            OnAfter(invocation); // Metottan sonra çalıştırma
        }
    }
}
/* Aspect Oriented yaklaşımını incelediğimizde aslında temel olarak birbiri ile çakışan ilgilerin
 * (cross-cutting) ayrılmasını hedefler.
 * Peki bu ilgiler nelerdir? - Geliştirdiğimiz yazılımlara baktığımızda aslında gerçekleştirdiğimiz 
 * business işlemleri sırasında bir takım işlemleri sürekli olarak gerçekleştiririz.
 * Gerçekleştirmiş olduğumuz Business metotlarının hemen hemen hepsinde,
 * try{
 *      MetodaGelenIlkIstegiLogla();
 *      IstegiYapanKullanicininYetkisiVarmi();
 *      
 *      //Business İslemlerini Gerçeklestir
 *      
 *      IslemSonucunuLogla();
 *      }
 *      
 * catch (Exception exception){
 *      HatayiLogla(exception);
 *      }
 * gibi akışları bulunmaktadır.
 * Bu akışlara ek olarak metotlar içerisinde bir takım diğer ortak işlemler: Loglama, Validasyon,
 * Authentication, ExceptionHandling, Caching...
 * Business katmanında yaptığımız işlemlerin büyük kısmını bu işlemler oluşturur.
 * İşte Aspect Oriented Programming yaklaşımı yukarıda gördüğümüz gibi birbirlerinden farklı olan 
 * ancak çoğu kez kesişen ilgilerin birbirlerinden ayrılmasını amaçlamaktadır. Böylece geliştirilen 
 * yazılımların bakımlarının kolaylaştırılması, ölçeklendirilebilmesi amaçlanmaktadır.
 * Peki yukarıda gördüğümüz birbirleri ile çakışan ilgileri birbirlerinden nasıl ayırırız? - Interceptor 
 * Interceptor’lar belirli noktalarda metot çağrımları sırasında araya girerek çakışan ilgileri 
 * işletmemizi ve yönetmemizi sağlar. Böylece metotların çalışmasından önce veya sonra bir takım işlemleri
 * gerçekleştirebilmekteyiz.
 * Bu şekilde bir yapı geliştirmek için .Net Framework içerisinde bir takım tipler bulunmaktadır. Ancak 
 * baktığımızda hızlı bir şekilde geliştirme yapmak oldukça güçtür.
 * Bunun yerine gerek Microsoft Enterprise Library içerisinde bulunan Unity bloğu olsun gerekse 
 * Castle Windsor gibi kütüphaneler olsun bizlere Interceptor yapısını sağlamak için bir takım 
 * kütüphaneler sunmaktalar.
 */