#addin nuget:?package=Cake.OctoDeploy

#tool "GitVersion.CommandLine"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var githubApiKey = Argument("githubapikey", EnvironmentVariable("GITHUB_API_KEY"));

//////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
//////////////////////////////////////////////////////////////////////
var version = "0.1.0";

var artifacts = Directory("./artifacts");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////
Task("Clean")
    .Does(() =>
{
    if (DirectoryExists(artifacts))
    {
        DeleteDirectory(artifacts, new DeleteDirectorySettings
        {
            Recursive = true,
            Force = true
        });
    }
});

Task("Versioning")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var result = GitVersion();

    version = result.NuGetVersion;
});

Task("Package")
    .IsDependentOn("Versioning")
    .Does(() =>
{
    NuGetPack("Prefab.nuspec", new NuGetPackSettings
    {
        Version = version,
        OutputDirectory = artifacts,
        ArgumentCustomization = x => x.Append("-NoDefaultExcludes")
    });
});

Task("Publish")
    .IsDependentOn("Package")
    .WithCriteria(() => BuildSystem.IsRunningOnAppVeyor)
    .WithCriteria(() => AppVeyor.Environment.Repository.Tag.IsTag)
    .Does(() =>
{
    var packages = GetFiles("./artifacts/**/*.nupkg");

    foreach (var package in packages)
    {
        UploadArtifact(AppVeyor.Environment.Repository.Tag.Name, package, package.GetFilename().ToString(), "application/zip", new OctoDeploySettings
        {
            AccessToken = githubApiKey,
            Owner = "robertcoltheart",
            Repository = "Prefab"
        });
    }
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////
Task("Default")
    .IsDependentOn("Publish");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////
RunTarget(target);
