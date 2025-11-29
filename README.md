# Universal E-Commerce Integration Middleware

A professional ASP.NET Core MVC middleware application that connects ERP systems (D365) with e-commerce platforms (Shopify, Amazon, VTEX) for seamless data synchronization.

![Dashboard Screenshot](https://github.com/user-attachments/assets/8b94b65e-307e-4073-a157-088411e2b766)

## Features

- **Multi-Platform Connectors**: Connect to Shopify, Amazon, VTEX, D365, and custom platforms
- **Integration Flows**: Configure data synchronization flows between any connected platforms
- **Mapping Templates**: Define custom JSON transformations between source and target schemas
- **Job Monitoring**: Track execution status, success/failure rates, and processing times
- **Webhook Support**: Receive and process events from external platforms
- **Comprehensive Logging**: Monitor system activity and debug issues

## Project Structure

```
src/UniversalIntegrationMiddleware/
├── Controllers/
│   ├── DashboardController.cs      # System overview
│   ├── ConnectorController.cs      # Platform connections management
│   ├── FlowController.cs           # Integration flows configuration
│   ├── MappingController.cs        # Transformation templates
│   ├── JobController.cs            # Job execution monitoring
│   ├── LogsController.cs           # System logs viewer
│   └── WebhookController.cs        # External webhook endpoints
├── Models/
│   ├── Enums/
│   │   ├── Platform.cs             # Shopify, Amazon, VTEX, D365, Custom
│   │   ├── FlowType.cs             # Orders, Inventory, Products
│   │   ├── JobStatus.cs            # Pending, Running, Success, Failed, PartialSuccess
│   │   └── LogLevel.cs             # Info, Warning, Error
│   ├── Merchant.cs                 # Organization/tenant
│   ├── ChannelConnection.cs        # Platform connection configuration
│   ├── IntegrationFlow.cs          # Sync flow definition
│   ├── MappingTemplate.cs          # JSON transformation rules
│   ├── Schedule.cs                 # Cron/interval scheduling
│   ├── JobRun.cs                   # Job execution record
│   ├── LogEntry.cs                 # System log entry
│   ├── WebhookEndpoint.cs          # Registered webhook URL
│   └── WebhookEvent.cs             # Incoming webhook event
├── ViewModels/
│   ├── DashboardViewModel.cs
│   ├── ConnectorViewModel.cs
│   ├── CreateFlowViewModel.cs
│   ├── FlowViewModel.cs
│   ├── MappingViewModel.cs
│   ├── JobDetailsViewModel.cs
│   └── LogViewModel.cs
├── Services/
│   ├── IDashboardService.cs / DashboardService.cs
│   ├── IConnectorService.cs / ConnectorService.cs
│   ├── IFlowService.cs / FlowService.cs
│   ├── IMappingService.cs / MappingService.cs
│   ├── IJobService.cs / JobService.cs
│   └── ILogService.cs / LogService.cs
└── Views/
    ├── Dashboard/Index.cshtml
    ├── Connector/Index.cshtml, Create.cshtml, Edit.cshtml
    ├── Flow/Index.cshtml, Create.cshtml, Edit.cshtml, Details.cshtml
    ├── Mapping/Index.cshtml, Create.cshtml, Edit.cshtml
    ├── Job/Index.cshtml, Details.cshtml
    └── Logs/Index.cshtml, Details.cshtml
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022, VS Code, or JetBrains Rider

### Running the Application

```bash
cd src/UniversalIntegrationMiddleware
dotnet restore
dotnet build
dotnet run
```

The application will start at `http://localhost:5000` (or the configured port).

### API Endpoints

- `GET /Dashboard` - System overview with metrics and recent activity
- `GET /Connector` - List all platform connections
- `GET /Flow` - List all integration flows
- `GET /Mapping` - List all mapping templates
- `GET /Job` - List all job runs with filtering
- `GET /Logs` - View system logs
- `POST /api/webhooks/{platform}` - Receive webhook events from platforms

## Domain Models

### Core Entities

1. **Merchant** - Organization/tenant that owns the integrations
2. **ChannelConnection** - Connection to an e-commerce platform or ERP
3. **IntegrationFlow** - Defines how data flows from source to target
4. **MappingTemplate** - JSON transformation rules for data mapping
5. **Schedule** - Cron expressions or intervals for automated runs
6. **JobRun** - Record of a flow execution
7. **LogEntry** - System and job log entries

### Flow Types

- **Orders** - Sync sales orders between platforms
- **Inventory** - Sync stock levels and availability
- **Products** - Sync product catalog data

## Technology Stack

- **ASP.NET Core 8.0 MVC** - Web framework
- **Bootstrap 5** - UI styling
- **Entity Framework Core** - Data access (ready to add)
- **Background Jobs** - Hangfire/Quartz.NET (ready to integrate)

## Next Steps

This foundation provides the MVC structure for a production-ready middleware. To complete the implementation:

1. Add Entity Framework Core with a database provider
2. Implement actual platform connectors (Shopify API, Amazon SP-API, etc.)
3. Add authentication and authorization
4. Implement background job processing
5. Add health checks and telemetry
6. Configure Docker/Kubernetes deployment

## License

MIT License