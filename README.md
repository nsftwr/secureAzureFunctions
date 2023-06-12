DISCLAIMER: This is not my code. All credit goes [juunas11](https://github.com/juunas11) & repo [IsolatedFunctionsAuthentication](https://github.com/juunas11/IsolatedFunctionsAuthentication).

`local.settings.json`

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "AuthenticationAuthority": "https://login.microsoftonline.com/your-aad-tenant-id",
    "AuthenticationClientId": "api://your-aad-client-id"
  }
}
```

`Powershell commands to aquire JWT`

```ps
az login --scope api://your-aad-client-id/user_impersonation --tenant your-aad-tenant-id
(az account get-access-token --scope api://your-aad-client-id/user_impersonation --query accessToken).TrimStart('"').TrimEnd('"') | Set-Clipboard -Value {$_.Trim()}
```