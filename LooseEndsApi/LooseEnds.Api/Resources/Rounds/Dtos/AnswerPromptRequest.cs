namespace LooseEnds.Api.Resources.Rounds.Dtos;

public record AnswerPromptRequest(string gameCode, int PlayerResponseId, int PlayerId, string Answer);
