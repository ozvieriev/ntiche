using Email.Templates;
using NLog;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Mail;
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
                        {
                            Attachment attachment = null;
                            FileStream fileStream = null;

                            try
                            {
                                if (!object.Equals(model.Attachment, null))
                                {
                                    fileStream = File.OpenRead(model.Attachment.FileInfo.FullName);
                                    attachment = new Attachment(fileStream, model.Attachment.FileName);
                                }
                                EmailSender.Send(model.Subject, model.To, model.EmailTemplate, model.Notification, attachment);
                            }
                            catch (Exception exc) { _oauthLoggerEmail.Error(exc); }
                            finally
                            {
                                if (!object.Equals(fileStream, null))
                                    fileStream.Dispose();
                            }
                        }
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
        public LasyEmailViewModel(string subject, string to, EmailTemplate emailTemplate, Notification notification,
            LasyEmailAttachment attachment = null)
        {
            Subject = subject;
            To = to;

            EmailTemplate = emailTemplate;
            Notification = notification;
            Attachment = attachment;
        }

        public string Subject { get; private set; }
        public string To { get; private set; }
        public EmailTemplate EmailTemplate { get; private set; }
        public Notification Notification { get; private set; }
        public LasyEmailAttachment Attachment { get; private set; }
    }

    public class LasyEmailAttachment
    {
        public LasyEmailAttachment(string fileName, FileInfo fileInfo)
        {
            FileName = fileName;
            FileInfo = fileInfo;
        }

        public string FileName { get; private set; }
        public FileInfo FileInfo { get; private set; }
    }
}