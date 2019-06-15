## Create a Function App

*_Portal_*

Click `Create a Resource`. Search for "Function App", then click `Create`. 

Enter the following details
- App Name: UrlExpiry
- Subscription: You current one
- Resource group: Existing `ndcOsloRG`
- OS: Windows
- Hosting Plan: Consumption
- Location: North Europe
- Runtime stack: .NET
- Storage: Create new

Click `Create`

*_CLI_*

Create a storage account which functions use as a general-purpose account to maintain state and other information about your functions.

`az storage account create --name urlexpiry32 --location northeurope -g ndcOsloRG --sku Standard_LRS` 

Create the Function App.

`az functionapp create -g ndcOsloRG --consumption-plan-location northeurope --name UrlExpiry --storage-account  urlexpiry32 --runtime dotnet`

## Create Function Timer 

In Visual Studio 2019 go to `File -> New Project` and seach for `Function`. Choose the *Azure Functions* project and click *Next*. 

Give the project a name such as `UrlExpiryTimer`. Choose a location, create a new solution and leave the solution name as suggested. Click *Create*.

Choose the *Timer Trigger*. Select storage emulator as storage account. The schedule is a Cron job format. Set it as `0 */5 * * * *`
