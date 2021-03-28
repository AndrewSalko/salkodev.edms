using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace SalkoDev.EDMS.IdentityProvider.Mongo.Db
{
	public interface IDatabase
	{
		IMongoDatabase DB
		{
			get;
		}

	}
}
