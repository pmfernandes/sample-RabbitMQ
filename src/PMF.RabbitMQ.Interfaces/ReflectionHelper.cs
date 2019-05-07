namespace PMF.RabbitMQ.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// 
	/// </summary>
	public static class ReflectionHelper
	{

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <typeparam name="TInterface">The type of the interface.</typeparam>
		/// <param name="location">The location.</param>
		/// <param name="name">The name.</param>
		/// <param name="typeName">Name of the type.</param>
		/// <returns></returns>
		/// <exception cref="System.IO.FileNotFoundException"></exception>
		/// <exception cref="System.NotImplementedException"></exception>
		public static TInterface GetInstance<TInterface>(string location, string name, string typeName)
		where TInterface : class
		{
			var assemblyPath = $@"{location}\{name}.dll";
			if (!File.Exists(assemblyPath))
			{
				throw new FileNotFoundException($"Assembly not found: {assemblyPath}");
			}

			var assembly = Assembly.LoadFrom(assemblyPath);
			var assemblyType = GetLoadableTypes(assembly).FirstOrDefault(t => t.Name.Equals(typeName));

			if (assemblyType != null)
			{
				return (TInterface)assemblyType.GetConstructor(new Type[] { })?.Invoke(null);
			}
			else
			{
				// Error checking is needed to help catch instances where
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Gets the loadable types.
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">assembly</exception>
		private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException(nameof(assembly));
			}

			try
			{
				return assembly.GetTypes();
			}
			catch (ReflectionTypeLoadException e)
			{
				return e.Types.Where(t => t != null);
			}
		}
	}
}
