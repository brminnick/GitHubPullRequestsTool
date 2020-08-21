using Newtonsoft.Json;

namespace GitHubPullRequestsTool
{
    class RepositoryResponse
    {
        public RepositoryResponse(Repository repository) => Repository = repository;

        [JsonProperty("repository")]
        public Repository Repository { get; }
    }

}
