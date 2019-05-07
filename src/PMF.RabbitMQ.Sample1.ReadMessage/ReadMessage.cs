namespace PMF.RabbitMQ.Sample1.ReadMessage
{
	using System;
	using System.Configuration;
	using System.Text;
	using System.Threading;
	using global::RabbitMQ.Client;
	using global::RabbitMQ.Client.Events;
	using Interfaces;

	/// <inheritdoc />
	/// <summary>
	/// </summary>
	/// <seealso cref="T:PMF.RabbitMQ.Interfaces.IReadMessage" />
	public class ReadMessage : IReadMessage
	{
		/// <summary>
		/// Executions the specified pool name.
		/// </summary>
		/// <param name="poolName">Name of the pool.</param>
		public void Execution(string poolName)
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

				var consumer = new EventingBasicConsumer(channel);
				consumer.Received += (model, ea) =>
				{
					var body = ea.Body;
					var message = Encoding.UTF8.GetString(body);
					Console.WriteLine(" [x] Received -> {0}", message);

					var dots = message.Split('.').Length - 1;
					Thread.Sleep(dots * 1000);

					Console.WriteLine(" [x] Done");
				};

				channel.BasicConsume(queue: poolName, autoAck: true, consumer: consumer);

				Console.WriteLine(" Press [enter] to exit.");
				Console.ReadLine();
			}
		}
	}
}
