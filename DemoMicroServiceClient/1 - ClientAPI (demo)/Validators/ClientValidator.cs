using System.Text.RegularExpressions;

namespace ClientAPI.Validators
{
    public class ClientValidator
    {
        private const string erroAgencia = "AGÊNCIA DEVE CONTER APENAS NÚMEROS";
        private const string erroConta = "CONTA DEVE CONTER APENAS NÚMEROS";
        private static readonly Regex regex = new Regex(@"^\d+$");
        public static string ValidaAgencia(string agencia)
        {
            if (!regex.IsMatch(agencia))
                return erroAgencia;

            return string.Empty;
        }

        public static string ValidaConta(string conta)
        {
            if (!regex.IsMatch(conta))
                return erroConta;

            return string.Empty;
        }
    }
}
