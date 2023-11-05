using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

//Based on: StyleCop SA1512
namespace Cobra.Analyzer
{
    //ReSharper disable once InconsistentNaming
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    internal class CO0001SingleLineCommentMustNotBeginWithSpace : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "CO0001";

        private static readonly LocalizableString _title = new LocalizableResourceString(
            nameof(Resources.CO0001Title), Resources.ResourceManager, typeof(Resources)
        );

        private static readonly DiagnosticDescriptor _descriptor = new DiagnosticDescriptor(
            DiagnosticId,
            _title,
            _title,
            Constants.Category,
            DiagnosticSeverity.Error,
            true
        );

        private static readonly Action<SyntaxTreeAnalysisContext> _syntaxTreeAction = HandleSyntaxTree;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(_descriptor);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxTreeAction(_syntaxTreeAction);
        }

        private static void HandleSyntaxTree(SyntaxTreeAnalysisContext context)
        {
            var root = context.Tree.GetCompilationUnitRoot(context.CancellationToken);

            foreach (var trivia in root.DescendantTrivia())
            {
                switch (trivia.Kind())
                {
                    case SyntaxKind.SingleLineCommentTrivia:
                        HandleSingleLineCommentTrivia(context, trivia);
                        break;
                }
            }
        }

        private static void HandleSingleLineCommentTrivia(SyntaxTreeAnalysisContext context, SyntaxTrivia trivia)
        {
            var text = trivia.ToFullString();

            var spaceCount = 0;
            for (var i = 2; (i < text.Length) && (text[i] == ' '); i++)
            {
                spaceCount++;
            }

            if (spaceCount == 0)
            {
                return;
            }

            context.ReportDiagnostic(Diagnostic.Create(_descriptor, trivia.GetLocation()));
        }
    }
}
