namespace ClientAPI.Validators
{
    public interface IClientValidator
    {
        string ValidaAgencia(string agencia);
        string ValidaConta(string conta);
        string ValidaTipoConta(string tipoConta);


    }
}
