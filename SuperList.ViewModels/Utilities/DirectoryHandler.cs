using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperList.ViewModels.Utilities
{
	public static class DirectoryHandler
	{
		/// <summary>
		/// Retrieves the name of the executing program
		/// </summary>
		/// <returns>String containing the name of the executing program</returns>
		public static string GetAssemblyName()
		{
			Assembly assembly = Assembly.GetEntryAssembly();

			// Check to see if the project is being viewed in a designer
			if (assembly == null)
				return null;

			return assembly.GetName().Name;
		}

		/// <summary>
		/// Retrieves the location (folder) where the application is running from
		/// </summary>
		/// <returns>String containing the assemblie's path</returns>
		public static string GetAssemblyPath()
		{
			Assembly assembly = Assembly.GetEntryAssembly();

			// Check to see if the project is being viewed in a designer
			if (assembly == null)
				return null;

			return Path.GetDirectoryName(assembly.Location);
		}

		/// <summary>
		/// Retrieve the [User AppData]\[Company]\[Title]
		/// </summary>
		/// <returns>User's AppData folder with the app's company and title appended</returns>
		public static string GetAppDataPath()
		{
			Assembly assembly = Assembly.GetEntryAssembly();
			AssemblyCompanyAttribute companyAttr =
				AssemblyCompanyAttribute.GetCustomAttribute(assembly,
				typeof(AssemblyCompanyAttribute)) as AssemblyCompanyAttribute;
			AssemblyTitleAttribute titleAttr =
				AssemblyTitleAttribute.GetCustomAttribute(assembly,
				typeof(AssemblyTitleAttribute)) as AssemblyTitleAttribute;
			string path = Path.Combine(Environment.GetFolderPath(
				Environment.SpecialFolder.ApplicationData),
				companyAttr.Company, titleAttr.Title);

			VerifyCreatePath(path);
			return path;
		}

		/// <summary>
		/// Retrieve the [My Documents]\[Company]\[Title]
		/// </summary>
		/// <returns>User's 'My Documents' folder with the app's company and title appended</returns>
		public static string GetPersonalPath()
		{
			Assembly assembly = Assembly.GetEntryAssembly();
			AssemblyCompanyAttribute companyAttr =
				AssemblyCompanyAttribute.GetCustomAttribute(assembly,
				typeof(AssemblyCompanyAttribute)) as AssemblyCompanyAttribute;
			AssemblyTitleAttribute titleAttr =
				AssemblyTitleAttribute.GetCustomAttribute(assembly,
				typeof(AssemblyTitleAttribute)) as AssemblyTitleAttribute;
			string path = Path.Combine(Environment.GetFolderPath(
				Environment.SpecialFolder.Personal),
				companyAttr.Company, titleAttr.Title);

			VerifyCreatePath(path);
			return path;
		}

		/// <summary>
		/// Verifies that a path exists, and if it does not, it will create it
		/// </summary>
		/// <param name="path">Path to be verified</param>
		public static bool VerifyCreatePath(string path)
		{
			try
			{
				// Verify that the path exists
				if (!Directory.Exists(path))
				{
					// It don't exist :( Create it
					DirectoryInfo dirInfo = Directory.CreateDirectory(path);

					if (!dirInfo.Exists)
						return false;
				}

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
