#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./*.sln", "./WeChatSrc/"]
COPY ["./WeChat/*.csproj", "./WeChatSrc/WeChat/"]
COPY ["./WeChat.Common/*.csproj", "./WeChatSrc/WeChat.Common/"]
COPY ["./WeChat.Component/*.csproj", "./WeChatSrc/WeChat.Component/"]
WORKDIR /src/WeChatSrc
RUN pwd
RUN dotnet restore

COPY ./WeChat/. ./WeChat/
COPY ./WeChat.Common/. ./WeChat.Common/
COPY ./WeChat.Component/. ./WeChat.Component/
RUN pwd
RUN ls
#RUN dotnet build "WeChat.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/src/WeChatSrc/WeChat"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeChat.dll"]