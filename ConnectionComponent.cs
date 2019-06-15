using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDE_Transfer
{
    public abstract class ConnectionComponent
    {
        protected bool _isConnecting = false;
        protected List<DataSource> _dataSources;
        public bool IsConnecting()
        {
            return _isConnecting;
        }
        public abstract void StartConnecting();
        public abstract void StopConnecting();
        public abstract void AddorUpdate(DataSource ds);
        public  void SetupDataSource(List<DataSource> dataSources)
        {
            this._dataSources = dataSources;
        }


    }
}
