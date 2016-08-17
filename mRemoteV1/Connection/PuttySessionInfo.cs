using mRemoteNG.App;
using mRemoteNG.Messages;
using mRemoteNG.Tools;
using System;
using System.ComponentModel;
using mRemoteNG.Connection.Protocol;
using mRemoteNG.Root.PuttySessions;


namespace mRemoteNG.Connection
{
	public class PuttySessionInfo : ConnectionInfo, IComponent
	{
        #region Properties
        [Browsable(false)]
        public PuttySessionsNodeInfo RootPuttySessionsInfo { get; set; }

        [ReadOnly(true)]
        public override string PuttySession { get; set; }

        [ReadOnly(true)]
        public override string Name { get; set; }

        [ReadOnly(true), Browsable(false)]
        public override string Description { get; set; }

        [ReadOnly(true), Browsable(false)]
        public override string Icon
        {
            get { return "PuTTY"; }
            set { }
        }

        [ReadOnly(true), Browsable(false)]
        public override string Panel
        {
            get { return RootPuttySessionsInfo.Panel; }
            set { }
        }

        [ReadOnly(true)]
        public override string Hostname { get; set; }

        [ReadOnly(true)]
        public override string Username { get; set; }

        [ReadOnly(true), Browsable(false)]
        public override string Password { get; set; }

        [ReadOnly(true)]
        public override ProtocolType Protocol { get; set; }

        [ReadOnly(true)]
        public override int Port { get; set; }

        [ReadOnly(true), Browsable(false)]
        public override string PreExtApp { get; set; }

        [ReadOnly(true), Browsable(false)]
        public override string PostExtApp { get; set; }

        [ReadOnly(true), Browsable(false)]
        public override string MacAddress { get; set; }

        [ReadOnly(true), Browsable(false)]
        public override string UserField { get; set; }
        #endregion

        [Command(),LocalizedAttributes.LocalizedDisplayName("strPuttySessionSettings")]
        public void SessionSettings()
		{
			try
			{
				var puttyProcess = new PuttyProcessController();
				if (!puttyProcess.Start())
				{
					return ;
				}
				if (puttyProcess.SelectListBoxItem(PuttySession))
				{
					puttyProcess.ClickButton("&Load");
				}
				puttyProcess.SetControlText("Button", "&Cancel", "&Close");
				puttyProcess.SetControlVisible("Button", "&Open", false);
				puttyProcess.WaitForExit();
			}
			catch (Exception ex)
			{
				Runtime.MessageCollector.AddMessage(MessageClass.ErrorMsg, Language.strErrorCouldNotLaunchPutty + Environment.NewLine + ex.Message);
			}
	    }
	    
		
        #region IComponent
        [Browsable(false)]
        public ISite Site
		{
			get { return new PropertyGridCommandSite(this); }
			set { throw (new NotImplementedException()); }
		}
				
		public void Dispose()
		{
		    Disposed?.Invoke(this, EventArgs.Empty);
		}

	    public event EventHandler Disposed;
        #endregion
	}
}