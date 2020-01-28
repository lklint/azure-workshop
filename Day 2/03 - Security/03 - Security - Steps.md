## Create Azure Key Vault

We want to remove sensitive information from anything that is checked into source control. This includes connection strings, passwords and certificates. Azure Key Vault is a great place to store these.

*Portal*: Go to the Portal and search for `Key Vault`. Click Add and give it a name, subscription and resource group. Leave the rest as default for now.

*CLI*: `az keyvault create --name "<YourKeyVaultName>" --resource-group "<YourResourceGroupName>" --location "North Europe"`

Now, add the Cosmos DB production primary key as a secret in the key vault. Use either the Portal or CLI. 

*CLI*: az keyvault secret set --vault-name "ndcLondonKeyVault" --name "CosmosDBConnection" --value "<Cosmos Primary Key>" 

Make a note of this secret for later.

## Connect To Key Vault

Create a service principal for authentication to your Key Vault. 

Use the Azure CLI: `az ad sp create-for-rbac -n "http://ndcPrincipal" --sdk-auth`. This will return a bunch of key/value pairs. Make a note of these. 

Run the following command to let the service principal access your key vault: `az keyvault set-policy -n <YourKeyVaultName> --spn <clientId-of-your-service-principal> --secret-permissions delete get list set --key-permissions create decrypt delete encrypt get list unwrapKey wrapKey` 

Add environment variables to store the values of *clientId*, *clientSecret*, and *tenantId*. In the command prompt:

``setx AZURE_CLIENT_ID <clientId-of-your-service-principal>
setx AZURE_CLIENT_SECRET <clientSecret-of-your-service-principal>
setx AZURE_TENANT_ID <tenantId-of-your-service-principal>``

## Create Azure App Configuration

Azure App Configuration provides a service to centrally manage application settings and feature flags. Use it here to store the Key Vault secret reference.

In the Azure Portal, create a new resource and search for `App Configuration`. Create a new instance and set the following values.

* Resource Name: globally unique name.
* Subscription: your subscription.
* Resource Group: NDCLondonRG
* Location: North Europe

Once create make a note of the primary read-only key in `Settings > Access Keys`. You'll use this connection string to configure your application to communicate with the App Configuration store that you created.

Select **Configuration Explorer**. Select **+ Create** > **Key vault reference**, and then specify the following values:

- **Key**: Select **NDCApp:Settings:KeyVaultMessage**.
- **Label**: Leave this value blank.
- **Subscription**, **Resource group**, and **Key vault**: Enter the values corresponding to those in the key vault you created in the previous section.
- **Secret**: Select the secret named **CosmosDBConnection** that you created in the previous section.

Click **Apply**.


## Update Web App in Visual Studio

Next is to add the Azure KeyVault configuration provider to our web application and configure it so that it uses the ClientId and ClientSecret of the App that was registered in Azure AD.

Add the two Nuget packages
- `Microsoft.Extensions.Configuration.AzureAppConfiguration` (this is a preview SDK, so check the "include prerelease" box)
- `Microsoft.Azure.KeyVault`
- `Azure.Identity`

Add the following configuration to the startup code in `Program.cs` (replace the current function).

~~~~c#
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            var settings = config.Build();

            config.AddAzureAppConfiguration(options =>
            {
                options.Connect(settings["ConnectionStrings:AppConfig"])
                        .ConfigureKeyVault(kv =>
                        {
                            kv.SetCredential(new DefaultAzureCredential());
                        });
            });
        })
        .UseStartup<Startup>();
~~~~

Update the `Startup.cs` file with the Azure Key Vault secret name for Cosmos DB.

~~~~c#
return new Persistence(
    new Uri(Configuration["CosmosDB:URL"]),
            Configuration["NDCApp:Settings:KeyVaultMessage"]);
~~~~

## Securing the Web App with Let's Encrypt

Follow the steps [here](https://github.com/sjkp/letsencrypt-siteextension/wiki/How-to-install) and also read [Scott Hanselman's version](https://www.hanselman.com/blog/SecuringAnAzureAppServiceWebsiteUnderSSLInMinutesWithLetsEncrypt.aspx) of it for reference. 