## Install VS2019
[VS 2019 Community Web installer](https://visualstudio.microsoft.com/vs/)

## Get Workshop Project
Go here: [URL shortener](https://github.com/fxmauricard/aspnetcore-url-shortener )

Download as .zip. __Do Not Fork/Clone__

Open it in VS2019. Unzip the downloaded folder, then open the `UrlShortener.sln` file.

Press F6 to build the solution. Then go to `Tools -> Package Manager Console` and enter `dotnet ef database update`.

Press F5 to run the solution. Enter your favourite URL to shorten. Revel in the glory. 

Note: You have to include `http://` or `https://` in your link to shorten.

## Install Azure CLI
[Find your right version of the CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest )

### Test out the CLI :sparkles:
Open a command prompt 

`az login`

Choose subscription

`az account list`

Copy the ID for your preferred Azure subscription

`az account set --subscription <ID>`


## Install Cosmos DB emulator
[This is the place to click](https://aka.ms/cosmosdb-emulator/)

