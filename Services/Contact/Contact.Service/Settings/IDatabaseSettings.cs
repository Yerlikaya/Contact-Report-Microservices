namespace Contact.Service.Settings
{
    public interface IDatabaseSettings
    {
        public string ContactCollectionName { get; set; }
        public string CommunicationCollectionName { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
