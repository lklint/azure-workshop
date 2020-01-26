## Set defaults in CLI

`az configure --defaults group=ndcLondonRG location=northeurope web=ndcLondonS1`

## Interactive mode
`az interactive`

## ARM templates
Look at several templates from [here](https://github.com/Azure/azure-quickstart-templates)

Understand the structure and format of ARM templates.

Pick one you want to deploy. 
Suggestion: [101-cosmosdb-table](https://github.com/Azure/azure-quickstart-templates/tree/master/101-cosmosdb-table)

In the CLI: 

`az group deployment create -g ndcLondonRG --mode incremental --template-uri https://github.com/Azure/azure-quickstart-templates/blob/master/101-cosmosdb-table/azuredeploy.json --parameters https://github.com/Azure/azure-quickstart-templates/blob/master/101-cosmosdb-table/azuredeploy.parameters.json` 
