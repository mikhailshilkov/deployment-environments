using Pulumi;
using Pulumi.AzureNative.Storage;
using Pulumi.AzureNative.Storage.Inputs;
using System.Collections.Generic;

return await Pulumi.Deployment.RunAsync(() =>
{
    var config = new Config();
    var resourceGroupName = config.Require("resource-group-name");
    var skuName = config.Get("sku") ?? "Standard_LRS";
    var storageAccount = new StorageAccount("sa", new StorageAccountArgs
    {
        ResourceGroupName = resourceGroupName,
        Sku = new SkuArgs
        {
            Name = skuName
        },
        Kind = Kind.StorageV2
    });
    return new Dictionary<string, object?>(
        ["saname"] = storageAccount.Name
    );
});
