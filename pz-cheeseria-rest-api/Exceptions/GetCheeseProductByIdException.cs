namespace PZCheeseriaRestApi.Controllers
{
    /// <summary>
    /// Exception thrown when there is an error retrieving a cheese product by its ID.
    /// </summary>
    [Serializable]
    internal class GetCheeseProductByIdException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetCheeseProductByIdException"/> class.
        /// </summary>
        public GetCheeseProductByIdException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCheeseProductByIdException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public GetCheeseProductByIdException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCheeseProductByIdException"/> class
        /// with a specified error message and a reference to the inner exception
        /// that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public GetCheeseProductByIdException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}
