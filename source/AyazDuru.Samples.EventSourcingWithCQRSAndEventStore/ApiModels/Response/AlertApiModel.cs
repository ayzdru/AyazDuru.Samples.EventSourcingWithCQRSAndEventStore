namespace AyazDuru.Samples.EventSourcingWithCQRSAndEventStore.ApiModels.Response
{
    public class AlertApiModel
    {
        public AlertApiModel(string title, string message)
        {
            Title = title;
            Message = message;
        }

        public AlertApiModel(string title, string message, object body) : this(title, message)
        {
            Body = body;         
        }

        public string Title { get; private set; }
        public string Message { get; private set; }
        public object Body { get; set; } = null;
    }
}
