namespace LooseEnds.Api.Dtos.Rounds;

public record AnswerPromptRequest(string gameCode, int PlayerResponseId, int PlayerId, string Answer);
