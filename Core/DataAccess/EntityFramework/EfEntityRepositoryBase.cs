using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    // TEntity --> Type Entity
    // TContext --> Type Context
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        // EfEntityRepositoryBase, IEntityRepository'i implemente ettiği için otomatik implemente ettik ve fonksiyonlar buraya geldi
    where TEntity : class, IEntity, new()
    where TContext : DbContext, new()

    {
        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                // Ürün ekleme yaparken saveChanges kısmında bir hata alıyorsan sebebi PrimaryKey'i postman'e kendin girip ekleme yapıyor olmandan kaynaklanabilir
                // PrimaryKey (productID) vermeden Postman aracılığıyla ekleme yapman lazım. Pk kendisi otomatik oluşturuyor.
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
