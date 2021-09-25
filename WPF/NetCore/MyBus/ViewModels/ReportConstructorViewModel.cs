using System;
using System.IO;
using System.Windows.Documents;
using MyBus.ViewModels.Base;
using MyBus.Infrastructure.Navigation;
using MyBus.Infrastructure.Navigation.Base;
using MyBus.Infrastructure.Utils;
using Word = Microsoft.Office.Interop.Word;

namespace MyBus.ViewModels
{
    public class ReportConstructorViewModel : ViewModel, INavigationAware
    {
        private NavigationManager _NavigationManager;
        public void OnNavigatingTo(NavigationManager navigationManager, params object[] args)
        {
            _NavigationManager = navigationManager;
        }

        public ReportConstructorViewModel()
        {
            //Word.Application wordApp = new Word.Application { Visible = false };
            var path = AppDomain.CurrentDomain.BaseDirectory;
            //Document = wordApp.Documents.Add(path + @"\Trip.dotx");
            
            
            using var stream = File.Open(path + @"\Trip1.dotx", FileMode.Open);
            DocxToFlowDocumentConverter flowDocumentConverter = new(stream);
            flowDocumentConverter.Read();
            FlowDocument = flowDocumentConverter.Document;
        }
        #region Document
        private Word.Document _Document;
        public Word.Document Document
        {
            get=> _Document;
            set=> Set(ref _Document, value);
        }
        #endregion
        #region FlowDocument
        private FlowDocument _FlowDocument;
        public FlowDocument FlowDocument
        {
            get=> _FlowDocument;
            set=> Set(ref _FlowDocument, value);
        }
        #endregion
        #region Text
        private string _Text;
        public string Text
        {
            get=> _Text;
            set=> Set(ref _Text, value);
        }
        #endregion
    }
}
