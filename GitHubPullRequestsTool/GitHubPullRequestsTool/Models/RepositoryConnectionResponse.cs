using Newtonsoft.Json;

namespace GitHubPullRequestsTool
{
     class RepositoryConnectionResponse
    {
        public RepositoryConnectionResponse(User user) => GitHubUser = user;

        [JsonProperty("user")]
        public User GitHubUser { get; }
    }
}
