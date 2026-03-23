using LooseEnds.Api.Dtos.Rounds;
using LooseEnds.Api.Dtos.Sessions;

namespace LooseEnds.Api.Data;

public static class Mock
{
    public static SessionStateDto HostPromptStart() => new SessionStateDto
    {
        GameCode = "MHPS",
        DateCreatedUtc = DateTime.UtcNow,
        IsHost = true,
        UserId = "1",
        PromptingDuration = 60,
        VotingDuration = 60,
        Players = [new("1", "P1", 0)],
        Rounds = [
            new RoundDto {
                Number = 1,
                AnswerDueUtc = DateTime.UtcNow.AddSeconds(60),
                PromptingCompleted = false,
                ActiveVotingPromptId = null,
                VotingCompleted = false,
                Prompts = [
                    new PromptDto {
                        Id = 1,
                        Prompt = "P1",
                        VoteDueUtc = null,
                        IsCompleted = false,
                        VoteOptions = [
                            new VoteOptionDto {
                                PlayerId = "1",
                                ResponseId = 1
                            }
                        ]
                    }
                ]
            }
        ]
    };
}
