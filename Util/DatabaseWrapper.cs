using System;
using SQLite;
using System.Collections.Generic;

namespace HalalGuide.Util
{
	public class DatabaseWrapper : IDisposable
	{
		// Fields.
		private  readonly SQLiteConnection Connection;
		private readonly object Lock = new object ();

		public DatabaseWrapper (string databasePath)
		{
			if (string.IsNullOrEmpty (databasePath))
				throw new ArgumentException ("Database path cannot be null or empty.");

			Connection = new SQLiteConnection (databasePath);


		}

		public int CreateTable<T> ()
		{
			lock (this.Lock) {
				return Connection.CreateTable<T> ();
			}
		}

		public TableQuery<T> Table<T> () where T : new()
		{
			lock (this.Lock) {
				return Connection.Table<T> ();
			}
		}

		public List<T> Query<T> (string query, params object[] args) where T : new()
		{
			lock (this.Lock) {
				return Connection.Query<T> (query, args);
			}
		}

		public int ExecuteNonQuery (string sql, params object[] args)
		{
			lock (this.Lock) {
				return Connection.Execute (sql, args);
			}
		}

		public T ExecuteScalar<T> (string sql, params object[] args)
		{
			lock (this.Lock) {
				return Connection.ExecuteScalar<T> (sql, args);
			}
		}

		public void Insert<T> (T entity)
		{
			lock (this.Lock) {
				Connection.Insert (entity);
			}
		}

		public void InsertOrReplace<T> (T entity)
		{
			lock (this.Lock) {
				Connection.InsertOrReplace (entity);
			}
		}

		public void InsertAll<T> (List<T> entities)
		{
			lock (this.Lock) {
				Connection.InsertAll (entities);
			}
		}

		public void Update<T> (T entity)
		{
			lock (this.Lock) {
				Connection.Update (entity);
			}
		}

		public void UpdateAll<T> (List<T> entities)
		{
			lock (this.Lock) {
				Connection.UpdateAll (entities);
			}
		}

		public void Upsert<T> (T entity)
		{
			lock (this.Lock) {
				var rowCount = Connection.Update (entity);

				if (rowCount == 0) {
					Connection.Insert (entity);
				}
			}
		}

		public void Delete<T> (T entity)
		{
			lock (this.Lock) {
				Connection.Delete (entity);
			}
		}

		public void Dispose ()
		{
			Connection.Dispose ();
		}
	}
}

