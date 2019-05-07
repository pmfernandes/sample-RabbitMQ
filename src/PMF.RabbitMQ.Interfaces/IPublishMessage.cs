namespace PMF.RabbitMQ.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IPublishMessage
	{
		/// <summary>
		/// Executions the specified pool name.
		/// </summary>
		/// <param name="poolName">Name of the pool.</param>
		/// <param name="message">The message.</param>
		void Execution(string poolName, string message);
	}
}
