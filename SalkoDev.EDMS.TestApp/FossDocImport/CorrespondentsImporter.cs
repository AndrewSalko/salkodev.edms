using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using Foss.FossDoc.ApplicationServer;
using Foss.FossDoc.ApplicationServer.ObjectDataManagment;
using MongoDB.Driver;
using Converters = Foss.FossDoc.ApplicationServer.Converters;

using EDMSSchema = Foss.FossDoc.ExternalModules.EDMS.Schema;

namespace SalkoDev.EDMS.TestApp.FossDocImport
{
	class CorrespondentsImporter: Foss.TemplateLibrary.Disposable, IWorker
	{
		ISession _Session;
		IMongoCollection<Organization.Data.Organization> _DestCollection;

		public CorrespondentsImporter(string fossdocConnectionString, IMongoCollection<Organization.Data.Organization> destCollection)
		{
			_Session = (ISession)Foss.FossDoc.ApplicationServer.Connection.Connector.Connect(fossdocConnectionString);
			_DestCollection = destCollection ?? throw new ArgumentNullException(nameof(destCollection));
		}

		public void DoWork(BackgroundWorker worker)
		{
			var obj = _Session.ObjectDataManager;

			OID korrID = Converters.OID.FromString("000000001BF41792DCE04348A5A4367D8BABD435");

			OID[] ids = obj.GetChildren(korrID, Foss.FossDoc.ExternalModules.BusinessLogic.Schema.Folder.Attributes.Documents.Tag);

			int blockSize = 50;
			List<OID> procList = new List<OID>();
			foreach (var id in ids)
			{
				procList.Add(id);

				if (procList.Count >= blockSize)
				{
					_ImportCorrespondents(procList.ToArray(), obj);
					procList.Clear();
				}
			}
			if (procList.Count > 0)
			{
				_ImportCorrespondents(procList.ToArray(), obj);
				procList.Clear();
			}


		}

		void _ImportCorrespondents(OID[] ids, IObjectDataManager obj)
		{
			SalkoDev.Organization.Data.Organization[] orgs = _LoadCorrespondents(ids, obj);
			if (orgs == null || orgs.Length == 0)
				return;

			//теперь импорт в базу mongo...
			_DestCollection.InsertMany(orgs);
		}

		SalkoDev.Organization.Data.Organization[] _LoadCorrespondents(OID[] ids, IObjectDataManager obj)
		{
			var allProps = obj.GetProperties(ids, Foss.FossDoc.ApplicationServer.ObjectDataManagment.Schema.PropertyTags.DisplayName,   //0 Имя
				EDMSSchema.PropertyTags.Kontragent.EDMS_FIELD_NAME_LONG,				//1 Длинное имя
				EDMSSchema.PropertyTags.Kontragent.EDMS_FIELD_NOTE,						//2 Описание
				EDMSSchema.PropertyTags.Kontragent.EDMS_FIELD_ADDRESS_PHYSICAL,			//3 физ адрес
				EDMSSchema.PropertyTags.Kontragent.EDMS_FIELD_ADDRESS_LEGAL,			//4 юр адрес
				EDMSSchema.PropertyTags.Kontragent.EDMS_FIELD_PERSONAL_ID_CODE,			//5 код ЕДРПОУ
				EDMSSchema.PropertyTags.Kontragent.EDMS_FIELD_PHONE,					//6 телефон
				EDMSSchema.PropertyTags.Kontragent.PR_EMAIL_ADDRESS						//7 имейл
				);

			List<SalkoDev.Organization.Data.Organization> result = new List<Organization.Data.Organization>(ids.Length);

			for (int i = 0; i < ids.Length; i++)
			{
				var props = allProps[i];
				if (props == null)
					continue;

				SalkoDev.Organization.Data.Organization org = new Organization.Data.Organization();

				List<SalkoDev.Organization.Data.Contact> contacts = null;

				if (props[0].PropertyTag.IsEquals(Foss.FossDoc.ApplicationServer.ObjectDataManagment.Schema.PropertyTags.DisplayName))
				{
					org.Name = props[0].Value.GetstrVal().Trim();
				}

				if (props[1].PropertyTag.IsEquals(EDMSSchema.PropertyTags.Kontragent.EDMS_FIELD_NAME_LONG))
				{
					org.FullName = props[1].Value.GetstrVal().Trim();
				}

				if (props[2].PropertyTag.IsEquals(EDMSSchema.PropertyTags.Kontragent.EDMS_FIELD_NOTE))
				{
					org.Description = props[2].Value.GetstrVal().Trim();
				}

				if (props[3].PropertyTag.IsEquals(EDMSSchema.PropertyTags.Kontragent.EDMS_FIELD_ADDRESS_PHYSICAL))
				{
					org.PhysicalAddress = props[3].Value.GetstrVal().Trim();
				}

				if (props[4].PropertyTag.IsEquals(EDMSSchema.PropertyTags.Kontragent.EDMS_FIELD_ADDRESS_LEGAL))
				{
					org.LegalAddress = props[4].Value.GetstrVal().Trim();
				}

				if (props[5].PropertyTag.IsEquals(EDMSSchema.PropertyTags.Kontragent.EDMS_FIELD_PERSONAL_ID_CODE))
				{
					org.OrganizationCode = props[5].Value.GetstrVal().Trim();
				}

				if (props[6].PropertyTag.IsEquals(EDMSSchema.PropertyTags.Kontragent.EDMS_FIELD_PHONE))
				{
					if (contacts == null)
						contacts = new List<Organization.Data.Contact>();

					var phoneContact = new Organization.Data.Contact
					{
						Value = props[6].Value.GetstrVal().Trim(),
						ValueType = Organization.Data.ContactType.Phone
					};

					contacts.Add(phoneContact);
				}

				if (props[7].PropertyTag.IsEquals(EDMSSchema.PropertyTags.Kontragent.PR_EMAIL_ADDRESS))
				{
					if (contacts == null)
						contacts = new List<Organization.Data.Contact>();

					var emailContact = new Organization.Data.Contact
					{
						Value = props[7].Value.GetstrVal().Trim(),
						ValueType = Organization.Data.ContactType.Email
					};

					contacts.Add(emailContact);
				}

				if (contacts != null)
				{
					org.Contacts = contacts.ToArray();
				}

				result.Add(org);

			}//for

			return result.ToArray();

		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_Session.Dispose();
			}

			base.Dispose(disposing);
		}

	}
}
