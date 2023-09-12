start cmd /k "dotnet run --project ./src/API/TermPulse.API/bin/Release/net7.0/TermPulse.API.dll"
start cmd /k "dotnet run --project ./src/WorkerServices/TermPulse.ETL.Worker/bin/Release/net7.0/TermPulse.ETL.Worker.dll"
start cmd /k "dotnet run --project ./src/WorkerServices/TermPulse.ML.Worker/bin/Release/net7.0/TermPulse.ML.Worker.dll"