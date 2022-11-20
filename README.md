# quotely-csharp
---

## Prerequisites

1. [Install .NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

2. (Optional) Install `dotnet` command with brew: `brew install dotnet` (Automatically resolves PATH)

## Steps To Run Project

1. [Clone Github Quotely Project](https://github.com/CliffordClintonInterview/quotely-csharp)

`git clone https://github.com/CliffordClintonInterview/quotely-csharp.git`

2. Navigate to the `quotely-csharp/Quotely` directory.

`cd quotely-csharp/Quotely`

3. Run the `dotnet build` command

`dotnet build Quotely.sln`

4. Run the program

`./Quotely.Code/bin/Debug/net6.0/Quotely.Code [Russian|English]`

5. Run the Unit Test

`dotnet test` Or `dotnet test -v d`
