# Valet Key Upload
This is a demo project that allows async multiple uploads to Azure BLOB Storage using the Valet Key Design Pattern. Read my blog at [hexmaster.nl](https://hexmaster.nl/article/valet-key-pattern-in-azure) for more information about the background of the Valet Key Design Pattern.

About this project, the backend is written in ASP.NET Core 2.2 (C#) and Visual Studio 2019. The backend connects to a storage account (set the connection string in appsettings.json to your own storage account). The API contains one endpoint and generates a Secure Access Token to a blob on Azure BLOB Storage.

The front-end displays a file select box (multi-select is allowed by-the-way). For each file selecter, it will call the backend API and request access to 'a' BLOB. Then the front-end will upload the selected files directly to Azure BLOB storage.

The front-end system is written using Angular (8). Note you're not tied to Angular, it's just a nice way to show how to upload files to BLOB storage directly.

Note I used some code from [Stuart](https://github.com/stottle-uk/stottle-angular-blob-storage-upload) to help me get the actual upload working.

In order to run the front-end project, download and install NodeJS (10).

Open a console window navigate to the front-end project (containing package.json) and type
```
    npm i @angular/cli -g
    npm i
    ng serve -o
```
