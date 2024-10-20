public record ChatGtpResponse(
    string Id,
    string Object,
    long Created,
    string Model,
    Choice[] Choices,
    Usage Usage,
    string SystemFingerprint
);

public record Choice(
    int Index,
    Message Message,
    object? Logprobs,
    string FinishReason
);

public record Message(
    string Role,
    string Content,
    object? Refusal
);

public record Usage(
    int PromptTokens,
    int CompletionTokens,
    int TotalTokens,
    TokenDetails PromptTokensDetails,
    TokenDetails CompletionTokensDetails
);

public record TokenDetails(
    int CachedTokens,
    int? ReasoningTokens = null
);