# TicTacToeApi

Via API, the player can create a new game, connect \ find it, finish the game and make moves. Users doesn't need to register, server remembers them via ip.
I'm using MS SQL

## Create Game

POST 'api/TicTacToe' takes size of the field square (1 side) and first player's value. Creates table, points, and returns NextMove object.

## Find Your Game

*GET 'api/TicTacToe'* finds a game, which you was playing, searching for your ip address.

## End Game

*DELETE 'api/TicTacToe/{gameId}'* deletes game with given id from database

## Get info about game

*GET 'api/TicTacToe/{gameId}'* gives you full info (GameInfo object) about game with given id

## Connect to game

*PUT 'api/TicTacToe/{gameId}'* takes your ip address and puts as the second player of game with given id


## Connect to game

*POST 'api/TicTacToe/{gameId}/move'* takes position of point, where user wants to set circle or cross and updates point with that position. Returns NextMove object

## Objects

### PointValue
- Enum of pont values: Emty, Cross, Circle

### NextMove
- gameId: int
- nextValue: PointValue 
- gameEnd: bool
- winner: PointValue

### GameInfo
- gameId: int
- nextValue: PointValue 
- player1: string (ip)
- player2: string (ip)
- player1Value: PointValue
- player2Value: PointValue (opposite of player1Value)
- winner: PointValue
- table: Table 

### Table
- size: int (1 side)
- unusedPoints: int
- points: Point[]

### Point
- X: int
- Y: int
- value: PointValue
