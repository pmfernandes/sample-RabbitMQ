namespace PMF.RabbitMQ.Interfaces
{
	/// <summary>
	/// 
	/// </summary>
	public interface IReadMessage
	{
		/// <summary>
		/// Executions the specified pool name.
		/// </summary>
		/// <param name="poolName">Name of the pool.</param>
		void Execution(string poolName);
	}
}
