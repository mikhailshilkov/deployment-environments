#!/bin/bash

# Copyright (c) Microsoft Corporation.
# Licensed under the MIT License.

set -e # exit on error

DIR=$(dirname "$0")
source  $DIR/_common.sh

echo -e "\n>>> Pulumi Info...\n"
pulumi version

# Login to Pulumi (Cloud or Local)
pulumiLogin

echo -e "\n>>> Selecting Pulumi Stack...\n"
pulumi stack select $ADE_ENVIRONMENT_NAME

echo -e "\n>>> Setting Pulumi Config...\n"
export PULUMI_CONFIG_FILE=$ADE_STORAGE/Pulumi.$ADE_ENVIRONMENT_NAME.yaml
echo $PULUMI_CONFIG_FILE
cat $PULUMI_CONFIG_FILE

echo -e "\n>>> Running Pulumi Destroy...\n"
pulumi destroy --refresh --yes --config-file $PULUMI_CONFIG_FILE
