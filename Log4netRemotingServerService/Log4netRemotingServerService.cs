using System;
using System.Configuration;
using System.Runtime.Remoting;
using System.ServiceProcess;
using log4net;

/*
 * Copyright (C) 2010 Svante Seleborg/Axantum Software AB, All rights reserved.
 * 
 * This program is free software; you can redistribute it and/or modify it under the terms
 * of the GNU General Public License as published by the Free Software Foundation;
 * either version 2 of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with this program;
 * if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330,
 * Boston, MA 02111-1307 USA
 * 
 * If you wish to use this program under different terms, please contact the author.
 * 
 * The author may be reached at mailto:svante@axantum.com and http://www.axantum.com
 *
 */
[assembly: log4net.Config.XmlConfigurator()]
namespace Log4netRemotingServerService
{
    public partial class Log4netRemotingServerService : ServiceBase
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Log4netRemotingServerService));

        public Log4netRemotingServerService()
        {
            InitializeComponent();

            // Configure remoting. This loads the TCP channel as specified in the .config file.
            RemotingConfiguration.Configure(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, false);
        }

        private log4net.Plugin.RemoteLoggingServerPlugin _plugin = null;

        protected override void OnStart(string[] args)
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info("Log4netRemotingServerService Starting");
            }

            if (_plugin != null)
            {
                _plugin.Shutdown();
                _plugin = null;
            }

            _plugin = new log4net.Plugin.RemoteLoggingServerPlugin("Log4netRemotingServerService");

            // Publish the remote logging server. This is done using the log4net plugin.
            LogManager.GetRepository().PluginMap.Add(_plugin);

            if (_log.IsInfoEnabled)
            {
                _log.Info("Log4netRemotingServerService Started");
            }
        }

        protected override void OnStop()
        {
            if (_log.IsInfoEnabled)
            {
                _log.Info("Log4netRemotingServerService Stopping");
            }

            string isReadable = ConfigurationManager.AppSettings["log4net.Internal.Debug"];

            LogManager.GetRepository().PluginMap.Remove(_plugin);
            _plugin.Shutdown();
            _plugin = null;

            if (_log.IsInfoEnabled)
            {
                _log.Info("Log4netRemotingServerService Stopped");
            }
        }
    }
}
