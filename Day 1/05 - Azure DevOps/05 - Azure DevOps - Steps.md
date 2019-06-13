## Create Azure DevOps Account and Project

Go to the [DevOps Main Page](https://dev.azure.com) and click `sign in`.

Enter the credentials used for your Azure account.

Click on `Create Project` and give it a name like `NDCOslo`

Familiarize yourself with the tools, sections, options. Note the `Repos -> Files` section and how you can connect to the code repository

## Push Code to Repository

Open VS 2019 and the project solution (URL Shortener)

In the bottom right corner click on `Add to Source Control -> Git`. This will add git specific files and folders to your existing directory.

Open Team Explorer: `View -> Team Explorer` and click `Publish Git Repo` under Push to Azure DevOps Services.

Find the Organization and DevOps Project you have created above. Click `Publish repository`.

Go to [DevOps Main Page](https://dev.azure.com) -> Repos and confirm code has been pushed. 

## Create Build Pipeline

It's time to create a continous integration pipeline for the project.

In DevOps go to `Pipelines`. Click on `New Pipeline`. 

In this tutorial, we will use the visual editor, which is under `Use the classic editor` link.

Choose *Team Project*, *repository* and branch for the project. Probably `NDCOslo` if you have used the workshop default. 

Select the ASP.NET Core template. If you can't see it, use the search feature.

Then you arrive at the configuration for the pipeline. There are a number of sections here.

For *Pipeline* choose 
- Name: Leave as default or choose your own.
- Agent Pool: Hosted VS2017. Click on the `pool information` link to see more about what each pool includes.

Continous Integration

## Create Release

Push code to Azure Web App
