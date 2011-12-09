using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace DartLanguage.Classifier
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.InheritanceClassType)]
    [Name("Dart.InheritanceClassificationFormat")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class InheritanceClassificationFormat : ClassificationFormatDefinition
    {
        public InheritanceClassificationFormat()
        {
            this.DisplayName = "Dart Inheritance";
            this.ForegroundColor = Colors.SlateBlue;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.KeywordClassType)]
    [Name("Dart.KeywordClassificationFormat")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class KeywordClassificationFormat : ClassificationFormatDefinition
    {
        public KeywordClassificationFormat()
        {
            this.DisplayName = "Dart Keyword";
            this.ForegroundColor = Colors.Blue;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.ConstantClassType)]
    [Name("Dart.ConstantsClassificationFormat")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class StringClassificationFormat : ClassificationFormatDefinition
    {
        public StringClassificationFormat()
        {
            this.DisplayName = "Dart Constants";
            this.ForegroundColor = Colors.Brown;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.MultilineCommentClassType)]
    [Name("Dart.MultilineCommentClassificationFormat")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class MultilineCommentClassificationFormat : ClassificationFormatDefinition
    {
        public MultilineCommentClassificationFormat()
        {
            this.DisplayName = "Dart Comment Multiline";
            this.ForegroundColor = Colors.DimGray;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Constants.SingleLineCommentClassType)]
    [Name("Dart.SinglelineCommentClassificationFormat")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class SinglelineCommentClassificationFormat : ClassificationFormatDefinition
    {
        public SinglelineCommentClassificationFormat()
        {
            this.DisplayName = "Dart Comment";
            this.ForegroundColor = Colors.DimGray;
        }
    }
}
