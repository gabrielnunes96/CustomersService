using CustomerService.Security;
using CustomerService.Services.ClientServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace CustomerService.Services.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly IClientService _clientService;
        public SigningConfigurations _signingConfigurations;
        public TokenConfiguration _tokenConfiguration;
        public LoginService(IClientService clientService, SigningConfigurations signingConfigurations, TokenConfiguration tokenConfiguration)
        {
            _clientService = clientService;
            _signingConfigurations = signingConfigurations;
            _tokenConfiguration = tokenConfiguration;
        }
        public async Task<object> FindByLogin(string agency, string account)
        {
            var baseClient = new Client();

            if (!string.IsNullOrWhiteSpace(agency) && !string.IsNullOrWhiteSpace(account))
            {
                baseClient = await _clientService.ClientLogin(agency, account);
                if (baseClient == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Falha de autenticação"
                    };
                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(agency, account),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, agency),
                            new Claim(JwtRegisteredClaimNames.UniqueName, account)
                        });

                    var createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handler);
                    return SuccessObject(createDate, expirationDate, token, agency, account);
                }
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha de autenticação"
                };
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, String token, string agency, string account)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = "Bearer " + token,
                userAgency = agency,
                userAccount = account,
                message = "Usuário logado com sucesso"
            };
        }
    }
}
