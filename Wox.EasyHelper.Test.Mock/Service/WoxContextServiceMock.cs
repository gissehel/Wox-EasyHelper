using System;
using System.Collections.Generic;
using Wox.EasyHelper.Core.Service;

namespace Wox.EasyHelper.Test.Mock.Service
{
    public class WoxContextServiceMock : IWoxContextService
    {
        private QueryServiceMock QueryService { get; set; }

        private Dictionary<string, IWoxResultFinder> WoxResultFinderByCommandName { get; set; } = new Dictionary<string, IWoxResultFinder>();

        public WoxContextServiceMock(QueryServiceMock queryService)
        {
            QueryService = queryService;
        }

        public void AddQueryFetcher(string commandName, IWoxResultFinder queryFetcher)
        {
            WoxResultFinderByCommandName[commandName] = queryFetcher;
        }

        public string ActionKeyword { get; set; }

        public string Seperater => " ";

        public string CurrentQuery { get; set; } = "";

        public string IconPath => "This is icon path";

        public void ChangeQuery(string query)
        {
            SetCurrentQuery(query);
        }

        public void SetQueryFromInterface(string query)
        {
            SetCurrentQuery(query);
        }

        private void SetCurrentQuery(string query)
        {
            CurrentQuery = query;
            var woxQuery = QueryService.GetWoxQuery(CurrentQuery);
            ActionKeyword = woxQuery.Command;
            if (WoxResultFinderByCommandName.ContainsKey(woxQuery.Command))
            {
                Results = WoxResultFinderByCommandName[woxQuery.Command].GetResults(woxQuery);
            }
            else
            {
                Results = new List<WoxResult>();
            }
        }

        public WoxResult GetActionResult(string title, string subTitle, Action action) => new WoxResult
        {
            Title = title,
            SubTitle = subTitle,
            Action = () =>
            {
                action();
            },
            ShouldClose = true,
        };

        public WoxResult GetCompletionResult(string title, string subTitle, Func<string> getNewQuery) => new WoxResult
        {
            Title = title,
            SubTitle = subTitle,
            Action = () => ChangeQuery(ActionKeyword + Seperater + getNewQuery() + Seperater),
            ShouldClose = false,
        };

        public WoxResult GetCompletionResultFinal(string title, string subTitle, Func<string> getNewQuery) => new WoxResult
        {
            Title = title,
            SubTitle = subTitle,
            Action = () => ChangeQuery(ActionKeyword + Seperater + getNewQuery()),
            ShouldClose = false,
        };

        public IEnumerable<WoxResult> Results { get; set; } = new List<WoxResult>();

        public bool WoxDisplayed { get; set; } = false;
    }
}