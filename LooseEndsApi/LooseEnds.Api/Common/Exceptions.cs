namespace LooseEnds.Api.Common;

public static class GameExceptions
{
    public static NotFoundException GameNotFound(string gameCode) => new NotFoundException($"Couldn't find game with code {gameCode}");
    public static GameStartedException AlreadyStarted(string gameCode) => new GameStartedException($"Game with code {gameCode} has already started");
    public static ThreePlayersRequiredException ThreeRequired() => new ThreePlayersRequiredException($"Three players are required before starting the game");
    public static InvalidAnswer InvalidAnswer() => new InvalidAnswer($"You cannot answer this prompt");
    public static InvalidVote InvalidVote() => new InvalidVote($"You cannot vote on this response");
}

public class NotFoundException(string message) : Exception(message) { }
public class GameStartedException(string message) : Exception(message) { }
public class ThreePlayersRequiredException(string message): Exception(message) { }
public class InvalidAnswer(string message): Exception(message) { }
public class InvalidVote(string message): Exception(message) { }
