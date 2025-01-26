## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Any IDE that supports .NET 8.0 SDK (e.g., [Visual Studio Code](https://code.visualstudio.com/))

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/im211-final-project.git
    ```
2. Open the solution file `im211-final-project.sln` in Visual Studio.

### User Secrets Setup

To set up user secrets for `ConnectionStrings:MySqlVersion` and `ConnectionStrings:MySqlConnectionString`, follow these steps:

1. Set the `ConnectionStrings:MySqlVersion` secret:
    ```sh
    dotnet user-secrets set "ConnectionStrings:MySqlVersion" "your_mysql_version"
    ```

2. Set the `ConnectionStrings:MySqlConnectionString` secret:
    ```sh
    dotnet user-secrets set "ConnectionStrings:MySqlConnectionString" "your_mysql_connection_string"
    ```

### Migration Setup for MySQL Server

To implement migrations for the MySQL server, follow this step:

1. Apply the existing migrations to the database:
    ```sh
    dotnet ef database update
    ```

### Project Components

- **Backend**: ASP.NET MVC Framework with Entity Framework Core
- **Frontend**: HTML, CSS, JavaScript
- **Database**: MySQL
- **Libraries**: Bootstrap, jQuery

### License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

### Acknowledgments

- IM211 group 8 of BSIT-2B
- [Bootstrap](https://getbootstrap.com/)
- [jQuery](https://jquery.com/)