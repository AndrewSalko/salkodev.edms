using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalkoDev.EDMS.IdentityProvider.Mongo.Db;
using SalkoDev.EDMS.IdentityProvider.Mongo.Db.Users;
using SalkoDev.EDMS.Mongo.Db;

namespace SalkoDev.EDMS.TestApp
{
	class ManyUserCreate
	{
		/// <summary>
		/// Test#123
		/// </summary>
		const string _PASS_HASH = "AQAAAAEAACcQAAAAEO4d/NXkBBTtkCj+KbQKOLw4/rWxeCSiGx/m/JmZKdFE0D8ofpnMscXfXlaPB7Nscw==";

		static readonly string[] _TEST_FIRST_NAMES =
		{
			"Andrew", "Liam", "Olivia", "Noah", "Emma", "Oliver", "Ava", "Elijah", "Charlotte", "William", "Sophia",
			"James", "Amelia", "Benjamin", "Isabella", "Lucas", "Mia", "Henry", "Evelyn", "Alexander", "Harper"
		};

		static readonly string[] _TEST_SECOND_NAMES =
		{
			"Smith","Johnson","Williams","Brown","Jones","Garcia","Miller","Davis","Rodriguez","Martinez","Hernandez","Lopez",
			"Gonzalez","Wilson","Anderson","Thomas","Taylor","Moore","Jackson","Martin"
		};

		UsersCollection _Users;

		Random _Rnd;

		public ManyUserCreate()
		{
			string connString = "mongodb://localhost:27017/?readPreference=primary&appname=salkodev&ssl=false";
			string dbName = "salkodev-tests";

			var db = new Database(connString, false, dbName);

			_Users = new UsersCollection(db, null, true);

			_Rnd = new Random();
		}

		public void Create(int usersCount)
		{

			for (int i = 0; i < usersCount; i++)
			{
				string userFullName = _ComposeRandomName(out string email);

				var user = new User
				{
					Email = email,
					EmailConfirmed = true,
					OrganizationUID = Guid.NewGuid().ToString(),
					PasswordHash = _PASS_HASH,
					UserName = userFullName
				};

				_Users.Users.InsertOne(user);
			}

		}

		string _ComposeRandomName(out string email)
		{
			int indName = _Rnd.Next(_TEST_FIRST_NAMES.Length);
			int indSec = _Rnd.Next(_TEST_SECOND_NAMES.Length);

			string fullName = $"{_TEST_FIRST_NAMES[indName]} {_TEST_SECOND_NAMES[indSec]}";

			string g = Guid.NewGuid().ToString().Replace("-", string.Empty);

			email = $"{_TEST_FIRST_NAMES[indName]}.{_TEST_SECOND_NAMES[indSec]}.{g}@gmail.com".ToLower();

			return fullName;
		}

	}
}
