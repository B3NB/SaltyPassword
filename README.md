# SaltyPassword 🤔💭
No one is as bad at remembering things, especially passwords, as humans, well at least this applies to my memory. That's why I made this command line program in C# to help remember long, new passwords.

Secure passwords are really important. So the way we choose them also becomes important. The best way I found to make new passwords, is using the method described [here](https://www.eff.org/dice) by the EFF. Though sometimes causes me to mix up the words in my brain and I then need to check if I can still remember the password before I really use it.

## Stuff you need
- [Git](https://git-scm.com/)
- [.NET Core SDK](https://www.microsoft.com/net/core)

## Setup
Launch a terminal window and clone the repository and go into it with
```
git clone https://github.com/B3NB/SaltyPassword.git
cd SaltyPassword
```
Now restore dependencies and publish the application
```
dotnet restore
dotnet publish -f netcoreapp1.1 -c Release -r win10-x64
```
The program should be located in _..\bin\Release\netcoreapp1.1\win10-x64\_.

## Usage
The program has two modes `SaltyPassword make <FILE>` and `SaltyPassword test <FILE>`. Substitute `<FILE>` with the file where the hash and salt for the password should be stored. After this the program will ask you for your password.