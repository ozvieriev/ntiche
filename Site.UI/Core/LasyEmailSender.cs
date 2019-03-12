using Email.Templates;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Site.UI.Core
{
    public interface ILasyEmailSender
    {
        void Send(LasyEmailViewModel model);
    }

    public class LasyEmailSender : ILasyEmailSender
    {
        private static ConcurrentQueue<LasyEmailViewModel> _lasyEmailViewModels;
        private static Logger _oauthLoggerEmail = LogManager.GetLogger("oauth-logger-email");

        static LasyEmailSender()
        {
            _lasyEmailViewModels = new ConcurrentQueue<LasyEmailViewModel>();

            var cancelationTokenSource = new CancellationTokenSource();
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        LasyEmailViewModel model;
                        if (_lasyEmailViewModels.TryDequeue(out model))
                            EmailSender.Send(model.Subject, model.To, model.EmailTemplate, model.Notification);
                    }

                    catch (Exception exc) { _oauthLoggerEmail.Error(exc); }
                    Task.Delay(TimeSpan.FromSeconds(5), cancelationTokenSource.Token).Wait();
                }

            }, cancelationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        public void Send(LasyEmailViewModel model)
        {
            _lasyEmailViewModels.Enqueue(model);
        }
    }
    public class LasyEmailViewModel
    {
        public LasyEmailViewModel(string subject, string to, EmailTemplate emailTemplate, Notification notification)
        {
            Subject = subject;
            To = to;

            EmailTemplate = emailTemplate;
            Notification = notification;
        }

        public string Subject { get; private set; }
        public string To { get; private set; }
        public EmailTemplate EmailTemplate { get; private set; }
        public Notification Notification { get; private set; }
    }
}