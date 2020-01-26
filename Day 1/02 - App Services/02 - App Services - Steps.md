## App Services

Start by creating an app service that can host part of the project we are building. 

Open a command prompt.

### Create a resource group

`az group create -n ndcLondonRG -l northeurope`

### Create web app

`az appservice plan create -g ndcLondonRG -n ndcLondonS1 --sku S1`

`az webapp create -g ndcLondonRG -p ndcLondonS1 -n <unique name>`

App service names are unique across all of Azure, as the name is also the URL. 

## Publish Web Project

You can publish a web project directly from Visual Studio to an Azure App Service. **This is not recommended for production scenarios,** but can be a very useful tool. 

Open the workshop project in VS2019 `File -> Open -> Project/Solution`

Make sure the project builds successfully `Build -> Build Solution` or press F6

Once you know the build is successful, we can publish it.

`Build -> Publish UrlShortener`

Follow the wizard. Enter Microsoft credentials if necessary.

Publish the solution to the web app we created before.

## Create Webjob

A WebJob is a program running in the background of an App Service. It runs in the same context as your web app at no additional cost. 

### Storage Account

All web jobs need a storage account.
Create a new storage account either through the portal or CLI.

Portal: `Storage Accounts -> Add`

CLI: `az storage account create -g ndcLondonRG -n ndclondonwebjobstorage -l northeurope --sku Standard_LRS`

Get the storage account connection string (copy this)

Portal: `Storage Account -> Access Keys` 

CLI: `az storage account show-connection-string -n ndclondonwebjobstorage -g ndcLondonRG`

### WebJob

Create a new project in VS 2019.

`File -> New -> Project`

Select the "Azure Webjob (.NET Framework)" template, give the project a name and local location.

Open the `app.config` and insert the connection string from above for both `AzureWebJobsDashboard` and `AzureWebJobsStorage`

Hit F5 to run WebJob locally.

In the portal go to `storage account -> Storage Explorer` and inspect the queue storage values.

### Publish WebJob

In Visual Studio go to `Build -> Publish` and publish the WebJob to the WebApp you created just before.

In the portal go to `Web App -> WebJobs -> Run the published webjob`