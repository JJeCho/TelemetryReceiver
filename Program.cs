using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.WebSockets;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWebSockets();

app.Use(async (context, next) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        await HandleWebSocketConnection(webSocket);
    }
    else
    {
        await next();
    }
});

async Task HandleWebSocketConnection(WebSocket webSocket)
{
    var buffer = new byte[1024 * 4];
    while (webSocket.State == WebSocketState.Open)
    {
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        var receivedData = Encoding.UTF8.GetString(buffer, 0, result.Count);

        try
        {
            var telemetryData = JsonSerializer.Deserialize<TelemetryData>(receivedData);

            Console.WriteLine($"Received Data: {JsonSerializer.Serialize(telemetryData, new JsonSerializerOptions { WriteIndented = true })}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to process received data: {ex.Message}");
        }
    }
}

app.Run();

public class TelemetryData
{
    public GpsData GpsData { get; set; }
    public SensorData SensorData { get; set; }
    public EnvironmentalData EnvironmentalData { get; set; }
    public SystemHealthData SystemHealthData { get; set; }
    public CommunicationsData CommunicationsData { get; set; }
    public double Timestamp { get; set; }
}

public class GpsData
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Altitude { get; set; }
    public double Speed { get; set; }
}

public class SensorData
{
    public double AccelerationX { get; set; }
    public double AccelerationY { get; set; }
    public double AccelerationZ { get; set; }
    public double OrientationPitch { get; set; }
    public double OrientationRoll { get; set; }
    public double OrientationYaw { get; set; }
    public double RotationalVelocityX { get; set; }
    public double RotationalVelocityY { get; set; }
    public double RotationalVelocityZ { get; set; }
}

public class EnvironmentalData
{
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public double Pressure { get; set; }
}

public class SystemHealthData
{
    public double BatteryLevel { get; set; }
    public double SignalStrength { get; set; }
    public int ErrorCode { get; set; }
    public double InternalTemperature { get; set; }
}

public class CommunicationsData
{
    public double UplinkLatency { get; set; }
    public double DownlinkLatency { get; set; }
    public double PacketLossRate { get; set; }
    public double DataTransferRate { get; set; }
}
