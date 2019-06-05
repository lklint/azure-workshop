## App Services

Make sure you have an app service plan and app service in your Azure subscription. If not, go back to lesson 3 and create them.

## Publish Web Project

You can publish a web project directly from Visual Studio to an Azure App Service. This is not recommended for production scenarios, but can be a very useful tool. 

Open the workshop project in VS2019 `File -> Open -> Project/Solution`

Make sure the project builds successfully `Build -> Build Solution` or press F6

Once you know the build is successful, we can publish it.

`Build -> Publish UrlShortener`

Follow the wizard. Enter Microsoft credentials if necessary.

Publish the solution to the web app we created earlier.