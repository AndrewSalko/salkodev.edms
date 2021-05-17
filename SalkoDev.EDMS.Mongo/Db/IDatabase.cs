using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace SalkoDev.EDMS.Mongo.Db
{
	public interface IDatabase
	{
		IMongoDatabase DB
		{
			get;
		}

	}
}
