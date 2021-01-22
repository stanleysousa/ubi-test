# General information

This repository consists of 3 components, all of then running on Docker.

# 1 - Database

**Creating the database image:**

Open the command line on the "database" folder and execute
```sh
$ docker build -t inventory-db .
```

**Running database container:**

 Open the command line on the "database" folder and execute
```sh
$ docker run -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root -e MYSQL_DATABASE=Inventory -e MYSQL_USER=user -e MYSQL_PASSWORD=user inventory-db
```

Note that the following environment variables defines database's settings and its value can be changed as desired:
>MYSQL_ROOT_PASSWORD
>MYSQL_DATABASE
>MYSQL_USER
>MYSQL_PASSWORD

**Verifying that the database is working:**
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
```sh
$ SHOW DATABASES
$ use Inventory
$ select * from user;
```

