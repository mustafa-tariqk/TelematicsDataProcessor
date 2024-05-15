**Project Idea:** "Telematics Data Processor"

Create a simple console application in .NET Core that processes telematics data (e.g., vehicle location, speed, acceleration) and generates reports on
performance metrics. The app should have the following features:

1. **Data Processing**: Read CSV files containing telematics data and process it to extract relevant information.
2. **Reporting**: Generate simple reports (e.g., HTML or CSV files) that display performance metrics such as average speed, acceleration, and
location-based statistics.
3. **Automated Testing**: Write unit tests using a testing framework like NUnit or xUnit to verify the correctness of the data processing and reporting
functions.

**Project File Structure:**

Create the following folders and files:

* `TelematicsDataProcessor` (root folder)
	+ `src` (source code folder)
		- `TelematicsDataProcessor.csproj` (project file for .NET Core)
		- `Program.cs` (entry point for the console app)
		- `DataProcessor.cs` (data processing logic)
		- `Reporter.cs` (reporting logic)
	+ `tests` (testing folder)
		- `TelematicsDataProcessor.Tests.csproj` (project file for unit tests)
		- `Tests.cs` (unit test class)

**How to Complete the Project:**

1. Set up a new .NET Core console application project using your preferred IDE or the command line.
2. Implement the data processing logic in `DataProcessor.cs`. This will involve reading CSV files, parsing the data, and storing it in a suitable data
structure (e.g., a list of objects).
3. Create a simple reporting function in `Reporter.cs` that generates reports based on the processed data. You can use libraries like EPPlus or
CsvHelper to generate reports.
4. Write unit tests for the data processing and reporting functions using NUnit or xUnit. These tests should verify the correctness of the processed
data and generated reports.
5. Implement automated testing by writing test methods in a separate file (e.g., `Tests.cs`) that cover various scenarios, such as:
	* Validating the processed data against expected results
	* Verifying the report generation for different input data
6. Run your unit tests to ensure they pass.

**Tips and Considerations:**

* Keep your project simple and focused on showcasing your skills in automated testing and CI/CD pipeline management.
* Use a simple console application as the basis for your project, rather than trying to create a full-fledged web-based platform.
* Consider using a CI/CD tool like Azure DevOps or Jenkins to automate your testing and deployment processes.
* Make sure to include a README file in your project that explains how to run the tests and any dependencies required.

By completing this project, you'll demonstrate your skills in automated testing, data processing, and reporting, making it a great fit for the job
description. Good luck with your application!