# :globe_with_meridians: Conway's Game Of Life API
### https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
An implementation of Conway's Game of Life using C# and .NET 7.0

- :white_check_mark: Docker
- :white_check_mark: DDD
- :white_check_mark: SOLID
- :white_check_mark: SWAGGER
- :white_check_mark: Strategy Pattern
- :white_check_mark:  Unit Tests
- :white_check_mark: Integration Tests (Controller > Service -> Repository -> In-Memory Database)
- :white_check_mark: Local Response Cache
- :white_check_mark: Filters
- :white_check_mark: Exception Middleware
- :white_check_mark: In-Memory Database
- :white_check_mark: Generics

## :recycle: How to improve Performance

- Change ```int[,]``` to ```byte[,]```
- To flatten all two-dimensional arrays (Before changing something we need to measure)
- By changing from Relational Database to Document Database

## :mag_right: How to improve Observability

Every Microservice needs to have a good observability tool connected to it. The following tools can be used:

- AWS CloudWatch
- Splunk
- Elastic Search + Kibana

## :whale2: How to use with Docker

[My Docker Hub](https://hub.docker.com/repository/docker/geraldoab/game-of-life-api)

``` git clone https://github.com/Geraldoab/ConwayGameOfLifeAPI.git ```

``` cd ConwayGameOfLifeAPI ```

``` docker-compose up ```

``` http://localhost:5900/swagger/index.html ```

### You also can create your own Docker Image

``` docker build -t your-name/name-of-your-image:1.0 . ```

## :camera: Demonstration
![GameOfLifeDemostration](https://github.com/Geraldoab/ConwayGameOfLifeAPI/assets/3846304/9fabb8d4-71c3-4e9d-8390-cae2f8f23e5c)

![image](https://github.com/Geraldoab/ConwayGameOfLifeAPI/assets/3846304/fd215ee1-4fa8-47dc-ac7a-0f345b6ff6ec)

![Screenshot 2024-04-02 211259](https://github.com/Geraldoab/ConwayGameOfLifeAPI/assets/3846304/fcd5a66c-57cc-4181-8928-5a0971742ad7)


## :page_facing_up: Swagger

### Json sample:
``` 
{
  "board": {
    "grid": {
      "width": 10,
      "height": 10,
      "cells": [
		[0, 0, 0, 1, 0, 0, 0, 0, 0, 0],
		[1, 0, 0, 0, 0, 1, 0, 0, 1, 0],
		[0, 0, 0, 0, 0, 0, 0, 1, 0, 0],
		[0, 1, 0, 0, 0, 1, 0, 0, 0, 0],
		[0, 0, 0, 0, 0, 0, 1, 0, 0, 1],
		[0, 1, 0, 1, 0, 0, 0, 0, 0, 0],
		[0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
		[0, 0, 0, 1, 0, 0, 0, 0, 0, 0],
		[1, 0, 0, 0, 1, 0, 0, 0, 1, 0],
		[0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
	  ]
    }
  }
}
```

![image](https://github.com/Geraldoab/ConwayGameOfLifeAPI/assets/3846304/9945cc72-52d2-41ce-b4ad-ea2fe46c8dd6)


