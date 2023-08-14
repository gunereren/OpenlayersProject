using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter);
        // Veritabanından T (Product) türünde bir nesne çekmek
        // Her türlü parametreyi gönderebiliyor olmak için link expression'lar kullanıldı.  
        IList<T> GetList(Expression<Func<T, bool>> filter = null);
        // İlgili nesnenin listesini gönderecek

        void Add(T entity);
        // Gönderilen T nesnesini veritabanına ekleyecek

        void Update(T entity);
        // Gönderilen T nesnesini update edecek

        void Delete(T entity);
        // Belirtilen T nesnesini veritabanından silecek

    }
}
