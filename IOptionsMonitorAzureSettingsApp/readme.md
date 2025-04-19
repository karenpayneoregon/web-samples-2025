# About this project

<!--TOC-->
  - [Features](#features)
  - [Project Structure](#project-structure)
  - [Usage](#usage)
    - [AzureService](#azureservice)
      - [Key Methods](#key-methods)
      - [Events](#events)
  - [Documentation](#documentation)
  - [Index1 page](#index1-page)
<!--/TOC-->

IOptionsMonitorAzureSettingsApp is a Razor Pages project designed to demonstrate the use of `IOptionsMonitor<T>` for managing and monitoring Azure configuration settings in real-time. This project provides a robust and scalable way to handle dynamic configuration updates for Azure services.

## Features

- **Real-Time Configuration Updates**: Automatically detects and handles changes to Azure settings using `IOptionsMonitor<T>`.
- **Named Options Support**: Supports both default and named options for managing multiple configurations.
- **Event-Driven Notifications**: Notifies subscribers when configuration changes occur.
- **Thread-Safe Storage**: Uses `ConcurrentDictionary` to store and manage the last known configuration values.

## Project Structure

- **Models**
  - `AzureSettings`: Represents the Azure configuration settings model.
- **Services**
  - `AzureService`: A service that monitors and handles changes to Azure settings.
- **Pages**
  - Razor Pages for demonstrating the functionality of the service.


   
## Usage

### AzureService

The `AzureService` class is the core of this project. It monitors changes to Azure settings and provides methods to retrieve the current configuration.

#### Key Methods

- `GetDefaultSettings()`: Retrieves the default Azure settings.
- `GetNamedSettings()`: Retrieves the named Azure settings (e.g., "TenantName").

#### Events

- `OnSettingsChanged`: An event that is triggered when configuration changes are detected.



## Documentation

Created using `GitHub Copilot` with the following prompt:


_Using GitHub markdown for a readme.md file document the project IOptionsMonitorAzureSettingsApp_

## Index1 page

The majority of the code for Index1 page was written use AI then modified by Karen.

The code for Index1 page is a model for use in a real application, just needs `validation` and `error handling`.

Recommend taking time to run through the code and understand it by setting breakpoints as the code is rather complex.

In `SettingsMonitorService.ComputeSnapshot` ensures that rather than code in `AzureWorker.OnSettingsChanged` is trigger once rather than multiple times which is how `IOptionsMonitor<T>.OnChange(...)` works.

The following code ensures that the `OnChange` event is only triggered once when the settings are changed:

```csharp
// Only invoke if the snapshot really changed
if (newSnapshot != _lastSnapshot)
{
	_current = updated;
	_lastSnapshot = newSnapshot;
	SettingsChanged?.Invoke(_current);
}
```





