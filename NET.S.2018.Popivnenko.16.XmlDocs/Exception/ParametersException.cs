namespace NET.S._2018.Popivnenko._16.XmlDocs.Exception
{
    /// <summary>
    /// Implements Exception class.
    /// Used in context of checking for Parameters of uri being incorrect.
    /// </summary>
    public class ParametersException : System.Exception
    {
        /// <summary>
        /// Constructor with message.
        /// </summary>
        /// <param name="message">Message to be displayed.</param>
        public ParametersException(string message) : base(message)
        {
        }
    }
}
