using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace DartLanguage.Classifier
{
    internal static class DartContentTypeDefinition
    {
        [Export]
        [Name(Constants.ContentType)]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition ContentTypeDefinition { get; set; }

        [Export]
        [FileExtension(".dart")]
        [ContentType(Constants.ContentType)]
        internal static FileExtensionToContentTypeDefinition FileExtensionDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.KeywordClassType)]
        internal static ClassificationTypeDefinition KeywordClassificationType { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.ConstantClassType)]
        internal static ClassificationTypeDefinition ContantClassificationType { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.MultilineCommentClassType)]
        internal static ClassificationTypeDefinition MultilineCommentClassificationType { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.SingleLineCommentClassType)]
        internal static ClassificationTypeDefinition SinglelineCommentClassificationType { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name(Constants.InheritanceClassType)]
        internal static ClassificationTypeDefinition InheritanceClassificationType { get; set; }
    }
}
