public abstract class Client
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public bool AccessDisabled { get; set; }
    protected IAccessHandler _accessHandler;
    public virtual bool HandleAccess()
    {
        return _accessHandler.GetAccess(accessDisabled: AccessDisabled);
    }
}

public class User : Client
{
    public User()
    {
        _accessHandler = new HasReputation();
    }
    public int Reputation { get; set; }
    public override bool HandleAccess()
    {
        return _accessHandler.GetAccess(reputation: Reputation);
    }
}

public class Manager : Client
{
    public Manager()
    {
        _accessHandler = new HasAccessAutomatic();
    }
}

public class Admin : Client
{
    public Admin()
    {
        _accessHandler = new HasAccessAutomatic();
    }
}

// ACCESS

public interface IAccessHandler
{
    public bool GetAccess(int? reputation = 0, bool accessDisabled = false);
}

public class HasReputation : IAccessHandler
{
    public bool GetAccess(int? repuation = 0, bool accessDisabled = false)
    {
        return repuation > 20;
    }

}

public class HasAccessAutomatic : IAccessHandler
{
    public bool GetAccess(int? repuation = 0, bool accessDisabled = false)
    {
        return !accessDisabled;
    }

}