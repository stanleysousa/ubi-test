# General information

This repository consists of 4 components, all of then running on Docker.
1. Backend-Database
2. Backend-App
3. Frontend-Database
4. Frontend-App

To follow this guide you need:
- [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [Visual Studio 2019](https://visualstudio.microsoft.com)
- [Docker Desktop](https://www.docker.com/products/docker-desktop), otherwise you might need to adapt the steps to your Docker environment.

# 1 - Backend-Database

## Creating the database image:

Open the command line on the **backend-database** folder and execute
```sh
$ docker build -t backend-db .
```

## Running database container:

 Open the command line on the **backend-database** folder and execute
```sh
$ docker run -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root -e MYSQL_DATABASE=Inventory -e MYSQL_USER=user -e MYSQL_PASSWORD=user backend-db
```

Note that the following environment variables defines database's settings and its value can be changed as desired:
>MYSQL_ROOT_PASSWORD
>MYSQL_DATABASE
>MYSQL_USER
>MYSQL_PASSWORD

## Verifying that the database is working:
Open the command line on the **backend-database** folder, get the database image id, run the image and enter it's bash
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

# 2 - Backend-App

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

Update the **ConnectionString** on **appsettings.json** file located at **backend-app\Inventory.Service**
```
  "Config": {
    "ConnectionString": "Server=172.17.0.2;port=3306;Database=Inventory;userid=user;Password=user"
  },
```

## Launch the application

- Open the **Inventory** solution located at **backend-app** on Visual Studio Solution 
- Press start (F5) to run on Docker
- It should launch SwaggerUI automatically on **http://localhost:49159/TestDevWebService/services/index.html**

# 3 - Frontend-Database

## Creating the database image:

Open the command line on the **frontend-database** folder and execute
```sh
$ docker build -t frontend-db .
```

## Running database container:

 Open the command line on the **frontend-database** folder and execute
```sh
$ docker run -d -p 3307:3307 -e MYSQL_ROOT_PASSWORD=root -e MYSQL_DATABASE=Inventory -e MYSQL_USER=user -e MYSQL_PASSWORD=user frontend-db
```

Note that the following environment variables defines database's settings and its value can be changed as desired:
>MYSQL_ROOT_PASSWORD
>MYSQL_DATABASE
>MYSQL_USER
>MYSQL_PASSWORD

## Verifying that the database is working:
Open the command line on the **frontend-database** folder, get the database image id, run the image and enter it's bash
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

# 4 - Frontend-App

## Set database connection:

Check the database IP

```sh
$ docker network inspect bridge
```
Find the IPv4Address for the running container, it is **172.17.0.3** in the example below
```
        "Containers": {
            "13f019c6fca5311e59abf6eda936b884eb438657b84f64312cb9afe4c710741a": {
                "Name": "InventoryUI.Presentation",
                "EndpointID": "b23d05c273a2deee110a55dd10905928aef8579741ab8a8d5f8f638dc6466f7f",
                "MacAddress": "02:42:ac:11:00:04",
                "IPv4Address": "172.17.0.4/16",
                "IPv6Address": ""
            },
            "aa0632b4bba9f9102e80401d6ea00d41aa1b6ceb42b4c23b90a945dc1e5a167b": {
                "Name": "Inventory.Service",
                "EndpointID": "bc962b3939bfcbdcb84aee3d77be934344d82c24ef5bfaaab9b44afb31f67cf7",
                "MacAddress": "02:42:ac:11:00:05",
                "IPv4Address": "172.17.0.5/16",
                "IPv6Address": ""
            },
            "dce98debd1d7d2f91aef336d80e3737864741e0f2018181ff23567c7ec456a41": {
                "Name": "eager_meitner",
                "EndpointID": "8a75e0b334ce3699b98d11c4af61326e0b6c27674557222e377172da18ceeada",
                "MacAddress": "02:42:ac:11:00:02",
                "IPv4Address": "172.17.0.2/16",
                "IPv6Address": ""
            },
            "dff7e448c744a887d8438e9468180697d06b5fc482bc23e3586a2e07a7ae3bb5": {
                "Name": "bold_ramanujan",
                "EndpointID": "be79dd9a16743267c51ee8972ac94cbe77b6778bdb9dd31d05c78a8b600afef6",
                "MacAddress": "02:42:ac:11:00:03",
                "IPv4Address": "172.17.0.3/16",
                "IPv6Address": ""
            }
        },
```

- Update the **ConnectionString** on **appsettings.json** file located at **frontend-app\Inventory.Presentation**
- Check if the **UserServiceURL** port matches the port where the backend service is running on your container
```
  "Config": {
    "ConnectionString": "Server=172.17.0.3;port=3306;Database=Inventory;userid=user;Password=user",
    "UserServiceURL": "http://host.docker.internal:49163/TestDevWebService/services/User/"
  },
```

## Launch the application

- Open the **InventoryUI** solution located at **frontend-app** on Visual Studio Solution 
- Press start (F5) to run on Docker
- It should launch the AspNet page automatically on **http://localhost:49162/**
