# KML File Creator Console Application Documentation

This documentation outlines the steps to create a C# .NET Framework 4.6 console application that generates KML files from comma-separated latitude and longitude data. The application will utilize the SharpKML library and AspMapNET.dll.

## Prerequisites

1. **Development Environment**: Visual Studio or any C# IDE.
2. **NuGet Packages**:
   - SharpKml 1.1.0
   - SharpKml.Core 5.2.0
3. **Additional DLL**:
   - AspMapNET.dll

## Setting Up the Project

### Step 1: Create the Console Application

1. Open Visual Studio.
2. Create a new project: `File -> New -> Project`.
3. Select `Console App (.NET Framework)`.
4. Name your project and choose the .NET Framework version 4.6.

### Step 2: Add NuGet Packages

1. Right-click on the project in the Solution Explorer.
2. Select `Manage NuGet Packages`.
3. Search for `SharpKml` and install version 1.1.0.
4. Search for `SharpKml.Core` and install version 5.2.0.

### Step 3: Add AspMapNET.dll

1. Download `AspMapNET.dll` from the appropriate source.
2. Right-click on the project and select `Add -> Reference`.
3. Click `Browse` and navigate to the location of `AspMapNET.dll`.
4. Select the DLL and click `Add`.


## Explanation

1. **local method Reading**:
   - The LatLonData.Data() method reads  the latitude and longitude values.
   - Ensure your latitude and longitude separated by a comma.

2. **KML File Creation**:
   - The `CreateKmlFile` method creates a KML document and adds placemarks for each coordinate.
   - The SharpKml library is used to construct and save the KML file.


## Running the Application

1. Place your latitude and longitude data in LatLonData.Data() method.
3. Build and run the application.
4. The KML file will be created at the specified output path.

## Conclusion

This application provides a simple way to convert latitude and longitude data from a local string into a KML file. By following the steps and using the provided code, you can easily generate KML files for use with mapping applications.
