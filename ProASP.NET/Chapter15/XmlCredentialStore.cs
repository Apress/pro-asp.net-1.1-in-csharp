using System;
using System.Xml;
using System.Xml.XPath;
using System.Web.Security;

namespace Chapter15
{
	public class XmlCredentialStore : ICredentialStore
	{
		private string usersFile;

		public XmlCredentialStore(string usersFile)
		{
			this.usersFile = usersFile;  
		}

		public bool Authenticate(string userName, string password, out string userData)
		{
			userData = null;

			// Pass the call on to the other version of the 
			// Authenticate() method.
			return Authenticate(userName, password);
		}

		public bool Authenticate(string userName, string password)
		{
			// Create the XML document object.
			XmlDocument usersXml = new XmlDocument();

			// Create a namespace manager for the document (we need it later).
			XmlNamespaceManager namespaceManager =
				new XmlNamespaceManager(usersXml.NameTable);

			// The XML document might fail to load so error handling
			// makes sense.
			try
			{
				usersXml.Load(usersFile);
			}
			catch(Exception error)
			{
				// We could not load the XML file so the user can't be 
				// authenticated. Another option is to throw some sort of
				// exception to alert the web page.
				return false;
			}

			// Get the <users> node.
			XmlNode users = usersXml.GetElementsByTagName("users").Item(0);

			// Get the hashing algorithm.
			string hashingAlgorithm = users.Attributes["passwordFormat"].Value;

			string passwordToCompare;
			if ((hashingAlgorithm != null) && (hashingAlgorithm != "Clear"))
			{
				// If a hashing algorith is specified, hash the password.
				passwordToCompare = 
					FormsAuthentication.HashPasswordForStoringInConfigFile(
					password, hashingAlgorithm);
			}
			else
			{
				// Otherwise, use the plain text of the password.
				passwordToCompare = password;
			}

			// Get the root node.
			XmlNode root = usersXml.DocumentElement;

			// Create an XPath expression to match a user node
			// with the correct user name and password.
			// NOTE: You may need to protect against XPath code injection!
			string userXPath = "descendant::user[@userName='" + userName + "' and @password='" + passwordToCompare + "']";

			// Find the matching user node.
			XmlNode matchingUser = root.SelectSingleNode(userXPath,namespaceManager);
			if (matchingUser != null)
			{
				// Perform the final sanity check.
				if ((matchingUser.Attributes["userName"].Value == userName) && 
					(matchingUser.Attributes["password"].Value == passwordToCompare))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}

		}
	}




}
