using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using MongoDB.Driver;

namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db
{
	public class Database: IDatabase
	{
		public const string DATABASE_NAME = "salkodev";

		public Database(string connectionString, bool useSSL, string dataBaseName)
		{
			MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));

			if (useSSL)
			{
				settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
			}

			var mongoClient = new MongoClient(settings);

			if (dataBaseName == null)
				dataBaseName = DATABASE_NAME;

			DB = mongoClient.GetDatabase(dataBaseName);
		}

		public IMongoDatabase DB
		{
			get;
			private set;
		}



	}
}
