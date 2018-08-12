using System;
using System.Collections.Generic;
using Wox.EasyHelper.Core.Service;

namespace Wox.EasyHelper
{
    public abstract class WoxResultFinderBase : IWoxResultFinder
    {
        protected IWoxContextService WoxContextService { get; set; }

        public WoxResultFinderBase(IWoxContextService woxContextService)
        {
            WoxContextService = woxContextService;
        }

        protected WoxResult GetActionResult(string title, string subTitle, Action action) => new WoxResult
        {
            Title = title,
            SubTitle = subTitle,
            Action = () =>
            {
                action();
                // ChangeQuery("");
            },
            ShouldClose = true,
        };

        protected WoxResult GetCompletionResult(string title, string subTitle, Func<string> getNewQuery) => new WoxResult
        {
            Title = title,
            SubTitle = subTitle,
            Action = () => WoxContextService.ChangeQuery(WoxContextService.ActionKeyword + WoxContextService.Seperater + getNewQuery() + WoxContextService.Seperater),
            ShouldClose = false,
        };

        protected WoxResult GetCompletionResultFinal(string title, string subTitle, Func<string> getNewQuery) => new WoxResult
        {
            Title = title,
            SubTitle = subTitle,
            Action = () => WoxContextService.ChangeQuery(WoxContextService.ActionKeyword + WoxContextService.Seperater + getNewQuery()),
            ShouldClose = false,
        };

        public abstract IEnumerable<WoxResult> GetResults(WoxQuery woxQuery);
    }
}