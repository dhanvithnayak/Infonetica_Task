<div id="top"></div>

<!-- UPDATE -->
<div align="center">
  <a href="">
     <img width="140" alt="image" src="">
  </a>

  <h3 align="center">Dynamic Workflows</h3>

  <p align="center">
  <!-- UPDATE -->
    <i>A state-machine based, minimal API, configurable workflow engine</i>
    <br />
  </p>
</div>


<!-- TABLE OF CONTENTS -->
<details>
<summary>Table of Contents</summary>

- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)

</details>



## Getting Started

To set up a local instance of the application, follow the steps below.

### Prerequisites
The following dependencies are required to be installed for the project to function properly:
* .NET 8 SDK
  
  ```
  dotnet --version
  ```

<p align="right">(<a href="#top">back to top</a>)</p>

### Installation

1. Clone the repository

   ```
   git clone https://github.com/dhanvithnayak/Infonetica_Task.git
   ```

2. Run local instance
   
   ```
   cd Infonetica_Task/src
   dotnet run
   ```

Or if you prefer to use Visual Studio, run `DynamicWorkflow.sln` directly

<p align="right">(<a href="#top">back to top</a>)</p>


## Usage
You can call methods directly from the browser, for example

```
localhost:5000/workflow/5
```

Or Swagger UI at

```
localhost:5000/swagger
```

<div align="center">
  <img width="80%" alt="image" src="https://github.com/user-attachments/assets/238fcf31-8a25-4ab2-9992-fb1987e1ba0a">
</div>

## Endpoints

1. `POST /workflow`: Create new workflow definition
   
    Below is a demo payload
   ```json
   {
     "id": "1",
     "states": [
       {
         "id": "1",
         "isInitial": true,
         "actions": [
           { "id": "1", "targetState": "INIT_STATE" }
          ]
        },
       {
         "id": "2",
         "actions": [
           { "id": "2", "targetState": "STATE2" },
           { "id": "3", "targetState": "STATE3" }
         ]
       },
         { "id": "3", "isFinal": true },
         { "id": "4", "isFinal": true }
     ]
   }
    ```
2. `GET /workflow/{id}`: Get workflow details given definition ID
3. `POST /workflow/{id}/start`: Starts instance of given definition ID
4. `GET /instance/{id}`: Get instance details given instance ID
5. `POST /instance/{id}/action`: Run an action on given instance ID

<p align="right">(<a href="#top">back to top</a>)</p>
