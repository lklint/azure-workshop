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

## Create Continuos Integration Build Pipeline

It's time to create a continous integration pipeline for the project.

In DevOps go to `Pipelines`. Click on `New Pipeline`. 

In this tutorial, we will use the visual editor, which is under `Use the classic editor` link.

Choose _Team Project_, _repository_ and branch for the project. Probably `NDCOslo` if you have used the workshop default. 

Select the ASP.NET Core template. If you can't see it, use the search feature.

Then you arrive at the configuration for the pipeline. There are a number of sections here.

For _Pipeline_ choose 
- Name: Leave as default or choose your own.
- Agent Pool: Hosted VS2017. Click on the `pool information` link to see more about what each pool includes (spolier: it is a lot!).
- Leave parameters as default

And that is really it for this simple project. Because we chose the template for the .NET Core project, the steps are already done for us. A more complex project might have added steps, altered steps or a completely custom approach. 

Note that the 

Click _Save & queue_ to run the build for the first time. 

Once the build has completed you will receive an email with the result: success/failure. 

Go back into the build configuration and go to the _variables_ tab. These are the build-wide variables that you can manage for the whole pipeline. 

Go to the _triggers_ tab. Here you can schedule periodic builds, or link a build. For now check the _Enable Continous Integration_ checkbox. 

And that is it. You have now created a build pipeline with continous integration. Simple. 

## Create Release

The next step is to create a release for the build you have just done. We want the release to replace the manual `Build -> Publish` from Visual Studio with this managed, logged, documented and build dependant release in DevOps.

On the left hand menu click on _Builds_ and on the latest build hover over it. The _elipsis_ menu (3 dots) show. Click on it and select _Release_. This will link the build with a new release. It is a shortcut to define which build to create a release from in DevOps. 

Choose the `Azure App Service deployment` template. 

The _Tasks_ tab will have a red exclamation mark. Hover over it and select the newly created task (Stage 1). You can see now that the red exclamation mark was shown as you haven't yet entered your Azure credentials. 

Choose your Azure Subscription from the dropdown, then click _Authorize_ to link your Azure subscription. Enter your credentials if prompted. Leave App type as `Web app on Windows` and select the web app we created earlier (ndcOsloApp).

Click _Save_ and add a comment if you want to. 

Click on _Crate Release_, then click _Create_.

Go to Releases on the left navigation and confirm the release was successful. Click on the release to see the logs for it. 

## Test CI pipeline and data flow

Go back to Visual Studio, make a change to the code (can just be a comment), and check it in. 