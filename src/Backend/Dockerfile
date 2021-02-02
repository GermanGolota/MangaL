#Get SDK
FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-env 
WORKDIR /src
#Copy API project
COPY MangaLWebAPI/MangaLWebAPI.csproj API/
#Copy the rest of the files of API project
COPY MangaLWebAPI/ API/
#Copy dependent project
COPY Core Core/
COPY DataAccess DataAccess/
COPY Infrastructure Infrastructure/
#Restore nuget packages
RUN dotnet restore API/MangaLWebAPI.csproj

#Build the project
RUN dotnet build API/MangaLWebAPI.csproj
#Publish
RUN dotnet publish -c Release API/MangaLWebAPI.csproj -o out
#Generate runtime image
FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /src
ENV ASPNETCORE_ENVIROMENT=Production
EXPOSE 5001
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5001;https://+:5000   
COPY --from=build-env /src/out .
ENTRYPOINT ["dotnet", "MangaLWebAPI.dll"]