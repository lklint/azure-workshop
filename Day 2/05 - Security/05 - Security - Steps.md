## Create Azure Key Vault

We want to remove sensitive information from anything that is checked into source control. This includes connection strings, passwords and certificates. Azure Key Vault is a great place to store these.

*Portal*: Go to the Portal and search for `Key Vault`. Click Add and give it a name, subscription and resource group.

*CLI*: `az keyvault create --name "<YourKeyVaultName>" --resource-group "<YourResourceGroupName>" --location "North Europe"`




## Securing the Web App with Let's Encrypt

Follow the steps [here](https://github.com/sjkp/letsencrypt-siteextension/wiki/How-to-install) and also read [Scott Hanselman's version](https://www.hanselman.com/blog/SecuringAnAzureAppServiceWebsiteUnderSSLInMinutesWithLetsEncrypt.aspx) of it for reference. 