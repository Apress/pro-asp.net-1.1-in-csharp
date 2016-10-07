using System;
using System.Web;
using System.Xml;
using System.Configuration;

namespace ConfigurationExtension
{
	public class DbConnectionConfigSectionHandler : IConfigurationSectionHandler
	{
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			if (section.ChildNodes.Count > 0)
			{
				DbConnectionConfigSection[] connections = new DbConnectionConfigSection[section.ChildNodes.Count];
				for (int i = 0; i < section.ChildNodes.Count; i++)
				{			
					string connectionString = GetStringValueOfAttribute(section.ChildNodes[i], "connectionString");
					string tableName = GetStringValueOfAttribute(section.ChildNodes[i], "tableName");

					connections[i] =
					  new DbConnectionConfigSection(connectionString, tableName);
				}
				return connections;
			}	
			else
			{
				return null;
			}

		}

		public string GetStringValueOfAttribute(XmlNode node, string attribute)
		{
			XmlNode match = node.Attributes.RemoveNamedItem(attribute);
			if (match == null)
			{
				throw new ConfigurationException("Attribute required: " + attribute);
			}
			else
			{
				return match.Value;
			}
		}
	}

	public class DbConnectionConfigSection
	{
		private string connectionString;
		public string ConnectionString
		{
			get {return connectionString;}
		}

		private string tableName;
		public string TableName
		{
			get {return tableName;}
		}
      
		public DbConnectionConfigSection(string connectionString, string tableName)
		{
			this.connectionString = connectionString;
			this.tableName = tableName;
		}
	}
}
