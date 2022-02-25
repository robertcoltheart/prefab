# Prefab

[![License](https://img.shields.io/github/license/robertcoltheart/Prefab?style=for-the-badge)](https://github.com/robertcoltheart/Prefab/blob/master/LICENSE)

A `dotnet` template for creating a bare-bones C# solution.

## Prerequisites
- Install the [.NET Core 2.0 SDK](https://www.microsoft.com/net/core) or later versions.

## Installation
- Download the latest package from [releases](https://github.com/robertcoltheart/Prefab/releases).
- Install using the following command: `dotnet new -i <path to package>`

## Usage
- Create a new directory with the name of the solution, eg. `C:\MyProject`
- Install the solution using the following: `dotnet new prefab`

You can customize the output by using the following parameters:

Parameter | Default value | Comments
--- | --- | --- |
-n&#124;--name | [current dir] | Override the solution and project name to something other than the folder name.
-o&#124;--output | [current dir] | Write the project to the specified directory.
--owner | robertcoltheart | This is typically the owning organization or username in GitHub.
--author | Robert Coltheart | The full name or organization of the solution's author.

## Contributing
Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on how to contribute to this project.

## License
Prefab is released under the [MIT License](LICENSE)
