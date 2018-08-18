using System.Collections.Generic;
using Wox.EasyHelper.Core.Service;
using Wox.EasyHelper.Service;
using Wox.Plugin;

namespace Wox.EasyHelper
{
    public abstract class WoxPlugin : IPlugin
    {
        protected IWoxContextService WoxContextService { get; set; }
        protected IQueryService QueryService { get; set; }
        protected IResultService ResultService { get; set; }
        protected IWoxResultFinder WoxResultFinder { get; set; }

        public void Init(PluginInitContext context)

        {
            WoxContextService = new WoxContextService(context);
            QueryService = new QueryService();
            ResultService = new ResultService(WoxContextService);
            WoxResultFinder = PrepareContext();
        }

        public List<Result> Query(Query query)
        {
            var woxQuery = QueryService.GetWoxQuery(query);
            var results = WoxResultFinder.GetResults(woxQuery);
            return ResultService.MapResults(results);
        }

        public abstract IWoxResultFinder PrepareContext();
    }
}