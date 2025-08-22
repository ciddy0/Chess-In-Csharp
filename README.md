# Chess Engine

A comprehensive chess implementation in C# featuring a complete chess board representation, move validation, special moves, game state management, **modern GUI interface**, and **AI** opponent using **Minimax algorithm**. Thoroughly tested with **300+ automated test cases** using GitHub Actions CI/CD pipeline.

## Features

### Core Chess Functionality
- **Complete Chess Rules Implementation**: Full support all standard chess rules and piece movements
- **Special Moves Support**:
  - Castling
  - En Passant captures
  - Pawn promotion
- **Game State Detection**
  - Check detection
  - Checkmate detection
  - Stalement detection
  - Draw conditions (50-move rule)

 ### User Interface & Experience
 - **Cross-platform GUI**: Built using Avalonia UI framework for Windows, macOS, and Linux
 - **Interactive Chess Board**: click to move interface with visual feedback
 - **Real-Time Game State visualization**:
   - Square highlighting for possible moves
   - Selected piece indication
   - King in check visual warning
   - Point advantage display
 - **AI button**: Able to use AI to help determine next move
 - **Undo**: full undo functionality with visual state updates

### Artificial Intelligence

- **Minimax Algorithm Implementation**: Strategic AI opponent that evaluates positions
- **Asynchronous AI Processing**: makes sure the application does not crash

## Technical Architecture

### MVVM Pattern Implementation

The GUI follows the Model-View-ViewModel pattern:
- **Model**: ChessBoard - Pure game logic and state
- **View**: Avalonia UI components - Visual representation
- **ViewModel**: ChessViewModel - UI binding and interaction logic

### Strategy Pattern

Each piece type implements its own movement strategy:

- ```PawnStrategy```
- ```RookStrategy```
- ```KnightStrategy```
- ```BishopStrategy```
- ```QueenStrategy```
- ```KingStrategy```

### Move Types

- ```Normal```: Standard piece movements
- ```CastleKingSide```/```CastleQueenSide```: Castling moves
- ```PawnPromote```: Pawn promotion with piece selection
- ```EnPassant```: En passant captures

## Quality Assurance

### Comprehensive Testing Suite

- **300+ Automated Test Cases**: Extensive test coverage ensuring reliability and correctness
- **GitHub Actions CI/CD**: Continuous integration pipeline running all tests on every commit
- **Test Categories**:
  - Unit tests for individual piece movements
  - Integration tests for complex game scenarios
  - Edge case validation (castling conditions, en passant timing, etc.)
  - Game state detection accuracy (checkmate, stalemate, draw conditions)
  - Move validation and legal move generation
  - Board state integrity after move application/undo

## Technical Highlights

## Efficient Board Representation

```c#
private byte[,] board = new byte[BoardSize, 4];
```
The board uses a compact 4-bit representation per square, storing both piece type and player information efficiently.

## Contributing
This chess engine demonstrates advanced C# programming concepts including:

- Strategy design pattern for piece movements
- MVVM architectural pattern for GUI applications
- Efficient data structures and memory optimization
- Complex game logic implementation with comprehensive testing
- Asynchronous programming for responsive UI
- Cross-platform application development with Avalonia
- AI algorithm implementation (Minimax)
- Observable pattern for real-time UI updates
