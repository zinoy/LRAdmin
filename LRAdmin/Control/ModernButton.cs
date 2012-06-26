using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LRAdmin.Control
{
    [DefaultEvent("Click"), ParseChildren(false)]
    public class ModernButton : WebControl, IPostBackEventHandler
    {
        // Fields
        private static readonly object EventClick = new object();
        private static readonly object EventCommand = new object();

        // Events
        public event EventHandler Click
        {
            add
            {
                base.Events.AddHandler(EventClick, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventClick, value);
            }
        }

        public event CommandEventHandler Command
        {
            add
            {
                base.Events.AddHandler(EventCommand, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventCommand, value);
            }
        }

        //Methods
        public ModernButton()
            : base(HtmlTextWriterTag.Button)
        {
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            PostBackOptions postBackOptions = this.GetPostBackOptions();
            string uniqueID = this.UniqueID;
            if ((uniqueID != null) && ((postBackOptions == null) || (postBackOptions.TargetControl == this)))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Value, this.Value);
            bool isEnabled = base.IsEnabled;
            string firstScript = String.Empty;
            if (isEnabled)
            {
                firstScript = EnsureEndWithSemiColon(this.OnClientClick);
                if (base.HasAttributes)
                {
                    string baseClick = base.Attributes["onclick"];
                    if (baseClick != null)
                    {
                        firstScript += EnsureEndWithSemiColon(baseClick);
                        base.Attributes.Remove("onclick");
                    }
                }
                if (this.Page != null)
                {
                    string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(postBackOptions, false);
                    if (postBackEventReference != null)
                    {
                        firstScript = MergeScript(firstScript, postBackEventReference);
                    }
                }
            }
            if (this.Page != null)
            {
                this.Page.ClientScript.RegisterForEventValidation(postBackOptions);
            }
            if (firstScript.Length > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, firstScript);
            }
            if (this.Enabled && !isEnabled) writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
            base.AddAttributesToRender(writer);
        }

        protected override void AddParsedSubObject(object obj)
        {
            if (this.HasControls())
            {
                base.AddParsedSubObject(obj);
            }
            else if (obj is LiteralControl)
            {
                this.InnerHtml = ((LiteralControl)obj).Text;
            }
            else
            {
                string html = this.InnerHtml;
                if (html.Length != 0)
                {
                    this.InnerHtml = String.Empty;
                    base.AddParsedSubObject(new LiteralControl(html));
                }
            }
        }

        private string EnsureEndWithSemiColon(string value)
        {
            if (value != null)
            {
                int length = value.Length;
                if ((length > 0) && (value[length - 1] != ';'))
                {
                    return (value + ';');
                }
            }
            return value;
        }

        protected virtual PostBackOptions GetPostBackOptions()
        {
            PostBackOptions options = new PostBackOptions(this, String.Empty);
            options.ClientSubmit = false;
            if (this.Page != null)
            {
                if (this.CausesValidation && (this.Page.GetValidators(this.ValidationGroup).Count > 0))
                {
                    options.PerformValidation = true;
                    options.ValidationGroup = this.ValidationGroup;
                }
                if (!String.IsNullOrEmpty(this.PostBackUrl))
                {
                    options.ActionUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
                }
            }
            return options;
        }

        private string MergeScript(string firstScript, string secondScript)
        {
            if (!string.IsNullOrEmpty(firstScript)) return (firstScript + secondScript);
            if (secondScript.TrimStart(new char[0]).StartsWith("javascript:", StringComparison.Ordinal))
            {
                return secondScript;
            }
            return ("javascript:" + secondScript);
        }

        protected void OnClick(EventArgs e)
        {
            EventHandler handler = (EventHandler)base.Events[EventClick];
            if (handler != null) handler(this, e);
        }

        protected void OnCommand(CommandEventArgs e)
        {
            CommandEventHandler handler = (CommandEventHandler)base.Events[EventCommand];
            if (handler != null) handler(this, e);
            base.RaiseBubbleEvent(this, e);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write(this.InnerHtml);
        }

        //Properties
        public virtual bool CausesValidation
        {
            get
            {
                object causesValidation = this.ViewState["CausesValidation"];
                if (causesValidation != null) return (bool)causesValidation;
                return true;
            }
            set
            {
                this.ViewState["CausesValidation"] = value;
            }
        }

        public string CommandArgument
        {
            get
            {
                return this.ViewState["CommandArgument"] as string ?? String.Empty;
            }
            set
            {
                this.ViewState["CommandArgument"] = value;
            }
        }

        public string CommandName
        {
            get
            {
                return this.ViewState["CommandName"] as string ?? String.Empty;
            }
            set
            {
                this.ViewState["CommandName"] = value;
            }
        }

        public string InnerHtml
        {
            get
            {
                return this.ViewState["InnerHtml"] as string ?? String.Empty;
            }
            set
            {
                this.ViewState["InnerHtml"] = value;
            }
        }

        public virtual string OnClientClick
        {
            get
            {
                return this.ViewState["OnClientClick"] as string ?? String.Empty;
            }
            set
            {
                this.ViewState["OnClientClick"] = value;
            }
        }

        public virtual string PostBackUrl
        {
            get
            {
                return this.ViewState["PostBackUrl"] as string ?? String.Empty;
            }
            set
            {
                this.ViewState["PostBackUrl"] = value;
            }
        }

        public virtual string ValidationGroup
        {
            get
            {
                return this.ViewState["ValidationGroup"] as string ?? String.Empty;
            }
            set
            {
                this.ViewState["ValidationGroup"] = value;
            }
        }

        public string Value
        {
            get
            {
                return this.ViewState["Value"] as string ?? String.Empty;
            }
            set { this.ViewState["Value"] = value; }
        }

        public string Text
        {
            get { return this.InnerHtml; }
            set { this.InnerHtml = value; }
        }

        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            if (this.CausesValidation)
            {
                this.Page.Validate();
            }
            this.OnClick(new EventArgs());
            this.OnCommand(new CommandEventArgs(this.CommandName, this.CommandArgument));
        }
    }
}
