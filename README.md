DISCLAIMER: This is not my code. All credit goes [juunas11](https://github.com/juunas11) & repo [IsolatedFunctionsAuthentication](https://github.com/juunas11/IsolatedFunctionsAuthentication).

Instructions:

1. Log into Azure and go to Active Directory

![Alt text](attachments/image-1.png)

2. Navigate to App Registrations and create a new app registration

![Alt text](attachments/image-2.png)

3. Once created, enter the app registration and navigate to 'Expose an API'. Within there, add a new scope called 'user_impersonation'

![Alt text](attachments/image-3.png)

4. Navigate to 'App Roles' and create the necessary app roles for users

![Alt text](attachments/image-4.png)

5. Once all roles have been added, navigate back to Active Directory and go to 'Enterprise Applications'. There will be an application with the same name you entered for the app registration. Under 'Users and groups' add the required users and their roles accordingly to the access they will need

![Alt text](attachments/image-5.png)

6. Clone the repo

7. Add the `local.settings.json` filling in your Tenant Id and Application Client Id details.

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

8. Once all of the things above have been completed, you can launch the function application locally.

![Alt text](attachments/image-6.png)

9. Open Powershell and enter these commands to acquire a JWT token to access your function app. This script will get the token and automatically copy it in your clipboard.

```ps
az login --scope api://your-aad-client-id/user_impersonation --tenant your-aad-tenant-id
(az account get-access-token --scope api://your-aad-client-id/user_impersonation --query accessToken).TrimStart('"').TrimEnd('"') | Set-Clipboard -Value {$_.Trim()}
```

10. Using [jwt.io](https://jwt.io/) we can decipher the token and see if all the required details have been acquired.

![Alt text](attachments/image-7.png)

11. Using Postman you can test the authorization locally. If you have all the required roles, the API will return a 200, if not, then 403. 

![Alt text](attachments/image-8.png)

![Alt text](attachments/image-9.png)

If you're using an expired token you will receive a 401.

![Alt text](attachments/image-10.png)

Now your Azure Functions are secured with Azure Active Directory Application roles.
