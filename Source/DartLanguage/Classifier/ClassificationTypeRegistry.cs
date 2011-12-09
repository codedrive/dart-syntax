using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace DartLanguage.Classifier
{
    using Microsoft.VisualStudio.Text.Classification;

    interface IDartClassificationTypeRegistryService
    {
        bool TryGetGetClassificationType(int token, out IClassificationType classificationType);
    }

    [PartCreationPolicy(CreationPolicy.Shared)]
    [Export(typeof(IDartClassificationTypeRegistryService))]
    class DartClassificationTypeRegistry : IDartClassificationTypeRegistryService
    {
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry { get; set; }

        public DartClassificationTypeRegistry()
        {
            _classificationTypes = new Lazy<Dictionary<int, IClassificationType>>(CreateClassificationTypes);
        }

        #region IClassificationTypeRegistryService members

        public bool TryGetGetClassificationType(int token, out IClassificationType classificationType)
        {
            return _classificationTypes.Value.TryGetValue(token, out classificationType);
        }

        #endregion IClassificationTypeRegistryService members

        private Dictionary<int, IClassificationType> CreateClassificationTypes()
        {
            return (new Dictionary<int, IClassificationType>
                {
                    { DartLexer.STRING, GetType(Constants.ConstantClassType) },
                    { DartLexer.NUMBER, GetType(Constants.ConstantClassType) },
                    { DartLexer.HEX_DIGIT, GetType(Constants.ConstantClassType) },
                    { DartLexer.HEX_NUMBER, GetType(Constants.ConstantClassType) },
                    { DartLexer.SINGLE_LINE_COMMENT, GetType(Constants.SingleLineCommentClassType) },
                    { DartLexer.MULTI_LINE_COMMENT, GetType(Constants.MultilineCommentClassType) },

                    { DartLexer.BREAK, GetType(Constants.KeywordClassType) },
                    { DartLexer.CASE, GetType(Constants.KeywordClassType) },
                    { DartLexer.CATCH, GetType(Constants.KeywordClassType) },
                    { DartLexer.CONST, GetType(Constants.KeywordClassType) },
                    { DartLexer.CONTINUE, GetType(Constants.KeywordClassType) },
                    { DartLexer.DEFAULT, GetType(Constants.KeywordClassType) },
                    { DartLexer.DO, GetType(Constants.KeywordClassType) },
                    { DartLexer.ELSE, GetType(Constants.KeywordClassType) },
                    { DartLexer.FALSE, GetType(Constants.KeywordClassType) },
                    { DartLexer.FINAL, GetType(Constants.KeywordClassType) },
                    { DartLexer.FINALLY, GetType(Constants.KeywordClassType) },
                    { DartLexer.FOR, GetType(Constants.KeywordClassType) },
                    { DartLexer.IF, GetType(Constants.KeywordClassType) },
                    { DartLexer.IN, GetType(Constants.KeywordClassType) },
                    { DartLexer.NEW, GetType(Constants.KeywordClassType) },
                    { DartLexer.NULL, GetType(Constants.KeywordClassType) },
                    { DartLexer.RETURN, GetType(Constants.KeywordClassType) },
                    { DartLexer.SUPER, GetType(Constants.KeywordClassType) },
                    { DartLexer.SWITCH, GetType(Constants.KeywordClassType) },
                    { DartLexer.THIS, GetType(Constants.KeywordClassType) },
                    { DartLexer.THROW, GetType(Constants.KeywordClassType) },
                    { DartLexer.TRUE, GetType(Constants.KeywordClassType) },
                    { DartLexer.TRY, GetType(Constants.KeywordClassType) },
                    { DartLexer.VAR, GetType(Constants.KeywordClassType) },
                    { DartLexer.VOID, GetType(Constants.KeywordClassType) },
                    { DartLexer.WHILE, GetType(Constants.KeywordClassType) },
                    
                    { DartLexer.EXTENDS, GetType(Constants.InheritanceClassType) },
                    { DartLexer.IMPLEMENTS, GetType(Constants.InheritanceClassType) },

                    { DartLexer.ABSTRACT, GetType(Constants.KeywordClassType) },
                    { DartLexer.ASSERT, GetType(Constants.KeywordClassType) },
                    { DartLexer.CLASS, GetType(Constants.KeywordClassType) },
                    { DartLexer.FACTORY, GetType(Constants.KeywordClassType) },
                    { DartLexer.GET, GetType(Constants.KeywordClassType) },
                    { DartLexer.IMPORT, GetType(Constants.KeywordClassType) },
                    { DartLexer.INTERFACE, GetType(Constants.KeywordClassType) },
                    { DartLexer.IS, GetType(Constants.KeywordClassType) },
                    { DartLexer.LIBRARY, GetType(Constants.KeywordClassType) },
                    { DartLexer.NATIVE, GetType(Constants.KeywordClassType) },
                    { DartLexer.NEGATE, GetType(Constants.KeywordClassType) },
                    { DartLexer.OPERATOR, GetType(Constants.KeywordClassType) },
                    { DartLexer.SET, GetType(Constants.KeywordClassType) },
                    { DartLexer.SOURCE, GetType(Constants.KeywordClassType) },
                    { DartLexer.STATIC, GetType(Constants.KeywordClassType) },
                    { DartLexer.TYPEDEF, GetType(Constants.KeywordClassType) },
                });
        }

        private IClassificationType GetType(string type)
        {
            return (ClassificationRegistry.GetClassificationType(type));
        }

        private Lazy<Dictionary<int, IClassificationType>> _classificationTypes;
    }
}
