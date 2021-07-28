# rusada
1. To change the database connection access the appsettings.json file and change the relevant parameters to connect to the database
   you created in MS SQL Server.

2. Use the following SQL statement to create the required table on your MS SQL database:

CREATE TABLE Sighting(
	Id 		VARCHAR(100) NOT NULL,
	Location 	VARCHAR(255) NOT NULL,
	Make 		VARCHAR(128) NOT NULL,
	Model 		VARCHAR(128) NOT NULL,
	Registration 	VARCHAR(25) NOT NULL,
	TimeSighted 	DATETIME NOT NULL
 CONSTRAINT pk_Sighting PRIMARY KEY CLUSTERED (Id)
)

3. Run the application...tap "Sightings"



