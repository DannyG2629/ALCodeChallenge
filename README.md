# ALCodeChallenge
This repository contains an application which will query the Stack Overflow API and allow users to select from a list of recent questions with multiple answers and one accepted answer. Users will then guess the accepted answer from a randomly sorted list of answers and be notified if the selected answer was the accepted answer or not.


Development Environment
    - Node version: v14.15.1
    - NPM version: 6.14.8
    - Git version: 2.29.2.windows.2

    - Vue CLI: 4.5.9
        - npm install -g @vue/cli

    - Visual Studio 2019
        - Installed Workloads/Features
            - ASP.NET and Web development
            - Node.js development (Required for Vue template)

ALCodeChallenge.Vue
    - Basic Vue.js Web app w/JavaScript in VS2019

    - NPM installations    
        - Bootstrap: 4.5.3
        - BootstrapVue: 2.21.0
        - Axios: 0.21.0
        - Vue: 2.5.17

ALCodeChallenge.Web
    - MVC Core 3.1
    - Microsoft.AspNetCore.SpaServices.Extensions: 3.1.10

Test projects
    - xUnit 2.4.1
    - Moq 4.15.2

- Used gitignore.io to modify gitignore file

To run this application locally(dev):

1. Open a command line. Go to the folder location of ALCodeChallenge.Vue.njsproj
2. Run 'npm run serve' to start the local dev server. 'npm install' may be required if any of the prerequisites listed above are missing.
3. Ensure that the ALCodeChallenge.Web project is configured to point at the dev server from step 2.
	- Startup.cs -> Line 59. Currently the localhost is setup for port 8080. This port needs to match the one that the server from step 2 is running on.
4. Start the web app in Visual Studio. The single page for the application should open in a new browser window.