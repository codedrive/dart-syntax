using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Antlr.Runtime;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace DartLanguage.Classifier
{
    #region Provider

    [Export(typeof(IClassifierProvider))]
    [ContentType(Constants.ContentType)]
    [Name("Dart Classifier")]
    [Order(Before = "default")]
    internal class DartClassifierProvider : IClassifierProvider
    {
        [Import]
        internal IDartClassificationTypeRegistryService TypeRegistry { get; set; }

        #region IClassifierProvider Members

        //returns an instance of the classifier
        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            return buffer.Properties.GetOrCreateSingletonProperty(() => new Classifier(buffer, TypeRegistry));
        }

        #endregion
    }

    #endregion Provider

    #region Classifier

    class Classifier : IClassifier
    {
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        internal Classifier(ITextBuffer buffer, IDartClassificationTypeRegistryService registry)
        {
            _buffer = buffer;
            _registry = registry;
            _buffer.Changed += this.OnTextBufferChanged;
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var calassificationSpans = new List<ClassificationSpan>();

            var lexer = new DartLexer(new ANTLRStringStream(_buffer.CurrentSnapshot.GetText(span)));
            for (var token = lexer.NextToken(); token.Type != DartLexer.EOF; token = lexer.NextToken())
            {
                IClassificationType classificationType;
                if (_registry.TryGetGetClassificationType(token.Type, out classificationType))
                {
                    ClassificationSpan classificationSpan;
                    if (TryCreateClassificationSpan(span, token, classificationType, out classificationSpan))
                    {
                        calassificationSpans.Add(classificationSpan);
                    }
                }
            }

            return calassificationSpans;
        }

        private static bool TryCreateClassificationSpan(
            SnapshotSpan span,
            IToken token, IClassificationType type,
            out ClassificationSpan classificationSpan)
        {
            classificationSpan = null;

            if (token.Text == null)
                return false;

            var length = token.Text.Length;
            var start = span.Start.Position + token.CharPositionInLine;
            var snapshotSpan = new SnapshotSpan(span.Snapshot, start, length);

            classificationSpan = new ClassificationSpan(snapshotSpan, type);

            return true;
        }

        private void OnTextBufferChanged(object sender, TextContentChangedEventArgs e)
        {
            ClassificationChanged(this, new ClassificationChangedEventArgs(
                new SnapshotSpan(_buffer.CurrentSnapshot, 0, _buffer.CurrentSnapshot.Length)));
        }

        private ITextBuffer _buffer;
        private IDartClassificationTypeRegistryService _registry;
    }

    #endregion Classifier
}
