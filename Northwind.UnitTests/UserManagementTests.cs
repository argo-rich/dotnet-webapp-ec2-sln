using System.Text;
using static PasswordHash.PasswordHash;

namespace Northwind.UnitTests;

public class UserManagementTests
{
    [Fact]
    public void Password_Encrypts_and_Compares_25()
    {
        string password = "password10thisissten12345";
        string passwdEncrypted = CreateHash(password);

        Assert.True(ValidatePassword(password, passwdEncrypted));
    }

    [Fact]
    public void Password_Encrypts_and_Compares_8()
    {
        string password = "P@ssw/8c";
        string passwdEncrypted = CreateHash(password);

        Assert.True(ValidatePassword(password, passwdEncrypted));
    }
}