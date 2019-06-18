## Create a New Logic App

Create a Logic App that creates a short url every time a specific Tweet hashtag is mentioned. Then notify an email recipient.

Go to the portal and the search for `Logic Apps`. Click *Add* to create a new Logic App.

Give it a name, place it in the `ndcOsloRG` resource group. Location is set as *North Europe*. Click *Save*.

The Logic App is created and the Logic app designer opens. Explore the templates shown to get a sense for what kind of scenarios Logic Apps are used for.

Click on *Blank Template*.

Search for `Twitter` for the first step and select the *Twitter trigger*. Log in with any Twitter account credentials. If you don't have a Twitter account, you can create one for free.

Put in any search term to trigger the step, such as `#serverless`. 

Click *+ New Step* and search for `Compose`. Choose the *Compose* action under *Data Operations*. In the inputs text field enter:

````

{
    "UrlId": "⁠⁠",
    "OriginalUrl" : "https://twitter.com/⁠",
    "CreatedDateTime": "⁠⁠",
    "Id": ""
}

````

Then place the cursor in the empty `""` for UrlId, click on *Add Dynamic Content*, then choose *Originial Tweet Id*. 

Similarly for *Orignial URL* append the dyanmic field *Origninal tweet user name*, and for *CreatedDateTIme* use dynamic field *Created at*. 

For *Id* we want to great a GUID. In the *dynamic content* pop up, click on the *Expression* and add `guid()`. 

Click *+ New Step* and search for `Cosmos`. Choose the *Create or Update Document* action. Give the step a name, such as `Add Short Url to Cosmos DB`. The Cosmos DB connection should be automatically selected and configured. Select the Cosmos DB connection. Click *Create*. 

Select the *Database ID* and *Collection Id*. 

Then for the document add the `Outputs` from the Compose step. 

You need to add a parameter for partition key to make Cosmos DB happy. Click on *Add New Parameter* and choose *Partition Key Value*. The value needs to be in quotation marks to make it valid JSON. 

Click *Save* and then *Run*.