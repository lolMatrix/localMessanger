using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository
{
    public class Repository<Entity> where Entity : class
    {
        private readonly Context context;
        private DbSet<Entity> entities;

        public Repository()
        {
            context = new Context();
            entities = context.Set<Entity>();
        }

        public Entity[] GetEntities()
        {
            return entities.AsNoTracking().ToArray();
        }

        public IEnumerable<Entity> GetWithInclude(params Expression<Func<Entity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        private IQueryable<Entity> Include(params Expression<Func<Entity, object>>[] includeProperties)
        {
            IQueryable<Entity> query = entities.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public Entity FindById(int id)
        {
            return entities.Find(id);
        }

        public Entity Save(Entity entity)
        {
            entities.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public Entity Update(Entity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return entity;
        }
    }
}
