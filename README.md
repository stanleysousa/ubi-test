# General information

This repository consists of 3 components, all of then running on Docker.

To follow this guide you need:
- [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [Visual Studio 2019](https://visualstudio.microsoft.com)
- [Docker Desktop](https://www.docker.com/products/docker-desktop), otherwise you might need to adapt the steps to your Docker environment.

# 1 - Database

## Creating the database image:

Open the command line on the "database" folder and execute
```sh
$ docker build -t inventory-db .
```

## Running database container:

 Open the command line on the "database" folder and execute
```sh
$ docker run -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root -e MYSQL_DATABASE=Inventory -e MYSQL_USER=user -e MYSQL_PASSWORD=user inventory-db
```

Note that the following environment variables defines database's settings and its value can be changed as desired:
>MYSQL_ROOT_PASSWORD
>MYSQL_DATABASE
>MYSQL_USER
>MYSQL_PASSWORD

## Verifying that the database is working:
Open the command line on the 'database' folder, get the database image id, run the image and enter it's bash
```sh
$ docker ps
$ docker exec -it image_id bash
```

Once connected to the container's bash, open mysql:
```sh
$ mysql -uroot -proot
```

Explore the current database the check that the tables have been created and loaded with some data
```
SHOW DATABASES
use Inventory
select * from user;
```

# 2 - Backend

## Set database connection:

Check the database IP

```sh
$ docker network inspect bridge
```
Find the IPv4Address for the running container, it is **172.17.0.2** in the example below
```
[
    {
        "Name": "bridge",
        "Id": "ca0f31a5d7d6585e4f08986cdd097f9d7fb61203ab2a61a5bdff548b9ff36f60",
        "Created": "2021-01-22T13:57:26.9509121Z",
        "Scope": "local",
        "Driver": "bridge",
        "EnableIPv6": false,
        "IPAM": {
            "Driver": "default",
            "Options": null,
            "Config": [
                {
                    "Subnet": "172.17.0.0/16",
                    "Gateway": "172.17.0.1"
                }
            ]
        },
        "Internal": false,
        "Attachable": false,
        "Ingress": false,
        "ConfigFrom": {
            "Network": ""
        },
        "ConfigOnly": false,
        "Containers": {
            "711bd2687af8b5e376723b84bb24bfb0924874ab46a56731faac929d8651f1b7": {
                "Name": "busy_hypatia",
                "EndpointID": "98509c031dfdbf2bd794c37c7a0045dfe816c5c850fc693ca03deaa2b4c1c774",
                "MacAddress": "02:42:ac:11:00:02",
                "IPv4Address": "172.17.0.2/16", 
                "IPv6Address": ""
            }
        },
```

Update the **ConnectionString** on **appsettings.json** file located at **backend\Inventory.Service**
```
  "Config": {
    "ConnectionString": "Server=172.17.0.2;port=3306;Database=Inventory;userid=user;Password=user"
  },
```

## Launch the application

- Open the **Inventory** solution located at **backend** on Visual Studio Solution 
- Press start (F5) to run on Docker
- It should launch SwaggerUI automatically on **http://localhost:49159/TestDevWebService/services/index.html**
