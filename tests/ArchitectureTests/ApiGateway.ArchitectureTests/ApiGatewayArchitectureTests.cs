using FluentAssertions;
using System.Reflection;

namespace ApiGateway.ArchitectureTests;

public class ApiGatewayArchitectureTests
{
    private const string _applicationNamespace = "ApiGateway.Application";
    private const string _infrastructureNamespace = "ApiGateway.Infrastructure";
    private const string _webApiNamespace = "ApiGateway.WebApi";

    [Fact]
    public void ApplicationShouldNotDependOnInfrastructureAndWebApi()
    {
        // Arrange
        Assembly applicationAssembly = GetAssemblyByProjectName(_applicationNamespace);

        string[] apiGatewayProjects = new[]
        {
            _infrastructureNamespace,
            _webApiNamespace,
        };
        string[] applicationReferencedAssemblyNames = GetReferencedAssemblyNames(applicationAssembly);

        // Act

        // Assert
        apiGatewayProjects
            .Any(project => applicationReferencedAssemblyNames.Contains(project))
            .Should()
            .BeFalse();
    }

    [Fact]
    public void InfrastructureShouldNotDependOnWebApi()
    {
        // Arrange
        Assembly infrastructureAssembly = GetAssemblyByProjectName(_infrastructureNamespace);
        string[] apiGatewayProjects = new[]
        {
            _webApiNamespace,
        };

        string[] infrastructureReferencedAssemblyNames = GetReferencedAssemblyNames(infrastructureAssembly);

        // Act

        // Assert
        apiGatewayProjects
            .Any(project => infrastructureReferencedAssemblyNames.Contains(project))
            .Should()
            .BeFalse();
    }

    [Fact]
    public void InfrastructureShouldOnlyDependOnApplication()
    {
        // Arrange
        Assembly infrastructureAssembly = GetAssemblyByProjectName(_infrastructureNamespace);
        string[] apiGatewayProjects = new[]
        {
            _applicationNamespace,
        };

        string[] infrastructureReferencedAssemblyNames = GetReferencedAssemblyNames(infrastructureAssembly);

        // Act

        // Assert
        apiGatewayProjects
            .Any(project => infrastructureReferencedAssemblyNames.Contains(project))
            .Should()
            .BeTrue();
    }

    [Fact]
    public void WebApiShouldDependOnApplicationAndInfrastructure()
    {
        // Arrange
        Assembly webApiAssembly = GetAssemblyByProjectName(_webApiNamespace);
        string[] apiGatewayProjects = new[]
        {
            _applicationNamespace,
            _infrastructureNamespace
        };

        string[] webApiReferencedAssemblyNames = GetReferencedAssemblyNames(webApiAssembly);

        // Act

        // Assert
        apiGatewayProjects
            .All(project => webApiReferencedAssemblyNames.Contains(project))
            .Should()
            .BeTrue();
    }


    private static Assembly GetAssemblyByProjectName(string projectName)
    {
        Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        return
            loadedAssemblies
            .FirstOrDefault(assembly =>
                !assembly.IsDynamic &&
            !string.IsNullOrEmpty(assembly.Location) &&
                assembly.GetName().Name == projectName) ??
                throw new FileNotFoundException($"No assembly could be found with {projectName} project name.");
    }

    private static string[] GetReferencedAssemblyNames(Assembly assembly)
    {
        AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();

        string[] referencedAssemblyNames =
            referencedAssemblies
            .Where(referencedAssembly =>
                !string.IsNullOrWhiteSpace(referencedAssembly.Name) &&
                referencedAssembly.Name.Contains("ApiGateway")
                )
            .Select(referencedAssembly => referencedAssembly.Name)
            .ToArray()!;

        return referencedAssemblyNames;
    }
}