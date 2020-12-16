#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
RUN pwd
COPY ["./*.sln", "./WeChatSrc/"]
COPY ["./WeChat/*.csproj", "./WeChatSrc/WeChat/"]
COPY ["./WeChat.Common/*.csproj", "./WeChatSrc/WeChat.Common/"]
COPY ["./WeChat.Component/*.csproj", "./WeChatSrc/WeChat.Component/"]
RUN pwd
WORKDIR /WeChatSrc
RUN pwd
RUN dotnet restore

WORKDIR "/src/WeChatSrc"
COPY ./WeChat/. ./WeChat/
COPY ./WeChat.Common/. ./WeChat.Common/
COPY ./WeChat.Component/. ./WeChat.Component/
RUN pwd
RUN ls
#RUN dotnet build "WeChat.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR "/app/src/WeChatSrc"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeChat.dll"]