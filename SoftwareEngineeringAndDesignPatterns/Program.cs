
//Lab: Factory Design Pattern:  a security system for managing different users

using SoftwareEngineeringAndDesignPatterns;
using System.Text.Json;

UserFactory twoFactorRequiredFactory = new TwoFactorRequiredUserFactory();
UserFactory twoFactorNotRequiredFactory = new TwoFactorNotRequiredUserFactory();

try
{
    List<string> tagsRequired = new List<string> { "TwoFactorAuthentication", "IsAdmin" };
    User userRequiredTag = twoFactorRequiredFactory.CreateUser("Jason William", "password123", true, true, tagsRequired);
    Console.WriteLine("======");
    var tagsNotRequired = new List<string> {  };
    User userNotRequiredTag = twoFactorNotRequiredFactory.CreateUser("Jason William", "password456", false, false, tagsNotRequired);

}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}

Console.WriteLine();
//Bonus:set up a system that can read a JSON file and extract the aforementioned properties!
try
{
    string jsonFilePath = "users.json";
    string jsonData = File.ReadAllText(jsonFilePath);

    // Deserialize JSON data into an object
    List<UserData> usersData = JsonSerializer.Deserialize<List<UserData>>(jsonData);

    if (usersData != null)
    {
        foreach (UserData userData in usersData)
        {
            // Create users from the JSON data
            UserFactory userFactory;

            if (userData.TwoFactorAuthentication)
            {
                userFactory = new TwoFactorRequiredUserFactory();
            }
            else
            {
                userFactory = new TwoFactorNotRequiredUserFactory();
            }

            User user = userFactory.CreateUser(userData.Username, userData.Password, userData.TwoFactorAuthentication, userData.IsAdmin, userData.Tags);

            Console.WriteLine($"User created successfully: {user.Username}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}


// "Factory class"
public abstract class UserFactory
{
    public User CreateUser(string username, string password, bool twoFactorAuthentication, bool isAdmin, List<string> tags)
    {
        User user;

        if (twoFactorAuthentication)
        {
            if (isAdmin)
            {
                user = CreateAdministrator(username, password, tags);
            }
            else
            {
                user = CreateUser(username, password, tags);
            }
        }
        else
        {
            if (isAdmin)
            {
                user = CreateAdministratorWithoutTags(username, password);
            }
            else
            {
                user = CreateUserWithoutTags(username, password);
            }
        }

        return user;
    }

    protected abstract User CreateUser(string username, string password, List<string> tags);
    protected abstract Administrator CreateAdministrator(string username, string password, List<string> tags);
    protected abstract User CreateUserWithoutTags(string username, string password);
    protected abstract Administrator CreateAdministratorWithoutTags(string username, string password);
}

public class TwoFactorRequiredUserFactory : UserFactory
{
    protected override User CreateUser(string username, string password, List<string> tags)
    {
        User user = new User { Username = username, Password = password, Tags = tags };
        Console.WriteLine("TagsRequired User created successfully: " + user.Username);
        return user;
        
    }

    protected override Administrator CreateAdministrator(string username, string password, List<string> tags)
    {
        Administrator admin = new Administrator { Username = username, Password = password, Tags = tags };
        Console.WriteLine("TagsRequired AdminUser created successfully: " + admin.Username);
        return admin;
        
    }
    protected override User CreateUserWithoutTags(string username, string password)
    {
        User user = new User { Username = username, Password = password, Tags = new List<string>() };
        return user;
    }

    protected override Administrator CreateAdministratorWithoutTags(string username, string password)
    {
        Administrator admin = new Administrator { Username = username, Password = password, Tags = new List<string>() };
        return admin;
    }
}

public class TwoFactorNotRequiredUserFactory : UserFactory
{
    protected override User CreateUser(string username, string password, List<string> tags)
    {
        User user = new User { Username = username, Password = password, Tags = tags };
        return user;
    }

    protected override Administrator CreateAdministrator(string username, string password, List<string> tags)
    {
        Administrator admin = new Administrator { Username = username, Password = password, Tags = tags };
        return admin;
    }
    protected override User CreateUserWithoutTags(string username, string password)
    {
        User user = new User { Username = username, Password = password, Tags = new List<string>() };
        Console.WriteLine("TagsNotRequired User created successfully: " + user.Username);
        return user;
        
    }

    protected override Administrator CreateAdministratorWithoutTags(string username, string password)
    {
        Administrator admin = new Administrator { Username = username, Password = password, Tags = new List<string>() };
         Console.WriteLine("TagsNotRequired AdminUser created successfully: " + admin.Username);
        return admin;
       
    }
}

// Abstract + Concrete user classes
public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<string> Tags { get; set; } 

    public virtual void PasswordHash() { }
}

public class Administrator : User
{
    
}







