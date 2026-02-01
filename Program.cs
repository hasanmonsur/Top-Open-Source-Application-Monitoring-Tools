using Prometheus;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Custom counter similar to the Python example
var requestsTotal = Metrics.CreateCounter(
    "http_requests_total",
    "Total HTTP requests",
    "endpoint");

app.MapGet("/", () =>
{
    requestsTotal.WithLabels("/").Inc();  // Increment counter for this endpoint
    return "Hello, monitored world!";
});

// Optional: Automatically track built-in HTTP metrics (request count, duration, etc.)
app.UseHttpMetrics();

// Expose the Prometheus scrape endpoint at /metrics
app.MapMetrics();

app.Run();