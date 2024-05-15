# Telematics Data Processor

This project is a .NET application that processes telematics data. I'm writing this to show some level of competency in programming. I didn't know .NET a couple hours ago. Now I do. There is some light paralaellization with some multithreading but nothing too intense. I am hoping this does enough to get my foot in the door. I'm also writing up a blog about this and how I went through it. Check it out here: https://mustafa-tariqk.github.io/jekyll/update/2024/05/14/Hello-Geotab.html

## Project Structure

- `src/`: Contains the source code for the application.
  - `DataProcessor.cs`: The main data processing logic.
  - `Program.cs`: The entry point for the application.
  - `Reporter.cs`: Handles reporting of the processed data.
- `tests/`: Contains the unit tests for the application.
- `.gitignore`: Specifies which files and directories to ignore in Git.

## Building the Project

To build the project, navigate to src and use the .NET CLI:

```
dotnet build
dotnet run
```
## Running the Tests
To run the tests, navigate to tests and use the .NET CLI:
```
dotnet build
dotnet test
```
