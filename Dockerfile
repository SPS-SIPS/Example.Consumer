FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
WORKDIR /src
COPY SIPS.Example.Consumer.csproj .
RUN dotnet restore SIPS.Example.Consumer.csproj -a $TARGETARCH

COPY . .
RUN dotnet publish SIPS.Example.Consumer.csproj \
    -a $TARGETARCH \
    -c Release \
    --no-restore \
    --self-contained false \
    -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SIPS.Example.Consumer.dll"]
