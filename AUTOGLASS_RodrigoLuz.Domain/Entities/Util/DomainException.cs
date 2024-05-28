using System;
namespace AUTOGLASS_RodrigoLuz.Domain.Entities.Util
{
    public class DomainException : Exception
    {
        /// <summary>
        /// Instância única
        /// </summary>
        public DomainException()
        {
        }

        /// <summary>
        /// Msg personalizada
        /// </summary>
        /// <param name="message"></param>
        public DomainException(string message) : base(message) { }

        /// <summary>
        /// retorna msg e exception 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}

