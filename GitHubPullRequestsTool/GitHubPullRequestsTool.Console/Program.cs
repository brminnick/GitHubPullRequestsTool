using System;
using System.Threading;
using System.Threading.Tasks;

namespace GitHubPullRequestsTool.Console
{
    class Program
    {
        //Create a GitHub Token: https://help.github.com/articles/creating-a-personal-access-token-for-the-command-line/#creating-a-token
        readonly static string _token = Environment.GetEnvironmentVariable("GitHubToken") ?? throw new Exception("GitHub Token Missing");

        static async Task Main(string[] args)
        {
            var gitHubGraphQLApiService = new GitHubGraphQLApiService();
            var githubToken = new GitHubToken(_token, "bearer");

            //Get All Repositories
            await foreach (var repositories in gitHubGraphQLApiService.GetRepositories("brminnick", githubToken, CancellationToken.None).ConfigureAwait(false))
            {
                foreach (var repository in repositories)
                {
                    System.Console.WriteLine(repository);
                }
            }

            //Get One Repository
            var gitTrendsRepository = await gitHubGraphQLApiService.GetRepository("brminnick", "GitTrends", githubToken, CancellationToken.None).ConfigureAwait(false);
            System.Console.WriteLine(gitTrendsRepository);
        }
    }
}
