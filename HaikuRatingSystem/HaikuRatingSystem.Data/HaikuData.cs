using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaikuRatingSystem.Data.Repositories;
using HaikuRatingSystem.Models;

namespace HaikuRatingSystem.Data
{
    public class HaikuData : IHaikuData
    {
        private IHaikuRatingSystemContext context;

        private IDictionary<Type, object> repositories;

        public HaikuData()
            : this(new HaikuRatingSystemContext())
        {
        }

        public HaikuData(IHaikuRatingSystemContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<Haiku> Haikus
        {
            get
            {
                return this.GetRepository<Haiku>();
            }
        }

        public IGenericRepository<Rating> Ratings
        {
            get
            {
                return this.GetRepository<Rating>();
            }
        }

        public IGenericRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }
        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}
