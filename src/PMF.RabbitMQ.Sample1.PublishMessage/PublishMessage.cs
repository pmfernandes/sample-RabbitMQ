namespace PMF.RabbitMQ.Sample1.PublishMessage
{
	using System;
	using System.Configuration;
	using System.Text;
	using global::RabbitMQ.Client;
	using Interfaces;

	/// <inheritdoc />
	/// <summary>
	/// </summary>
	/// <seealso cref="T:PMF.RabbitMQ.Interfaces.IPublishMessage" />
	public class PublishMessage : IPublishMessage
	{
		/// <summary>
		/// Executions the specified pool name.
		/// </summary>
		/// <param name="poolName">Name of the pool.</param>
		/// <param name="message">The message.</param>
		public void Execution(string poolName, string message)
		{
			var factory = new ConnectionFactory()
			{
				HostName = ConfigurationManager.AppSettings["RabbitMQ.HostName"],
				Port = Convert.ToInt32(ConfigurationManager.AppSettings["RabbitMQ.Port"]),
				UserName = ConfigurationManager.AppSettings["RabbitMQ.UserName"],
				Password = ConfigurationManager.AppSettings["RabbitMQ.Password"],
				VirtualHost = ConfigurationManager.AppSettings["RabbitMQ.VirtualHost"]
			};

			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: poolName, durable: false, exclusive: false, autoDelete: false, arguments: null);

				var body = Encoding.UTF8.GetBytes(message);

				channel.BasicPublish(exchange: "", routingKey: poolName, basicProperties: null, body: body);

				Console.WriteLine(" [x] Sent {0}", message);
			}
		}
	}
}
