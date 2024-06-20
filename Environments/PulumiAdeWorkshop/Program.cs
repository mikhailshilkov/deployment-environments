using Pulumi;
using Pulumi.AzureNative.Resources;
using Pulumi.AzureNative.App;
using Pulumi.AzureNative.App.Inputs;
using System.Collections.Generic;
using System.Runtime.InteropServices;

return await Pulumi.Deployment.RunAsync(() =>
{
    var config = new Pulumi.Config();
    var resourceGroupName = config.Require("resource-group-name");
    var imageName = config.Require("image");

    var managedEnv = new ManagedEnvironment("env", new()
    {
        ResourceGroupName = resourceGroupName,
    });

    var containerApp = new ContainerApp("app", new() 
    {
        ResourceGroupName = resourceGroupName,
        ManagedEnvironmentId = managedEnv.Id,
        Configuration = new ConfigurationArgs
        {
            Ingress = new IngressArgs
            {
                External = true,
                TargetPort = 80,
            }
        },
        Template = new TemplateArgs
        {
            Containers = 
            {
                new ContainerArgs
                {
                    Name = "app",
                    Image = imageName
                }
            
            }
        }
    });

    return new Dictionary<string, object?>
    {
        ["url"] = containerApp.LatestRevisionFqdn
    };
});