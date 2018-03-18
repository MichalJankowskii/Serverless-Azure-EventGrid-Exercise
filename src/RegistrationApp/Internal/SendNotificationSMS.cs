namespace RegistrationApp.Internal
{
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Host;
    using Models;
    using Twilio;

    public static class SendNotificationSMS
    {
        [FunctionName("SendNotificationSMS")]
        public static void Run(
            [QueueTrigger("tosendnotification", Connection = "registrationstorage_STORAGE")] Customer customer,
            [TwilioSms(
                To = "ENTER_YOUR_PHONE_NUMBER",
                From = "ENTER_FROM_PHONE_NUMBER - PROVIDED BY TWILIO",
                Body = "New customer {Name} {Surname}!")]
            out SMSMessage message,
            TraceWriter log)
        {
            log.Info($"SendNotificationSMS function processed: {customer.Name} {customer.Surname}");
            message = new SMSMessage();
        }
    }
}
