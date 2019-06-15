Open a command prompt

## Create a resource group

`az group create -n ndcOsloRG -l northeurope`

## Create web app

`az appservice plan create -g ndcOsloRG -n ndcOsloS1 --sku S1`

`az webapp create -g ndcOsloRG -p ndcOsloS1 -n <unique name>`

App service names are unique across all of Azure, as the name is also the URL. 

## Set defaults in CLI

`az configure --defaults group=ndcOsloRG location=northeurope web=ndcOsloS1`

## Interactive mode
`az interactive`

## ARM templates
Look at several templates from [here](https://github.com/Azure/azure-quickstart-templates)

Understand the structure and format of ARM templates.

Pick one you want to deploy. 
Suggestion: [101-cosmosdb-table](https://github.com/Azure/azure-quickstart-templates/tree/master/101-cosmosdb-table)

In the CLI: 

`az group deployment create -g ndcOsloRG --mode incremental --template-uri https://github.com/Azure/azure-quickstart-templates/blob/master/101-cosmosdb-table/azuredeploy.json --parameters https://github.com/Azure/azure-quickstart-templates/blob/master/101-cosmosdb-table/azuredeploy.parameters.json` 
