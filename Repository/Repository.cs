using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return entities.ToArray();
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
