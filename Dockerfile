FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY src/StrEnum.System.Text.Json/StrEnum.System.Text.Json.csproj ./src/StrEnum.System.Text.Json/StrEnum.System.Text.Json.csproj
COPY test/StrEnum.System.Text.Json.IntegrationTests/StrEnum.System.Text.Json.IntegrationTests.csproj ./test/StrEnum.System.Text.Json.IntegrationTests/StrEnum.System.Text.Json.IntegrationTests.csproj
RUN dotnet restore

# copy everything else and build app
COPY ./ ./
WORKDIR /source
RUN dotnet build -c release -o /out/package --no-restore

FROM build as test
RUN dotnet test

FROM build as pack-and-push
WORKDIR /source

ARG PackageVersion
ARG NuGetApiKey

RUN dotnet pack ./src/StrEnum.System.Text.Json/StrEnum.System.Text.Json.csproj -o /out/package -c Release
RUN dotnet nuget push /out/package/StrEnum.System.Text.Json.$PackageVersion.nupkg -k $NuGetApiKey -s https://api.nuget.org/v3/index.json