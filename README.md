# Orders-System-App

## Add your files

- [ ] [Create](https://docs.gitlab.com/ee/user/project/repository/web_editor.html#create-a-file) or [upload](https://docs.gitlab.com/ee/user/project/repository/web_editor.html#upload-a-file) files
- [ ] [Add files using the command line](https://docs.gitlab.com/ee/gitlab-basics/add-file.html#add-a-file-using-the-command-line) or push an existing Git repository with the following command:

```
cd existing_repo
git remote add origin https://gitlab.com/OrdersSystem-App.git
git branch -M main
git push -uf origin main
```

## Name
Orders Application.


## Description
System which allow to see users and also for every user his orders by admin user
Each user can login and see his orders only.

## Pre Installation
Sql Server:
Install Sql server management

Optional: Docker installation
* Only of running by docker: Install docker or docker desktop from: https://docs.docker.com/desktop/install/windows-install/

Client Application
Install Node.js from: https://nodejs.org/en/download/
Install angular version 14.2.8

Server
Required software: Visual Studio.

## Installation and Build Commands

Client
Go to download folder ./Client and open cmd window
run 'npm install' in target have all dependencies
npm run build
npm run start or ng serve

Server
Required software: Visual Studio.
Make sure all required dependencies are downloading from nuget:

Sql Server: 
- Data base will bi initialized during running the server on first time.

## Run Applications
There are 2 options to run the system:
 - Using localhost
 - Using Docker-compose

 Localhost Option:

 Client:
  - Go to download folder ./Client and open cmd window
  - run npm start or ng serve is angular cli is installed
	Client web application is up and listening on: http://localhost:4200/

Server:
 - Open OrdersServer solution using visual studio (considering project already have all relevant dependencies) just build and run as OrdersSystem application to run as kestrel web server (development mode)
   Server web application is up and listening on: http://localhost:5000/
  * Using this link: http://localhost:5000/swagger/index.html will open Swashbuckle and will use for testing relevant api's

 Docker-compose Option:

  - Client:
    go to ./Client and open cmd window:
    - docker-compose build
    - docker-compose up  


  - Server:
  go to ./Server  open cmd window:
    - docker-compose build
    - docker-compose up  
      Server web application is up and listening on: http://localhost:5000/
    * Using this link: http://localhost:5000/swagger/index.html will open Swashbuckle and will use for testing relevant api's

Docker Verification (if docker desktop was installed):
- go to docker desktop, and verify all container are up an running

## Support
For any issue, please contact: ichei83@gmail.com

