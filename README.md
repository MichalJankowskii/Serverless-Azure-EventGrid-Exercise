# Task
Refactor existing solution to use only [Event Grid](https://azure.microsoft.com/en-us/services/event-grid/). All usage of queues should be removed.

# Solution deployment
## Clone / download git repository
`git clone https://github.com/MichalJankowskii/Serverless-Azure-EventGrid-Exercise.git`
## Provisioning
1. Go to folder `src\Provisioning`
2. Edit `azuredeploy.parameters.json` and update all parameters. Remember that *appName* must be unique for whole Azure cloud
3. Open *PowerShell*
4. Login to Azure:
`Login-AzureRmAccount`
5. Create resource group:
`New-AzureRmResourceGroup -Name EventGridExercise -Location "West Europe"`
6. Provision environment:
`New-AzureRmResourceGroupDeployment -Name EvnetGridDeployment -ResourceGroupName EventGridExercise -TemplateFile azuredeploy.json -TemplateParameterFile azuredeploy.parameters.json`

## Deployment
1. Open solution and update the following files:
- `local.settings.json` (only for local debugging)
- `SendNotificationSMS.cs`
- `SendThankYouEmailMessage.cs`
2. Build solution
3. Publish it to already created *Function App*. Please note that tables and queues will be automatically created before the first usage.
4. Open *RequestStatusCheck* function and copy *function key*.
5. Open *Function App* *Application Settings* and update value of *RequestStatusCheckUrl* key. Please copy there function key.

## Check if everything is working correctly.
You can use the following json in body:
```json
{
    "name": "Name",
    "surname": "Surname",
    "country": "UK",
    "email":"noreply@jankowskimichal.pl",
    "birthyear" : 1980
}
```
# You can start refactoring your solution.

## Hints:
1. Publish events to Event Grid
```csharp
var events = new List<Event>
{
  new Event()
    {
      id = Guid.NewGuid().ToString(),
      eventTime = DateTime.UtcNow,
      eventType = "user",
      subject = "add",
      data = UserFactory.BuildUser()
    }
  };

  var httpClient = new HttpClient();
  httpClient.DefaultRequestHeaders.Add("aeg-sas-key", "ENTER_AEG_SAS_KEY");
  await httpClient.PostAsJsonAsync("EVENT_GRID_ENDPOINT_URL", events);
```
2. [Event Grid trigger for Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-event-grid#packages)
3. [Automate resizing uploaded images using Event Grid](https://docs.microsoft.com/en-us/azure/event-grid/resize-images-on-storage-blob-upload-event)
4. [Azure Event Grid with Custom Events](https://msftplayground.com/2017/08/azure-event-grid-custom-events/)
