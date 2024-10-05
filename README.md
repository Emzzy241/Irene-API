## Irene API
### A Robust Payment API
### made by Emmanuel Mojiboye

## Technologies Used
* <strong>Version Control: </strong>Git
* <strong>Backend: </strong>ASP.NET Core, C#
* <strong>Frontend: </strong>SwaggerUI
* <strong>Database: </strong>MySQL
* <strong>ORM:</strong> Entity Framework Core
* <strong>Architecture:</strong> Layered (3-tier) Architecture
* <strong>ASP.NET Core RESTful APIs</strong>
* JSON
 * <strong>Authentication: </strong>JSON Web Tokens(J-W-T)

## Description
_I am participating in the Concordia University Hackathon, which ends on October 2nd, 2024, on Devpost.com. This is my hackathon project: a Payment API designed for processing payments, built using a layered (3-tier) architecture_

## Project Architecture
The project follows a **Layered (3-tier) Architecture** pattern, which facilitates separation of concerns and improves scalability. The main components of the project are described below:

- **Presentation Layer**: Implements the API endpoints, handling HTTP requests and responses. This layer interacts with the client and communicates with the business logic layer.
  
- **Business Logic Layer**: Contains the core business logic and validation. It processes data coming from the presentation layer and interacts with the data access layer to perform CRUD operations.

- **Data Access Layer**: Represents the database entities and handles all interactions with the MySQL database using Entity Framework Core. This layer abstracts the database logic from the other layers.

## Project Structure
IreneAPI/ 
│ ├── Controllers/ # API Controllers for handling requests 
| ├── Models/ # Database entities and DTOs 
| ├── Services/ #Business logic implementation
| ├── Repositories/ # Data access layer  implementation 
| ├── Data/ # Database context and migrations  ├── wwwroot/ # Static files (if any) 
|  ├── appSettings.json # Configuration file for connection strings and JWT settings 
|  ├── README.md # Documentation of the project └── Program.cs # Main entry point of the application

## API Endpoints
```sh
* GET http://localhost:5000/api/payments/
* GET http://localhost:5000/api/payments/{id}
* POST http://localhost:5000/api/payments
* PUT http://localhost:5000/api/payments{id}
* DELETE http://localhost:5000/api/payments/{id}
```

### Optional Query String Parameters for GET Requests



### Example Queries

1. The following query will return all payments with a firstname value of Dynasty i.e search for all payments that are Dynasty's:
```sh
GET http:localhost:5000/api/payments?firstname=Dynasty
```

2. The following query will return all payments with the lastname Matilda:
```sh
GET http:localhost:5000/api/payments?lastname=matilda
```

3. The following query will return all payments that exceed a $1000 amount:
```sh
    GET http:localhost:5000/api/payments?minimum=1000
```

4. Its possible to combine multiple queries with &. The following query will return all payments with specie Dynasty, that exceed a $1000 amount:

```sh
    GET http:localhost/5000/api/payments?name=Dynasty&minimum=1000
```

### Additional Requirement when making a POST request
* _When making a POST request to http://localhost:5000/api/payments/ you need to include a body. <b>Please do not add in the id when making a POST i.e when creating an animal, the database will help out with that</b>.
Here is an example body in JSON:_
``` json
    {
        "lastname": "Tyrannosaurus Rex",
        "firstname": "Elizabeth",
        "amount": 1000
    }
```

### Additional Requirement when making a PUT request
* _When making a PUT request to http://localhost:5000/api/payments/{id} you need to include a body that includes the paymentId property. A PUT request is to edit, please add in the id of the animal that you want to edit.Here's an example body in JSON:_
``` json
    {
        "paymentId": 1,
        "lastname": "Tyrannosaurus Rex",
        "firstname": "Elizabeth",
        "amount": 1000
    }
```
Do not include $ to your amount, the application already did that for you
And here is the PUT request we would send the previous body to:
```sh
    http://localhost:5000/api/payments/1
```
Notice that the value of paymentId needs to match the id number in the URL in this example, they are both 1



## Setup/Installation
1. **Clone the Repository**: Open your Git Bash terminal and run the following command to clone the project:
    ```sh
    git clone _REPOSITORY_NAME_
    ```

2. **Install .NET 6.0 Framework**: Ensure you have the .NET 6.0 Framework installed (used .NET 6.0.402 for this application) on your PC. Make sure you have completed the essential steps to write C# code in the C# REPL Terminal.

3. **Create `appSettings.json`**: After cloning the repository, you need to create an `appSettings.json` file in the root directory of the project. Be sure to create it in the production directory of your project (`IreneAPI.Solution/IreneAPI/appSettings.json`). Use the following template for your `appSettings.json`:

    ```json
    {
        "ConnectionStrings": {
            "DefaultConnection": "Server=localhost;Port=3306;database=irene_api;uid=[YOUR-USERNAME-HERE];pwd=[YOUR-PASSWORD-HERE];"
        }
    }
    ```

    Replace `[YOUR-USERNAME-HERE]` and `[YOUR-PASSWORD-HERE]` with your actual MySQL username and password.

4. **Build and Run the Application**:
    - If you're not interested in seeing the build messages, run the following command to build and run the application:
      ```sh
      dotnet run
      ```
      This command builds and runs the application for you.

    - If you are interested in seeing the build messages, follow these steps:
        1. **Build the Project**: Run the following command to build the project and add the essential directories to execute the application:
            ```sh
            dotnet build
            ```

        2. **Run the Project**: After building the application, run the following command to see all of the results outputted into the console:
            ```sh
            dotnet run
            ```
            Copy the link where the project is hosted on git bash or nay other terminal window you are use and paste the link in a browser

        3. **Run the Project with watch run**: This is the one we really recommend because it opens up the Swagger UI on your default browser, and there you can carry out all the API requests that have been defined inside the controller. Run the code below:
        ```sh
            dotnet watch run
        ```
        With the watch run, developers are able to simultaneously update the application's code and  also build the project.

        

## Detected Bugs/ Issues
* No detected bugs

## License 
Licensed under the GNU General Public License

## Contact Info
* _Email: emzzyoluwole@gmail.com_
* _Instagram @Emmanuel.9944_
* _Twitter: @Emzzy241 and Profile Name: Dynasty_
* _Github Username: Emzzy241_
