# BarterPoint

BarterPoint is a minimum viable product (MVP) for a bartering app, designed to facilitate the exchange of goods between users. This project aims to provide a simple and effective platform for users to list, browse, and trade items with ease.

## Purpose and Context

BarterPoint is actively under development and primarily serves as a portfolio piece. The main goal is to showcase my skills in full-stack development for job interviews, as employers often request GitHub repositories. There are no immediate plans to take this project beyond its current state. The ongoing updates are focused on refining and demonstrating capabilities rather than learning new technologies.

## Features

- **React Native**: A mobile user interface that adapts well to various devices.
- **.NET Web API**: A backend service to handle user requests, authentication, and transactions.
- **SQL Database**: A data storage and retrieval system to manage user data and bartering activities.

## Setup Options

BarterPoint can be set up either in the cloud or locally:

- **Cloud Setup**: The current setup uses Azure for the SQL server and web API deployment. If you would like to check out the Swagger documentation and the database through Azure Studio, please reach out directly. Your IP address can be whitelisted, and demo user accounts with passwords can be created to provide access.

- **Local Setup**: You can also run the project locally. Follow these steps:

    1. **Set up the SQL Server**:
        - Ensure an SQL server is running locally.
        - Run the provided SQL scripts in the `BarterPoint.Sql` directory to set up the database schema.

    2. **Configure the Web API**:
        - Navigate to the `BarterPoint.Api` directory.
        - Set the connection string in the `appsettings.json` file to point to your local SQL server.
        - Start the web API using the command:
            ```sh
            dotnet run
            ```

    3. **Configure the Mobile App**:
        - Navigate to `BarterPoint.Mobile/src/api/ApiService.ts`.
        - Adjust the `BASE_URL` in the configuration file to point to your local API URL.

    4. **Run the Mobile App**:
        - Navigate back to the `BarterPoint.Mobile` directory.
        - Install dependencies using:
          ```sh
          npm install
          ```
        - Start the React Native Expo app using:
          ```sh
          npm start
          ```

## Usage

Once the application is up and running, users can register, list items they want to barter, browse items from other users, and initiate trades. The app provides an interface for managing bartering activities and tracking transaction history.

For a video demonstration of how to use BarterPoint, including adding a product, placing a bid, and rejecting a bid, please watch the [YouTube Demo Video](https://youtu.be/zPqRwxAFm4k?si=HMBVvn4WwxGtXvWt).

## Future Enhancements

While BarterPoint is a functional MVP, several features and improvements are planned for future releases:

- **Enhanced security measures**: Advanced authentication and authorization mechanisms.
- **User reviews and ratings**: User ratings and reviews to build trust within the community.
- **Geolocation services**: Location-based features to facilitate local trades.
- **Push notifications**: Notifications about trade offers and updates.

## Contributing

Contributions to BarterPoint are welcome. Suggestions or issues can be submitted via pull requests or by opening an issue on GitHub.

## License

This project is licensed under the Creative Commons Attribution 4.0 International (CC BY 4.0) License. See the [LICENSE](./LICENSE) file for more details.