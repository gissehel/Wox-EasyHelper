using System;
using Wox.EasyHelper.DomainModel;

namespace Wox.EasyHelper.Core.Service
{
    public interface IWoxContextService
    {
        void ChangeQuery(string query);

        string ActionKeyword { get; }

        string Seperater { get; }

        string IconPath { get; }
    }
}