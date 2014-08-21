using System;
using SimpleDBPersistence.DAO;
using System.Threading.Tasks;
using XUbertestersSDK;
using System.Collections.Generic;
using SimpleDBPersistence.Domain;
using SimpleDBPersistence.SimpleDB.Model.Parameters;

namespace HalalGuide.DAO
{
	public abstract class HalalGenericDAO<T> : GenericDAO<T> where T : Entity
	{

		public override async Task<T> Get (string id, bool consistentRead)
		{
			XUbertesters.LogInfo (typeof(T).Name + ": Getting entity with id: " + id);
			return await base.Get (id, consistentRead);
		}

		public override async Task<List<T>> GetAll (bool consistentRead)
		{
			XUbertesters.LogInfo (typeof(T).Name + ": Getting all");
			return await base.GetAll (consistentRead);
		}

		public override async Task<long> CountAll (bool consistentRead)
		{
			XUbertesters.LogInfo (typeof(T).Name + ": Counting all");
			return await base.CountAll (consistentRead);
		}

		public override async Task<bool> CreateTable ()
		{
			XUbertesters.LogInfo (typeof(T).Name + ": Creating Table");
			return await base.CreateTable ();
		}

		public override async Task<bool> DeleteTable ()
		{
			XUbertesters.LogInfo (typeof(T).Name + ": Deleting table");
			return await base.DeleteTable ();
		}

		public override async Task<bool> Delete (T entity)
		{
			XUbertesters.LogInfo (typeof(T).Name + ": Deleting entity with id: " + entity.Id);
			return await base.SaveOrReplace (entity);
		}

		public override async Task<bool> DeleteMultiple (List<T> entities)
		{
			XUbertesters.LogInfo (typeof(T).Name + ": Deleting multiple");
			return await base.DeleteMultiple (entities);
		}

		public override async Task<bool> SaveOrReplace (T entity)
		{
			XUbertesters.LogInfo (typeof(T).Name + ": Saving entity: " + entity);
			return await base.SaveOrReplace (entity);
		}

		public override async Task<bool> SaveOrReplaceMultiple (List<T> entities)
		{
			XUbertesters.LogInfo (typeof(T).Name + ": Saving entities: " + entities);
			return await base.SaveOrReplaceMultiple (entities);
		}

		public override async Task<List<T>> Select (SelectQuery<T> query)
		{
			XUbertesters.LogInfo (typeof(T).Name + ": Running Query : " + query);
			return await base.Select (query);
		}
	}
}

