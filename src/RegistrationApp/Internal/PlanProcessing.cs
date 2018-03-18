namespace RegistrationApp.Internal
{
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Host;
    using Microsoft.WindowsAzure.Storage.Queue;

    public static class PlanProcessing
    {
        [FunctionName("PlanProcessing")]
        public static async Task Run(
            [QueueTrigger("requestaccepted", Connection = "registrationstorage_STORAGE")]string requestAcceptedItem,
            [Queue("tosendemail", Connection = "registrationstorage_STORAGE")] CloudQueue toSendEmailQueue,
            [Queue("tosendnotification", Connection = "registrationstorage_STORAGE")] CloudQueue toSendNotificationQueue,
            [Queue("tostorecustomer", Connection = "registrationstorage_STORAGE")] CloudQueue toStoreCustomerQueue,
            TraceWriter log)
        {
            log.Info($"PlanProcessing function processed: {requestAcceptedItem}");

            var cloudQueueMessage = new CloudQueueMessage(requestAcceptedItem);
            await toSendEmailQueue.AddMessageAsync(cloudQueueMessage);
            await toSendNotificationQueue.AddMessageAsync(cloudQueueMessage);
            await toStoreCustomerQueue.AddMessageAsync(cloudQueueMessage);
        }
    }
}
