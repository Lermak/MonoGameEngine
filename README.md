# MonoGameEngine
Building an engine to learn the fundamentals of video game architecture

## Exemplary works
- [Beside Us Alone (GEJAM), 2021](https://fermak.itch.io/beside-us-alone)

## Installation
- clone the repository, and open the project to let OmniSharp do it's thing.
- make sure to [Add nuget.org as a package source [msdn]](https://docs.microsoft.com/en-us/nuget/api/service-index) for your project if you don't have it (instructions follow)
  #### Visual Studio Code
  - open a terminal in the project directory and run
  ```
  dotnet nuget add source https://api.nuget.org/v3/index.json --name nuget.org
  ```
  
  #### Visual Studio (2019)
  - in Visual Studio (2019), this is done through "Tools" > "NuGet Package Manager" > "Package Manager Settings", searching "package sources" in the top left, and adding `nuget.org` with url `https://api.nuget.org/v3/index.json`
![Image showing Package Manager Settings](https://cdn.discordapp.com/attachments/722708774967574618/838102111032049674/unknown.png)