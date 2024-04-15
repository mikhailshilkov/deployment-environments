#!/bin/bash

# Copyright (c) Microsoft Corporation.
# Licensed under the MIT License.

set -e # exit on error

# Set up Pulumi Azure Native managed identity.
export ARM_USE_MSI=true
export ARM_CLIENT_ID=$ADE_CLIENT_ID
export ARM_TENANT_ID=$ADE_TENANT_ID
export ARM_SUBSCRIPTION_ID=$ADE_SUBSCRIPTION_ID

echo -e "\n>>> Pulumi Info...\n"
pulumi version

echo -e "\n>>> Pulumi Local Login...\n"
export PULUMI_CONFIG_PASSPHRASE=
pulumi login file://$ADE_STORAGE

echo -e "\n>>> Selecting Pulumi Stack...\n"
pulumi stack select $ADE_ENVIRONMENT_NAME

echo -e "\n>>> Setting Pulumi Configuration...\n"
pulumi config set azure-native:location $ADE_ENVIRONMENT_LOCATION
pulumi config set resource-group-name $ADE_RESOURCE_GROUP_NAME
echo "$ADE_OPERATION_PARAMETERS" | jq -r 'to_entries|.[]|[.key, .value] | @tsv' |
  while IFS=$'\t' read -r key value; do
    pulumi config set $key $value
  done

echo -e "\n>>> Running Pulumi Destroy...\n"
pulumi destroy --refresh --yes
