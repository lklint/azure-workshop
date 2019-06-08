## Create Cosmos DB

*_Portal_*: Go to Cosmos DB, then click `Add` to create a CosmosDB account.

Input the details for the instance:

- Resource Group: The one you created earlier for the workshop. `ndcOsloRG`
- Account name: ndcoslocosmosdb
- API: Core (SQL)
- Locatoin: North Europe
- Geo-redundancy: Enable
- Multi-region writes: Enable

Click `Next`

Create a new virtual network: `ndcOsloVNet`.

Allow access from my IP: Allow.

Click `Review + Create`, then click `Create`.

Now you need to create a database in the Cosmos account.

Click on the new Cosmos DB account you created. In the overview, click `Create Items Container`. This will create a data container within your account.

Go to `Data Explorer` and add a new container. 

- Database ID : `ndcoslocosmosdb`. 
- Container ID : `urlcontainer`. 
- Partition key: `/url`. 
- Throughput: 400.

*_CLI_*

Create the Cosmos DB account

`az cosmosdb create -g ndcOsloRG --name ndcoslocosmosdb --kind GlobalDocumentDB --locations "North Europe"=0 "West Europe"=1 --default-consistency-level "Session" --enable-multiple-write-locations true`

Create the database

`az cosmosdb database create -g ndcOsloRG --name ndcoslocosmosdb --db-name urlshortener`

Create the SQL container

`az cosmosdb collection create -g ndcOsloRG --collection-name urlcontainer  --name ndcoslocosmosdb --db-name urlshortener --partition-key-path /urlshortkey --throughput 400`

## Add Data to CosmosDB

Navigate to your Cosmos DB instance. Select `Data Explorer`. 

Expand the tree list for the `urlshortener` database and click on `items`. Click on `new item` and insert the following JSON:

~~~~ 
{
    "id": "2",
    "OriginalUrl" : "http://www.ndcoslo.com"
}
~~~~

This is the very basic data format we will use for the data from the URL shortener. 

## Connect the VS2019 solution