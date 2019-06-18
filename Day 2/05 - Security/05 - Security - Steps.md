## Create Azure Key Vault

We want to remove sensitive information from anything that is checked into source control. This includes connection strings, passwords and certificates. Azure Key Vault is a great place to store these.

*Portal*: Go to the Portal and search for `Key Vault`. Click Add and give it a name, subscription and resource group.

*CLI*: `az keyvault create --name "<YourKeyVaultName>" --resource-group "<YourResourceGroupName>" --location "North Europe"`

Now, add the Cosmos DB production primary key as a secret in the key vault. Use either the Portal or CLI. 

*CLI*: az keyvault secret set --vault-name "ndcOsloKeyVault" --name "CosmosDBConnection" --value "<Cosmos Primary Key>" 

Make a note of this secret for later.

## Register an application with Azure Active Directory

We will register a new application in Azure AD which can then be granted access to the Key Vault we created using an access policy.

From the Azure portal, navigate to Azure Active Directory and open up the App Registrations blade. Click on New application registration and fill in the defaults.

Make sure to select Web app / API as the application type but you can enter whatever URL you like at this point. Take note of the Application ID as this is needed later when configuring ASP.NET. 

We also need to create a Key to use for the Azure AD Applicatoin. Click on Keys and enter in a Description and Expiry setting for a new password. The Value will be displayed when you press Save. Copy the value so that you can use it later.

## Authorize the application to access your KeyVault resource

Now that we’ve got an App registered, we are going to grant it access to the KeyVault we created earlier. Head back to the KeyVault and click on Access Policies. Then click Add new.

Search for the App and set it as the principal for the access policy, and grant it access to the Get, List, and Set secret permissions.

Click OK and save the new access policy configuration.


## Update Web App in Visual Studio

Next is to add the Azure KeyVault configuration provider to our web application and configure it so that it uses the ClientId and ClientSecret of the App that was registered in Azure AD.

Add the two Nuget packages
- `Microsoft.Azure.Services.AppAuthentication`
- `Microsoft.Extensions.Configuration.AzureKeyVault`

Add the following configuration to the startup code in `Program.cs`

~~~~
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
     WebHost.CreateDefaultBuilder(args)
     .ConfigureAppConfiguration((context, config) =>
     {
         var root = config.Build();
         config.AddAzureKeyVault(
            $”https://{root["KeyVault:Vault"]}.vault.azure.net/",
            root[“KeyVault:ClientId”],
            root[“KeyVault:ClientSecret”]);
     })
     .UseStartup<Startup>();
~~~~

Next, add the Vault name, and the ClientId and Client Secret for the App registration to the `appsettings.json` file.

~~~~
"KeyVault": {
	"Vault": "<name of your vault>",
	"ClientId": "<Azure AD app ID>",
	"ClientSecret": "<Secret from Azure AD app>"
}
~~~~

Update the `Startup.cs` file with the Azure Key Vault secret name for Cosmos DB.

~~~~
return new Persistence(
    new Uri(Configuration["CosmosDB:URL"]),
            Configuration["<Key vault secret name>"]);
~~~~

## Securing the Web App with Let's Encrypt

Follow the steps [here](https://github.com/sjkp/letsencrypt-siteextension/wiki/How-to-install) and also read [Scott Hanselman's version](https://www.hanselman.com/blog/SecuringAnAzureAppServiceWebsiteUnderSSLInMinutesWithLetsEncrypt.aspx) of it for reference. 