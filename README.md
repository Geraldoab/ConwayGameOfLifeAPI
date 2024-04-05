# Conway's Game Of Life API
### https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
An implementation of Conway's Game of Life using C# and .NET 7.0

- DDD
- SOLID
- SWAGGER
- Strategy Pattern
- Unit Tests
- Integration Tests (Controller > Service -> Repository -> In-Memory Database)
- Local Response Cache
- Filters
- In-Memory Database
- Generics

## How to improve performance

- Change ```int[,]``` to ```byte[,]```
- To flatten all two-dimensional arrays

## Demonstration
![GameOfLifeDemostration](https://github.com/Geraldoab/ConwayGameOfLifeAPI/assets/3846304/9fabb8d4-71c3-4e9d-8390-cae2f8f23e5c)

![image](https://github.com/Geraldoab/ConwayGameOfLifeAPI/assets/3846304/fd215ee1-4fa8-47dc-ac7a-0f345b6ff6ec)

![Screenshot 2024-04-02 211259](https://github.com/Geraldoab/ConwayGameOfLifeAPI/assets/3846304/fcd5a66c-57cc-4181-8928-5a0971742ad7)


## Swagger

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


