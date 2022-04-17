namespace Football.Core.Contracts
{

    public interface IManagerService
    {
        public bool IsManager(string userId);

        public Guid IdByUser(string userId);

        //public Guid IdByUser(string managerId);
    }
}
