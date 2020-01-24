## Create a Function App

*_Portal_*

Click `Create a Resource`. Search for "Function App", then click `Create`. 

Enter the following details
- App Name: UrlExpiry (which again has to be unique across Azure)
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

Choose the *Timer Trigger*. Select storage emulator as storage account. The schedule is a Cron job format. Set it as `0 */1 * * * *`. This means the trigger will fire every minute. To learn more about cron jobs, go [here](https://www.ostechnix.com/a-beginners-guide-to-cron-jobs/).

Right click the Cosmos DB emulator icon in the Windows tray and *Open Data Explorer*. Copy the *Primary Connection String*.

Open `local.settings.json` in Visual Studio and update it as per the following:

~~~~
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "cosmosdbconnection": "<CosmosDB Emulator connection string>"
  }
}
~~~~

This will work for local development on your machine. To set the connection in Azure, first go to the portal and copy your CosmosDB connection string. 

Then find your newly created Function App. Go to *Function App Settings* -> *Manage Application Settings* and add a new application setting mirroring the above for production. Save the new application settings.

Back in Visual Studio, Right click the function project and select *Manage Nuget Packages*. Find and install `Microsoft.Azure.WebJobs.Extensions.CosmosDB`.

Add a new class (`Shift+Alt+C`) to the project called `ShortUrl`. This is the same model as in the URL Shortener project. Add the following properties to the class.

````
public class ShortUrl
{
    public string id { get; set; }

    public int UrlId { get; set; }

    public string OriginalUrl { get; set; }

    public DateTime CreatedDateTime { get; set; }
}
````

Update the arguments for the `Run` function in the Azure Function file to be 

~~~~
[TimerTrigger("0 */1 * * * *")]TimerInfo myTimer,
[CosmosDB(databaseName: "URLs", collectionName: "ShortUrls", ConnectionStringSetting = "cosmosdbconnection")] DocumentClient client, 
ILogger log
~~~~

The Azure function now have access to the CosmosDB we create earlier. We can now query the Cosmos DB to find expired URLs.

In the body of the `Run` function add this snippet:

````
var urlcollection = client.CreateDocumentQuery<ShortUrl>(UriFactory.CreateDocumentCollectionUri("URLs", "ShortUrls"));

foreach (var url in urlcollection){
    if (DateTime.Now - url.CreatedDateTime > new TimeSpan(0, 5, 0)) {
        client.DeleteDocumentAsync(UriFactory.CreateDocumentUri("URLs", "ShortUrls", url.id));
    }
}
````

This will retrieve all the ShortUrls in the Cosmos DB and if any are more than 5 minutes old, delete them! We don't want any old URLs here.

## Publish Azure Function

We want the new function to live in Azure, so do the naughty for now and publish it from Visual Studio. **Of course this would ideally be set up in Azure DevOps**. `Build -> Publish` and push the Function to the Azure Function App you created above. 

Last step is to allow the Function access to CosmosDB on the Azure Virtual Network. In the portal go to your Cosmos DB, then select `Firewall and Virtual Networks`. Here allow access from all networks and click save to update the firewall configuration. This would normally be manages by either placing the Function on the same network, or through role based policies. 

Go to your Azure Function in the Portal. Under *Functions*, click on the name of the function we just created. Click *Run*.

## Bonus points

If you have time left, move the build and release of the Azure Function to Azure DevOps. 