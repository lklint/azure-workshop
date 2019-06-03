Create a resource group

- az group create -n ndcOsloRG -l northeurope

Create web app

- az appservice plan create -g ndcOsloRG -n ndcOsloS1 --sku S1
- az webapp create -g ndcOsloRG -p ndcOsloS1 -n ndcOsloApp

Show in portal

Set defaults in CLI
- az configure --defaults group=ndcOsloRG location=northeurope web=ndcOsloS1

Show interactive mode
- az interactive
https://docs.microsoft.com/en-us/cli/azure/interactive-azure-cli?view=azure-cli-latest

Introduce ARM templates
https://github.com/Azure/azure-quickstart-templates 
