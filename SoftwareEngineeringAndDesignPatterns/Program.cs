// initialize a base-level user
Client user = new User
{
    Username = "john_wiliam",
    Email = "john@example.com"
};

// get base values
Console.WriteLine(user.GetDescription());

// then add decorators
// Creating a badge decorator for reputation
user = new ReputationBadgeDecorator(user);
Console.WriteLine("After the user get Reputation Badge decorating");

Console.WriteLine();
// Displaying user information with badges
Console.WriteLine(user.GetDescription());


// ABSTRACT COMPONENT
public abstract class Client
{
    public string Username { get; set; }
    public string Email { get; set; }

    protected string Description = "No Description";

    public virtual string GetBadges()
    {
        return Description;
    }

    public virtual string GetDescription()
    {
        return Description;
    }
}

// === CONCRETE COMPONENTS === 
public class User : Client
{
    public User()
    {
        Description = "Base-level User";
    }
}

// === ABSTRACT DECORATOR ===
public abstract class BadgeDecorator : Client
{
    public Client Client { get; set; }

    public abstract override string GetBadges();

    public override string GetDescription()
    {
        // Append badges to the description
        return $"{Client.GetDescription()} {GetBadges()}";
    }

    public BadgeDecorator(Client client)
    {
        Client = client;
    }
}

// Concrete Decorator

public class ReputationBadgeDecorator : BadgeDecorator
{
    public ReputationBadgeDecorator(Client client) : base(client)
    {
    }

    public override string GetBadges()
    {
        return "Golden Badge";
    }
}