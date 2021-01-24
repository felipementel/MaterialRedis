using Aplicacao.Application.Interfaces.Access;
using Aplicacao.Application.Security;
using Aplicacao.Domain.Model.Access;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Aplicacao.Application.Services.Access
{
    public class LoginApplication : ILoginApplication
    {
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfiguration _tokenConfiguration;

        public LoginApplication(
            SigningConfigurations signingConfigurations,
            TokenConfiguration tokenConfiguration)
        {
            _signingConfigurations = signingConfigurations;
            _tokenConfiguration = tokenConfiguration;
        }

        public Token VerifyAccess(User user)
        {
            bool credentialIsValid = false;

            if (user != null && !string.IsNullOrEmpty(user.Login))
            {
                //TODO: implementar servidor de identidade
                if (user.Login == "usuario" && user.AccessKey == "senha")
                    credentialIsValid = true;
            }

            if (credentialIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new System.Security.Principal.GenericIdentity(user.Login, "Login"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                    });

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = 
                    createDate +
                    TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);

                return SuccessObject(createDate, expirationDate, token);
            }
            else
            {
                return new Token
                {
                    Autenticated = false,
                    Message = "Falha na autenticação"
                };
            }
        }
        private string CreateToken(
            ClaimsIdentity identity, 
            DateTime createDate,
            DateTime expirationDate,
            JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(
                new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
                {
                    Issuer = _tokenConfiguration.Issuer,
                    Audience = _tokenConfiguration.Audience,
                    SigningCredentials = _signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = createDate,
                    Expires = expirationDate
                });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private Token SuccessObject(
            DateTime createDate,
            DateTime expirationDate,
            string token)
        {
            return new Token
            {
                Autenticated = true,
                Created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK"
            };
        }
    }
}