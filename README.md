# TelemetryReceiver

## Overview

TelemetryReceiver is a .NET application designed to receive and process streams of telemetry data over WebSocket connections. It serves as a backend receiver that can handle real-time data, making it suitable for testing or integrating with telemetry data streams from various sources.

## Features

- **WebSocket Server**: Listens for incoming WebSocket connections and handles data exchange.
- **JSON Data Parsing**: Processes incoming telemetry data in JSON format, allowing for easy integration with systems that produce JSON-encoded streams.
- **Asynchronous Handling**: Uses async programming to efficiently manage multiple simultaneous WebSocket connections.
- **Configuration**: Adjustable settings for WebSocket handling and server configuration through `appsettings.json`.

## Getting Started

### Prerequisites

- .NET 6.0 SDK or higher

### Installation

Clone the repository and navigate into the project directory:

```bash
git clone https://github.com/yourusername/TelemetryReceiver.git
cd TelemetryReceiver
```

### Configuration

Before running the application, you can adjust settings in `appsettings.json`:

- **Port**: Specify the port on which the WebSocket server listens.
- **WebSocket settings**: Customize parameters for WebSocket handling.

### Usage

To run the application, use the following command:

```bash
dotnet run
```

The WebSocket server will start, listening for incoming connections. Clients can connect to the specified WebSocket endpoint and stream telemetry data.

### Example

A sample client can send data to the WebSocket endpoint using tools like `wscat`:

```bash
wscat -c ws://localhost:5000/telemetry
```

### Customization

You can modify the `Program.cs` file to customize how the application handles incoming data, including parsing logic or data storage mechanisms.

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests for enhancements, bug fixes, or new features.

## License

This project is licensed under the MIT License. See the `LICENSE` file for more details.

## Acknowledgments

Inspired by the need for reliable telemetry data reception and processing in real-time applications.
