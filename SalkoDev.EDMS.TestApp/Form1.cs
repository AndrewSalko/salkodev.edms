using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SalkoDev.EDMS.TestApp
{
	public partial class Form1 : Form
	{
		const string _FOSSDOC_CONNECTION_STRING_ENV_NAME = "FossDocConnectionStringForSalkoDev";

		public Form1()
		{
			InitializeComponent();
		}

		private void _ButtonStart_Click(object sender, EventArgs e)
		{
			try
			{
				var tick = Environment.TickCount;

				string connectionString = "mongodb://127.0.0.1:27017";
				MongoClient dbClient = new MongoClient(connectionString);

				//список баз на сервере
				//var dbList = dbClient.ListDatabases().ToList();

				var database = dbClient.GetDatabase("EDMS");

				//валидация индексов
				var orgDb = new SalkoDev.Organization.Data.Mongo.OrganizationDB();
				orgDb.Validate(database);

				var collection = database.GetCollection<SalkoDev.Organization.Data.Organization>(SalkoDev.Organization.Data.Mongo.OrganizationDB.COLLECTION_ORGANIZATIONS);

				var org1 = new SalkoDev.Organization.Data.Organization();
				org1.Name = "Кабінет Міністрів України";
				org1.Description = "Наш КМУ";

				collection.InsertOne(org1);

				tick = Environment.TickCount - tick;

				MessageBox.Show(this, $"Виконано за {tick} мс", "Виконано", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				Foss.FUIS.ErrorUI.ExceptionForm.ShowException(ex, "Помилка");
			}
		}

		private void _BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			IWorker worker = (IWorker)e.Argument;
			try
			{
				worker.DoWork(_BackgroundWorker);
			}
			finally
			{
				worker.Dispose();
			}
		}

		Button _CurrentWorkerButton;

		private void _BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			_CurrentWorkerButton.Enabled = true;

			if (e.Error != null)
			{
				Foss.FUIS.ErrorUI.ExceptionForm.ShowException(e.Error, "Помилка");
			}
			else
			{
				MessageBox.Show(this, "Виконано", "Виконано", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		void _DoWork(IWorker worker, Button button)
		{
			if (_BackgroundWorker.IsBusy)
				return;

			_CurrentWorkerButton = button;

			_BackgroundWorker.RunWorkerAsync(worker);
			button.Enabled = false;
		}

		private void _ButtonLoadFromFossDocDB_Click(object sender, EventArgs e)
		{
			string connStrFileName = @"..\..\..\fossdoc-connection.txt";
			string connStr = File.ReadAllText(connStrFileName);

			string connectionString = "mongodb://127.0.0.1:27017";
			MongoClient dbClient = new MongoClient(connectionString);

			var database = dbClient.GetDatabase("EDMS");
			var orgDb = new SalkoDev.Organization.Data.Mongo.OrganizationDB();
			orgDb.Validate(database);

			var collection = database.GetCollection<SalkoDev.Organization.Data.Organization>(SalkoDev.Organization.Data.Mongo.OrganizationDB.COLLECTION_ORGANIZATIONS);

			var correspondentsImporter = new FossDocImport.CorrespondentsImporter(connStr, collection);

			_DoWork(correspondentsImporter, _ButtonLoadFromFossDocDB);
		}
	}
}
