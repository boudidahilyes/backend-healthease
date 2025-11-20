# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy only the csproj with correct folder structure
COPY Prescription/Prescription/Prescription.csproj Prescription/Prescription/

# restore
RUN dotnet restore Prescription/Prescription/Prescription.csproj

# copy the rest of the source
COPY . .

# publish using the exact project path
RUN dotnet publish Prescription/Prescription/Prescription.csproj -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "Prescription.dll"]
