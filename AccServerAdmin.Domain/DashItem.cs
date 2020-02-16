namespace AccServerAdmin.Domain
{
    public class DashItem
    {
        public Server Server { get; set; }

        public ServerProcessInfo ProcessInfo { get; set; }

        public bool HasImportableResults { get; set; }
    }
}
