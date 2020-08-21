using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Refit;

namespace GitHubPullRequestsTool
{
    public class GitHubGraphQLApiService
    {
        readonly static Lazy<IGitHubGraphQLApi> _githubApiClientHolder = new Lazy<IGitHubGraphQLApi>(() => RestService.For<IGitHubGraphQLApi>(new HttpClient { BaseAddress = new Uri(GitHubConstants.GitHubGraphQLApi) }));

        static IGitHubGraphQLApi GitHubApiClient => _githubApiClientHolder.Value;

        public async Task<Repository> GetRepository(string repositoryOwner, string repositoryName, GitHubToken authorizationToken, CancellationToken cancellationToken)
        {
            var response = await GitHubApiClient.RepositoryQuery(new RepositoryQueryContent(repositoryOwner, repositoryName), GetGitHubBearerTokenHeader(authorizationToken), cancellationToken).ConfigureAwait(false);

            return response.Data.Repository;
        }

        public async IAsyncEnumerable<IEnumerable<Repository>> GetRepositories(string repositoryOwner, GitHubToken authorizationToken, [EnumeratorCancellation] CancellationToken cancellationToken, int numberOfRepositoriesPerRequest = 100)
        {
            RepositoryConnection? repositoryConnection = null;

            do
            {
                repositoryConnection = await GetRepositoryConnection(repositoryOwner, repositoryConnection?.PageInfo?.EndCursor, authorizationToken, cancellationToken, numberOfRepositoriesPerRequest).ConfigureAwait(false);
                yield return repositoryConnection?.RepositoryList ?? Enumerable.Empty<Repository>();
            }
            while (repositoryConnection?.PageInfo?.HasNextPage is true);
        }

        static string GetGitHubBearerTokenHeader(in GitHubToken token) => $"{token.TokenType} {token.AccessToken}";

        static async Task<RepositoryConnection?> GetRepositoryConnection(string repositoryOwner, string? endCursor, GitHubToken authorizationToken, CancellationToken cancellationToken, int numberOfRepositoriesPerRequest)
        {
            var response = await GitHubApiClient.RepositoryConnectionQuery(new RepositoryConnectionQueryContent(repositoryOwner, getEndCursorString(endCursor), numberOfRepositoriesPerRequest), GetGitHubBearerTokenHeader(authorizationToken), cancellationToken).ConfigureAwait(false);

            return response.Data.GitHubUser.RepositoryConnection;

            static string getEndCursorString(string? endCursor) => string.IsNullOrWhiteSpace(endCursor) ? string.Empty : "after: \"" + endCursor + "\"";
        }
    }
}
