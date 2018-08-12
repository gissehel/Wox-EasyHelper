using System.Collections.Generic;
using Wox.EasyHelper.Core.Service;
using Wox.EasyHelper.Service;
using Wox.Plugin;

namespace Wox.EasyHelper
{
    public abstract class PluginBase<T> : IPlugin where T : IWoxResultFinder
    {
        protected IWoxContextService WoxContextService { get; set; }
        protected IQueryService QueryService { get; set; }
        protected IResultService ResultService { get; set; }
        protected T ResultFinder { get; set; }

        public void Init(PluginInitContext context)

        {
            WoxContextService = new WoxContextService(context);
            QueryService = new QueryService();
            ResultService = new ResultService(WoxContextService);
            ResultFinder = PrepareContext();
        }

        public List<Result> Query(Query query)
        {
            var woxQuery = QueryService.GetWoxQuery(query);
            var results = ResultFinder.GetResults(woxQuery);
            return ResultService.MapResults(results);
        }

        public abstract T PrepareContext();
    }
}