using Pulumi;
using Pulumi.AzureNative.App;
using Pulumi.AzureNative.App.Inputs;
using System.Collections.Generic;

return await Deployment.RunAsync(() =>
{
    var config = new Pulumi.Config();
    var resourceGroupName = config.Require("resource-group-name");
    var imageName = config.Require("image");

    var managedEnvironment = new ManagedEnvironment("env", new()
    {
        ResourceGroupName = resourceGroupName,
    });

    var containerApp = new ContainerApp("app", new() {
        ResourceGroupName = resourceGroupName,
        ManagedEnvironmentId = managedEnvironment.Id,
        Template = new TemplateArgs
        {
            Containers =
            {
                new ContainerArgs
                {
                    Name = "app",
                    Image = imageName,
                }
            }
        },
        Configuration = new ConfigurationArgs
        {
            Ingress = new IngressArgs
            {
                External = true,
                TargetPort = 80
            }
        }
    });

    // Export the primary key of the Storage Account
    return new Dictionary<string, object?>
    {
        ["url"] = containerApp.Configuration.Apply(c => c!.Ingress!.Fqdn)
    };
});