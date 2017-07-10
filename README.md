# SaltyPassword 🤔💭
Nothing is as bad at remembering things, especially passwords, as humans, well at least this applies to my memory. That's why I made this command line program in C# to help remember long, new passwords.

Secure passwords are really important, so the way we choose them becomes equally as important. The best way I found to make new passwords, is using the method described [here](https://www.eff.org/dice) by the EFF. Though sometimes this results in me mixing up the words in my brain and I sometimes need check if I remember everything correctly.

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