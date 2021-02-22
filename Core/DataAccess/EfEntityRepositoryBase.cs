using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public class EfEntityRepositoryBase<TEntity,TContext>
        where TEntity:class,IEntity,new()
        where TContext: DbContext, new()
    { // Bir sınıf/tablo ve bir Context/Veritabanı ile çalışır
        // Aynı veri erişim yöntemini kullanan sınıflar için operasyonlarını tek tek yazmak yerine Base bir sınıfa toplayarak inherit edeceğiz.
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedCar = context.Entry(entity); // Referansı bul
                addedCar.State = EntityState.Added; // Ekleme işlemini yap
                context.SaveChanges(); // Veritabanına yansıt
            }
        }
        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedCar = context.Entry(entity); // Referansı bul
                updatedCar.State = EntityState.Modified; // Güncelleme işlemini yap
                context.SaveChanges(); // Veritabanına yansıt
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedCar = context.Entry(entity); // Referansı bul
                deletedCar.State = EntityState.Deleted; // Silme işlemini yap
                context.SaveChanges(); // Veritabanına yansıt
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
                //DbSet'teki gönderilen TEntity ile uyuşan sınıfı yani veritabanı tablosunu seç, predicate ifadeyi uygula gelen kaydı(record) döndür.
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null // filtre null mı?
                    ? context.Set<TEntity>().ToList() // null ise DbSet'teki gönderilen TEntity yani veritabanı tablosunu seç, liste olarak döndür
                    : context.Set<TEntity>().Where(filter).ToList(); // null değil ise DbSet'teki gönderilen TEntity yani veritabanı tablosunu seç,
                // filtreyi yani predicate, lamda ifadesini koşulunu sağlayanları seç(Where), listede topla ve döndür

            }
        }
    }
}
