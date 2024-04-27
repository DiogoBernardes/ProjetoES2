# Event and Participant Management  #

### Introduction ###
This project consists of developing an event and participant management system, with the source code in C# organized in a solution composed of a backend project in Web API and a frontend project in Blazor.

- **User Authentication:** Users can create accounts and log in with three levels of authentication: User, UserManager, and Admin, each with specific permissions.

- **User Types:** Users can act as event organizers or participants.

- **Event Management:** Organizers can create events with detailed information, including name, date, time, location, description, maximum capacity, and ticket price. They can also define various ticket types for each event.

- **Event Editing:** Organizers have the ability to edit event information and manage activities, specifying details such as activity name, date, time, and description.

- **Event Search:** Participants can search for events by name, date, location, and category.

- **Event Registration:** Participants can register for events and select specific activities within the event.

- **Profile Management:** Participants can manage their profiles by editing personal information such as name, email, and phone number.

- **Unsubscribe:** Participants can opt to unsubscribe from specific events and activities.

- **View Participants:** Organizers can view lists of registered participants for their events and specific activities.

- **Communicating with Participants:** Organizers can send messages and updates to participants registered for their events.

- **General and Specific Reports:** Users can generate general event reports, including metrics such as the number of events per category and total participants. Additionally, specific reports for each event are available, detailing participant counts per activity and revenue generated.

General requirements include the implementation of three levels of authentication, data persistence in a database using Entity Framework, a simple web interface, data validation, structuring of the backend using architectural patterns and the use of Design Patterns.

<br>

### How to I setup my development environment? ###
This solution allows you to easily install the development environment and its dependencies.
This structure can be used for the project assignment in Software Engineering II course from Informatics Engineering at IPVC/ESTG.


* Install [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* There are 2 alternatives:
  * Compile and run the Infrastructure project 
```
cd Infrastructure
dotnet build
dotnet run
```
  * Run docker compose commands manually using the following set of commands in the root folder of the infrastructure project:
```
docker-compose build
docker-compose up
```
  * ***Note:*** you can use the **-d** flag to run the docker containers in background
```
docker-compose up -d
```

### Docker Images ###

#### PostgreSQL Database ####

* Available at localhost:PORT_NUMBER or db:5432 within docker virtual network. The port is defined by the variable EXP_PORT_PG in the .env file.
* This database also installs PostGIS to allow for dealing with the geographical data, if needed
* The credentials for the database are:
    * **username**: es2
    * **password**: es2
    * **database**: es2
* Please note that this image will recreate the database when it runs, if no previous volume is available. The database will be created based on the scripts under the migrations folder - feel free to modify those and adapt to your specific case.

#### Volumes ####

##### *pg_data* #####
This is a volume to store and keep the database data intact, even if you stop the containers.
