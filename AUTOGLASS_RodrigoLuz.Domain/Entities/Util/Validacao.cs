using System;
namespace AUTOGLASS_RodrigoLuz.Domain.Entities.Util
{
	public class Validacao
	{
		public Validacao()
		{
		}

        public static void VerificaCampoVazio(string stringValue, string message)
        {
            if (stringValue == null || stringValue.Trim().Length == 0)
            {
                throw new DomainException(message);
            }
        }


        public static void VerificaDataFabricacao(DateTime dataFabricacao, DateTime dataValidade, string message)
        {
            if(dataFabricacao >= dataValidade)
                throw new DomainException(message);
        }
        
    }
}

