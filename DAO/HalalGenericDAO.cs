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
			XUbertesters.LogInfo ("Getting " + typeof(T).Name + " with id: " + id);
			return await base.Get (id, consistentRead);
		}

		public override async Task<List<T>> GetAll (bool consistentRead)
		{
			XUbertesters.LogInfo ("Getting all " + typeof(T).Name);
			return await base.GetAll (consistentRead);
		}

		public override async Task<long> CountAll (bool consistentRead)
		{
			XUbertesters.LogInfo ("Counting all " + typeof(T).Name);
			return await base.CountAll (consistentRead);
		}

		public override async Task<bool> CreateTable ()
		{
			XUbertesters.LogInfo ("Creating Table: " + typeof(T).Name);
			return await base.CreateTable ();
		}

		public override async Task<bool> DeleteTable ()
		{
			XUbertesters.LogInfo ("Deleting table: " + typeof(T).Name);
			return await base.DeleteTable ();
		}

		public override async Task<bool> Delete (T entity)
		{
			XUbertesters.LogInfo ("Deleting " + typeof(T).Name + " with id " + entity.Id);
			return await base.SaveOrReplace (entity);
		}

		public override async Task<bool> DeleteMultiple (List<T> entities)
		{
			XUbertesters.LogInfo ("Deleting multiple " + typeof(T).Name);
			return await base.DeleteMultiple (entities);
		}

		public override async Task<bool> SaveOrReplace (T entity)
		{
			XUbertesters.LogInfo ("Saving: " + entity);
			return await base.SaveOrReplace (entity);
		}

		public override async Task<bool> SaveOrReplaceMultiple (List<T> entities)
		{
			XUbertesters.LogInfo ("Saving: " + entities);
			return await base.SaveOrReplaceMultiple (entities);
		}

		public override async Task<List<T>> Select (SelectQuery<T> query)
		{
			XUbertesters.LogInfo ("Running Query : " + query);
			return await base.Select (query);
		}
	}
}

