import * as pulumi from "@pulumi/pulumi";
import * as storage from "@pulumi/azure/storage";

const config = new pulumi.Config();
const resourceGroupName = config.require("resource-group-name");

const storageAccount = new storage.Account("sa", {
    resourceGroupName: resourceGroupName,
    accountTier: "Standard",
    accountReplicationType: "LRS",
    accountKind: "StorageV2",
});

export const saname = storageAccount.name;
