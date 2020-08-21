using Newtonsoft.Json;

namespace GitHubPullRequestsTool
{
    public class GitHubToken
    {
        public GitHubToken(string access_token, string token_type) =>
            (AccessToken, TokenType) = (access_token, token_type);

        public static GitHubToken Empty { get; } = new GitHubToken(string.Empty, string.Empty, string.Empty);

        [JsonProperty("access_token")]
        public string AccessToken { get; }

        [JsonProperty("token_type")]
        public string TokenType { get; }
    }

    public static class GitHubTokenExtensions
    {
        public static bool IsEmpty(this GitHubToken gitHubToken) =>
            gitHubToken.AccessToken == string.Empty && gitHubToken.TokenType == string.Empty;
    }
}
