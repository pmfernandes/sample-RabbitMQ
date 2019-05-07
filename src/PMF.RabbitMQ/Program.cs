namespace PMF.RabbitMQ
{
	using System;
	using Interfaces;

	/// <summary>
	/// 
	/// </summary>
	class Program
	{
		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		static void Main(string[] args)
		{
			var sourceDirectory = @"C:\Stuff\Workspaces\Research&Development\RabbitMQ\src\";
			var readMessageAssemblyLocation = sourceDirectory + @"PMF.RabbitMQ.Sample1.ReadMessage\bin\Debug";
			var readMessageAssemblyName = "PMF.RabbitMQ.Sample1.ReadMessage";
			var readMessageAssemblyTypeName = "ReadMessage";

			var readMessage = ReflectionHelper.GetInstance<IReadMessage>(readMessageAssemblyLocation, readMessageAssemblyName, readMessageAssemblyTypeName);

			var publishMessageAssemblyLocation = sourceDirectory + @"PMF.RabbitMQ.Sample1.PublishMessage\bin\Debug";
			var publishMessageAssemblyName = "PMF.RabbitMQ.Sample1.PublishMessage";
			var publishMessageAssemblyTypeName = "PublishMessage";

			var publishMessage1 = ReflectionHelper.GetInstance<IPublishMessage>(publishMessageAssemblyLocation, publishMessageAssemblyName, publishMessageAssemblyTypeName);

			publishMessage1.Execution("hello", "1 - Hello World!.");
			publishMessage1.Execution("hello", "2 - Hello World!..");
			publishMessage1.Execution("hello", "3 - Hello World!...");
			publishMessage1.Execution("hello", "4 - Hello World!....");
			publishMessage1.Execution("hello", "5 - Hello World!.....");

			readMessage.Execution("hello");

			Console.ReadLine();
		}
	}
}
