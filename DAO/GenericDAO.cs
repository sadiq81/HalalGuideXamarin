using System.Threading.Tasks;
using System.Collections.Generic;
using HalalGuide.Domain;
using Microsoft.WindowsAzure.MobileServices;
using System.Linq;
using HalalGuide.Services;
using System.Linq.Expressions;
using System;
using HalalGuide.Util;

namespace HalalGuide.DAO
{
	public abstract class GenericDAO<T> where T : BaseEntity
	{
		protected DatabaseWrapper database { get { return ServiceContainer.Resolve<DatabaseWrapper> (); } }

		protected MobileServiceClient azure { get { return ServiceContainer.Resolve<MobileServiceClient> (); } }

		public  async Task<T> Get (object id)
		{
			T entity = await azure.GetTable<T> ().LookupAsync (id);
			database.InsertOrReplace (entity);
			return entity;
		}

		public  async Task<List<T>> GetAll ()
		{
			List<T> list = await azure.GetTable<T> ().WithParameters (new Dictionary<string,string> { 
				{ "__includeDeleted", "true" }, 
				{ "__systemProperties","deleted" }
			}).ToListAsync ();

			database.InsertAll<T> (list);

			return list;
		}

		public  async Task Delete (T entity)
		{
			await azure.GetTable<T> ().DeleteAsync (entity);
		}

		public  async Task DeleteMultiple (List<T> entities)
		{
			foreach (T entity in entities) {
				await azure.GetTable<T> ().DeleteAsync (entity);
			}
		}

		public  async Task SaveOrReplace (T entity)
		{
			if (entity.id == null) {
				await azure.GetTable<T> ().InsertAsync (entity);
			} else {
				await azure.GetTable<T> ().UpdateAsync (entity);
			}
		}

		public  async Task SaveOrReplaceMultiple (List<T> entities)
		{
			foreach (T entity in entities) {
				await azure.GetTable<T> ().InsertAsync (entity);
			}
		}

		public  async Task<List<T>> Where (Expression<Func<T,bool>> predicate)
		{
			List<T> list = await azure.GetTable<T> ().WithParameters (new Dictionary<string,string> { 
				{ "__includeDeleted", "true" }, 
				{ "__systemProperties","deleted" }
			}).Where (predicate).ToListAsync ();
			foreach (T entity in list) {
				database.InsertOrReplace (entity);
			}
			return list;
		}
	}
}

