using LooseEnds.Api.Dtos.Rounds;

public record VotingStartedDto(int Number, int PromptId, DateTime VoteDueUtc, IEnumerable<VoteOptionDto> Options);