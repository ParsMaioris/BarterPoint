# BarterPoint

BarterPoint is a minimum viable product (MVP) for a bartering app, designed to facilitate the exchange of goods between users. This project aims to provide a simple and effective platform for users to list, browse, and trade items with ease.

## Purpose and Context

BarterPoint is actively under development and primarily serves as a portfolio piece. The main goal is to showcase my skills in full-stack development for job interviews, as employers often request GitHub repositories. There are no immediate plans to take this project beyond its current state. The ongoing updates are focused on refining and demonstrating capabilities rather than learning new technologies.

## Features

- **React Native**: A mobile user interface that adapts well to various devices.
- **.NET Web API**: A backend service to handle user requests, authentication, and transactions.
- **SQL Database**: A data storage and retrieval system to manage user data and bartering activities.

For video demonstrations of these features, please refer to the [Usage section](#usage).

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

Once the application is operational, users can:

- **Register:** Create an account to start bartering.
- **List Items:** Offer items for trade.
- **Browse:** Explore items listed by other users.
- **Initiate Trades:** Propose and manage trades with other users.
- **Manage Activities:** Oversee bartering activities and track transaction history.

### Video Demonstrations

For a deeper understanding of BarterPoint and its backend integration, explore our video tutorials:

- **Azure SQL Integration: Adding a Product, Placing a Bid, and Rejecting a Bid**  
  <a href="https://youtu.be/zPqRwxAFm4k?si=HMBVvn4WwxGtXvWt">
    <img src="https://img.youtube.com/vi/zPqRwxAFm4k/0.jpg" alt="Azure SQL Integration" width="300"/>
  </a>  
  *Watch on YouTube: [Azure SQL Integration](https://youtu.be/zPqRwxAFm4k?si=HMBVvn4WwxGtXvWt)*

- **User Features: Rating a User, Viewing Transaction History, Approving a Bid, and User Favorites**  
  <a href="https://youtu.be/UssjjZkvYl0">
    <img src="https://img.youtube.com/vi/UssjjZkvYl0/0.jpg" alt="Mobile App Features" width="300"/>
  </a>  
  *Watch on YouTube: [Mobile App Features](https://youtu.be/UssjjZkvYl0)*

## Future Enhancements

While BarterPoint is a functional MVP, several features and improvements are planned for future releases:

- **Microservices Architecture**: Gradually refactor services into microservices for better scalability and maintainability.
- **Push Notifications**: Implement notifications to alert users about trade offers and updates.
- **Caching**: Introduce caching in the web API to enhance performance and efficiency.
- **Comprehensive Logging**: Implement logging on both the client side (mobile application) and the web API side for better monitoring and troubleshooting.
- **Queuing Mechanisms**: Explore the use of queuing for efficient handling of asynchronous tasks.
- **Enhanced Security Measures**: Implement advanced authentication and authorization mechanisms.
- **Geolocation Services**: Add location-based features to facilitate local trades.

## Contributing

Contributions to BarterPoint are welcome. Suggestions or issues can be submitted via pull requests or by opening an issue on GitHub.

## License

This project is licensed under the Creative Commons Attribution 4.0 International (CC BY 4.0) License. See the [LICENSE](./LICENSE) file for more details.