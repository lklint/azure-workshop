## Containers

First step to using containers on Azure is having a Container Registry. 

In the Azure Portal, create new resource. Search for *Container registry* then enter:

* **Registry Name** which must be unique
* **Subscription**
* **Resource Group**: ndcLondonRG
* **Location**: North Europe
* **Admin User**: Disable
* **SKU**: Standard

Click `Create`. 

Explore the newly created ACR. Take note of the value of the **Login server**. You use this value in the following steps while working with your registry with the Azure CLI and Docker.

## Install Docker

To work with Docker images locally, you have to install the Docker client. Do that from [here](https://docs.docker.com/docker-for-windows/install). You will be required to create a Docker profile and sign up. Download the Docker Desktop (914MB 🤯). Install the desktop client. This can take a few minutes. Then you need to restart Windows. Just do it.

Go through the tutorial on the website to get an idea of what Docker desktop does. 

## Using the Container Registry

Open a command shell to login into the newly created ACR using the CLI.

`az acr login --name <acrName>` This is the registry name, not the login server (without .azurecr.io)

You are now logged in to your ACR.

## Get a Docker image

To push an image to an Azure Container registry, you must first have an image. If you don't yet have any local container images, run the following docker pull command from the shell to pull an existing image from Docker Hub. And yes, it is Hello World. 

`docker pull hello-world`

Before you can push an image to your registry, you must tag it with the fully qualified name of your ACR login server.

`docker tag hello-world <acrLoginServer>/hello-world:v1`

Finally, you need to push the Docker image to your ACR.

`docker push <acrLoginServer>/hello-world:v1`

You can now remove the docker image from your local Docker registry, if you want to. It will stay in the ACR though.

`docker rmi <acrLoginServer>/hello-world:v1`

Go back to the portal and inspect the container image in your ACR.

You can now run the container image from ACR locally on your machine.

`docker run <acrLoginServer>/hello-world:v1`

## Build a Container Pipeline

Go to Azure DevOps and create a new pipeline. Use the classic visual editor and choose the UrlShortener project as repository. 

Click `Continue` and search for the **Docker container** template. Apply it. 

Under the **Build an image** step select the subscription and Azure Container Register you created before. 

Select missing values for the **Push an image** step and save the pipeline.