# TermPulse Solution README

## Prerequisites

- **Visual Studio 2022 Community Edition**: Ensure you have Visual Studio 2022 Community Edition installed. If not, download it from the official Microsoft website.

## Installation Steps

1. **Installing Visual Studio with Required Features**:
    - During the installation of Visual Studio 2022 Community Edition, ensure to select the following components:
        * .NET Web tools
        * ML tools
        * .NET Desktop
        
    > After installation, you're ready to open and run the TermPulse solution.

2. **Opening the Solution**:
    - Navigate to the directory containing the `TermPulse.sln` file.
    - Double-click on `TermPulse.sln` to open with Visual Studio 2022.

3. **Setting up Startup Projects**:
    - In the Solution Explorer pane, right-click on the `Solution 'TermPulse' (X projects)` item.
    - Choose `Set StartUp Projects`.
    - Select 'Multiple startup projects'.
    - Ensure the following projects are set to 'Start':
        * TermPulse.API
        * TermPulse.ETL.Worker
        * TermPulse.ML.Worker

4. **Running the Solution**:
    - Press `F5` or click the 'Start' button in Visual Studio to run the solution.
    
    > Once the projects are up and running, a web browser will automatically open, presenting the Swagger interface showcasing a list of available APIs.

5. **Testing the Solution**:
    - On the Swagger interface, select the first API.
    - Execute with your desired query.
    
6. **Viewing the Results**:
    - Open the provided Power BI file for results visualization.
    - On the first page, select your search term for detailed insights.

---

Enjoy exploring the APIs and using the Power BI file for deeper analysis!
