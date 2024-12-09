using System.Text;
using static PasswordHash.PasswordHash;

namespace Northwind.UnitTests;

public class UserManagementTests
{
    [Fact]
    public void Password_Encrypts_and_Compares()
    {
        string password = "password10thisissten12345";
        string passwdEncrypted = CreateHash(password);

        Assert.True(ValidatePassword(password, passwdEncrypted));
    }
}