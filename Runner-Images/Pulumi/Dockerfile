# Copyright (c) Microsoft Corporation.
# Licensed under the MIT License.

ARG BASE_IMAGE=mcr.microsoft.com/deployment-environments/runners/core
ARG IMAGE_VERSION=latest

FROM ${BASE_IMAGE}:${IMAGE_VERSION}
WORKDIR /

ARG IMAGE_VERSION

# Metadata as defined at http://label-schema.org
ARG BUILD_DATE

# install pulumi
RUN apk add curl
RUN curl -fsSL https://get.pulumi.com | sh
ENV PATH="${PATH}:/root/.pulumi/bin"
RUN pulumi plugin install resource azure-native

# Grab all .sh files from scripts, copy to
# root scripts, replace line-endings and make them all executable
COPY scripts/* /scripts/
RUN find /scripts/ -type f -iname "*.sh" -exec dos2unix '{}' '+'
RUN find /scripts/ -type f -iname "*.sh" -exec chmod +x {} \;
