# Battleship Game

Welcome to the Battleship game repository! This project is a console-based implementation of the classic game Battleship, 
where two players strategically place their ships and take turns firing at each other's fleets until one emerges victorious.


## Features

- **Turn-Based Gameplay**: Players alternate turns, firing shots at their opponent's grid.
- **Ship Placement**: Players can place their ships on a grid according to the rules of Battleship.
- **Interface-Driven Design**: Implements clean architecture with the `IPlayer` interface for flexibility and expandability.

## Technologies Used

- **C#**: The primary programming language.
- **.NET**: Framework for building and running the application.
- **OOP Principles**: Object-oriented programming used for modular and reusable code.

## How to Play

1. **Start the Game**: Launch the application from your console or IDE.
2. **Place Ships**: Players place their ships on the grid, adhering to Battleship rules.
3. **Take Turns**: Players alternate turns, inputting coordinates to fire at the opponent's grid.
4. **Win Condition**: The first player to sink all of their opponent's ships wins the game.

## Code Structure

- **`Battleship.UI.Interfaces`**: Contains the `IPlayer` interface, defining the contract for player implementations.
- **`Battleship.UI.GameLogic`**: Contains the game logic and core functionality.
- **`BattleShip.Tests`**: Includes the `TestPlayer` class for testing scenarios and mock implementations.


## Key Classes and Interfaces

### `IPlayer`
Defines the contract for player implementations:
- Properties:
  - `Ship[] PlayerShips`
  - `string[] shotsTaken`
  - `int turnNumber`
  - `string Name`
  - `string lastShot`
- Methods:
  - `string[] PlaceShips(int shipNumber)`
  - `string getMove(IPlayer opp, Ship[] oppShips)`

### `TestPlayer`
A test implementation of `IPlayer` for automated testing and debugging.
- Includes functionality for manual or random shot selection.

## Planned Features

- **Graphical Interface**: Add a graphical user interface for enhanced user experience.
- **Enhanced AI**: Implement smarter AI opponents with varying difficulty levels.
- **Multiplayer**: Support for online or local multiplayer modes.

---
