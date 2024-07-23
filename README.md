# Telematics Data Processor

This project is a .NET application that processes telematics data. I'm writing this to show some level of competency in programming. I didn't know .NET a couple hours ago. Now I do. There is some light paralaellization with some multithreading but nothing too intense. I am hoping this does enough to get my foot in the door. I'm also writing up a blog about this and how I went through it. ~~Check it out here: https://mustafa-tariqk.github.io/jekyll/update/2024/05/14/Hello-Geotab.html~~ Blog is down. Copy pasting down below

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


## Blog contents

### 8:10PM
 I'm going to live blog my application to Geotab. I really like this company for a couple reasons: 
 1. Efficient transportation means a greener planet. Growing up in Kuwait where [climate change is hitting hard](https://youtu.be/UbYCEjbWBFs) this is something that's very important to me, and the opportunity to work on this is exciting.
 2. [Geotab just won it's 7th best managed companies award](https://www.geotab.com/press-release/2024-canada-best-managed-companies/) and the culture of trust, continuous learning and teamwork is something I strive for.
 3. Office in Oakville, I live here so it's awesome that I'd get to work in person not too far away.

 Given these 3 things, I don't want to just apply and leave things to chance. I'm going to work on a project based on the job description.
 # Write up
 Link: (https://github.com/mustafa-tariqk/TelematicsDataProcessor)[https://github.com/mustafa-tariqk/TelematicsDataProcessor]

 In this take-home project, I've demonstrated the following:
 - Successfully passing all tests and delivering a fully functional solution
 - Implementing multithreading capabilities, showcasing advanced technical skills

 Though there are areas for improvement, including enhancing error handling and transitioning to AWS hosting. Additionally, while utilizing a CSV for data storage served the project's needs, integrating with Postgres would have elevated its robustness.

 Reflecting on this experience, I've leveraged it to master C#. If given the opportunity to revisit this project, I'm confident it would reach even greater heights.

 **High Level Overview:**
 I read from a csv with artificial delay to simulate data streaming, then concurrently take any new lines given and process it into an effective data structure.

 A live terminal app is being shown while this is happening, users can exit or generate a report. If they generate a report this data structure is queried to generate some metrics about the fleet. If I were to redo this I'd make a couple changes: Metrics/reports would be generated live alongside all new data coming in no calculations wouldn't happen twice if reran, the data structure would look a little different, with a hash table with a couple tweaks:

 {vehicle_id: tree}

 tree would be a binary tree that contains log(n) lookup and log(n) insertion. As data being streamed in is not sorted by time, having this would increase efficiency, however lookups would get a little bit more expensive if you wanted a specific element in the middle. I think this is a worthy tradeoff.

 This tree would have timestamp as the value it's organized by, with all other values being a dictionary within each node.
 ## Rambling of a mad man:
 ### 8:27 PM
 I know 90% of what's on the job description. But I don't know C#, I'm going to learn it right now as I make a telematics data processor. It's going to be a simple console app in .NET Core that processes telematics data (e.g. vehicle location, speed, acceleration) and makes reports on performance metrics. I'm gonna have the job description out at all times to make sure I can fit stuff in that shows my competence.

 ### 8:50PM
 Learnt a bit about C# from some reading, looks close enough to Java so this shouldn't be hard at all. I fear I may get tripped up in something simple though. We'll see, I have a project structure down. I need to create a csv schema for how example data will look like, generate a ton of test data, and then create a file to process all the data, storing it in what will probably be a list of objects.
 ### 9:32PM
 I have an example csv schema, I'm working on storing incoming data effectively such that useful lookups can be made easily, and working on concurrency.
 ### 10:41PM
 I have data being stored properly, I am going to move focus to generating reports now. Then adding concurrency, then testing + debugging.
 ### 11:49PM
 Had to get my little brother from the train station, getting back at the reporter.
 ## 1:29AM
 Concurrency implemented, I'm moving on to testing. Then I'll do a write up, and submit my application.
 ## 2:23AM
 So many bugs.... I am complete however and will be doing a write up now.
