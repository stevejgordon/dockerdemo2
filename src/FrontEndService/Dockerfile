FROM microsoft/dotnet:2.1-sdk

WORKDIR /app

COPY . .

RUN dotnet build

EXPOSE 80

ENTRYPOINT ["dotnet", "run"]