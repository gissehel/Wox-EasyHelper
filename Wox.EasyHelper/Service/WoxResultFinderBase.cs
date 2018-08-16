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

        public virtual IEnumerable<WoxResult> GetResults(WoxQuery query) => MatchCommands(query, 0, CommandInfos, string.Empty);

        protected List<CommandInfo> CommandInfos => GetCommandInfos(string.Empty);

        protected Dictionary<string, List<CommandInfo>> CommandInfosByPath { get; set; } = new Dictionary<string, List<CommandInfo>>();
        protected Dictionary<string, CommandInfo> DefaultCommandInfoByPath { get; set; } = new Dictionary<string, CommandInfo>();

        protected void AddDefaultCommand(Func<WoxQuery, int, IEnumerable<WoxResult>> func)
            => AddDefaultCommand(func, string.Empty);

        protected void AddDefaultCommand(Func<WoxQuery, int, IEnumerable<WoxResult>> func, string path)
            => AddCommand(null, null, null, null, func, path);

        protected void AddCommand(string name, string title, string subtitle, Func<WoxQuery, int, IEnumerable<WoxResult>> func)
            => AddCommand(name, title, subtitle, func, string.Empty);

        protected void AddCommand(string name, string title, string subtitle, Func<WoxQuery, int, IEnumerable<WoxResult>> func, string path)
            => AddCommand(name, title, subtitle, null, func, path);

        protected void AddCommand(string name, string title, string subtitle, Action action)
            => AddCommand(name, title, subtitle, action, string.Empty);

        protected void AddCommand(string name, string title, string subtitle, Action action, string path)
            => AddCommand(name, title, subtitle, action, null, path);

        private void AddCommand(string name, string title, string subtitle, Action action, Func<WoxQuery, int, IEnumerable<WoxResult>> func, string path)
        {
            var actualPath = string.IsNullOrEmpty(path) ? string.Empty : path;
            if (name == null)
            {
                DefaultCommandInfoByPath[actualPath] = new CommandInfo { ResultGetter = func };
            }
            else
            {
                var commandInfo = new CommandInfo { Name = name, Title = title, Subtitle = subtitle, FinalAction = action, ResultGetter = func, Path = actualPath };
                GetCommandInfos(actualPath).Add(commandInfo);
            }
        }

        protected List<CommandInfo> GetCommandInfos(string path) => CommandInfosByPath.GetAndSetDefault(path, () => new List<CommandInfo>());

        protected CommandInfo GetDefaultCommandInfo(string path) => DefaultCommandInfoByPath.GetOrDefault(path, null as CommandInfo);

        protected WoxResult GetEmptyCommandResult(string commandName, IEnumerable<CommandInfo> commandInfos)
        {
            foreach (var commandInfo in commandInfos)
            {
                if (commandName == commandInfo.Name)
                {
                    return GetEmptyCommandResult(commandInfo);
                }
            }
            return null;
        }

        private WoxResult GetEmptyCommandResult(CommandInfo commandInfo)
        {
            if (commandInfo != null)
            {
                if (commandInfo.FinalAction != null)
                {
                    return GetActionResult(commandInfo.Title, commandInfo.Subtitle, commandInfo.FinalAction);
                }
                else
                {
                    return GetCompletionResult(commandInfo.Title, commandInfo.Subtitle, () => string.IsNullOrEmpty(commandInfo.Path) ? commandInfo.Name : commandInfo.Path + WoxContextService.Seperater + commandInfo.Name);
                }
            }
            return null;
        }

        protected IEnumerable<WoxResult> MatchCommands(WoxQuery query, int position, IEnumerable<CommandInfo> commandInfos, string path)
        {
            var results = new List<WoxResult>();
            var term = query.GetTermOrEmpty(position);
            foreach (var commandInfo in commandInfos)
            {
                var commandName = commandInfo.Name;
                var newPath = commandName;
                if (!string.IsNullOrEmpty(path))
                {
                    newPath = path + WoxContextService.Seperater + commandName;
                }
                if (commandName.MatchPattern(term))
                {
                    if (term == commandName)
                    {
                        if (commandInfo.FinalAction != null)
                        {
                            results.Add(GetActionResult(commandInfo.Title, commandInfo.Subtitle, commandInfo.FinalAction));
                        }
                        else if (commandInfo.ResultGetter != null)
                        {
                            var subCommandResults = commandInfo.ResultGetter(query, position + 1);
                            if (subCommandResults != null)
                            {
                                results.AddRange(subCommandResults);
                            }
                        }
                        else
                        {
                            var subCommandResults = MatchCommands(query, position + 1, GetCommandInfos(newPath), newPath);
                            if (subCommandResults != null)
                            {
                                results.AddRange(subCommandResults);
                            }
                        }
                    }
                    else
                    {
                        if (query.SearchTerms.Length <= position + 1)
                        {
                            var result = GetEmptyCommandResult(commandInfo);
                            if (result != null)
                            {
                                results.Add(result);
                            }
                        }
                    }
                }
            }
            if (results.Count == 0)
            {
                var commandInfo = GetDefaultCommandInfo(path);
                if (commandInfo != null)
                {
                    var commandResults = commandInfo.ResultGetter(query, position);
                    if (commandResults != null)
                    {
                        results.AddRange(commandResults);
                    }
                }
                else
                {
                    results.Add(GetEmptyCommandResult(path, commandInfos));
                }
            }
            return results;
        }
    }
}