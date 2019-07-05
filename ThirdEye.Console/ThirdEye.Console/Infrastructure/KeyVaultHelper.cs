﻿using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;

namespace ThirdEye.Console.Infrastructure
{
    class KeyVaultHelper : IKeyVaultHelper
    {
        private IOptions<AppSettings> _appSettings;
        private static string _clientId;
        private static string _clientSecret;
        private static KeyVaultClient _keyVaultClient;

        public KeyVaultHelper(IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings;
            _clientId = _appSettings.Value.Secrets.ClientId;
            _clientSecret = _appSettings.Value.Secrets.ClientSecret;
            _keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
        }

        async Task<string> IKeyVaultHelper.GetSecretAsync(string key)
        {
            var secret = await _keyVaultClient.GetSecretAsync("https://thirdeyesecrets.vault.azure.net/secrets/Test");
            return secret.Value;
        }

        private async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(_clientId, _clientSecret);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }
    }

    internal interface IKeyVaultHelper
    {
        Task<string> GetSecretAsync(string key);
    }
}
