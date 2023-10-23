using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

//Based on: StyleCop SA1518
namespace Cobra.Analyzer
{
    //ReSharper disable once InconsistentNaming
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    internal class CO0002BlankLineAtEndOfFile : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "CO0002";

        private static readonly LocalizableString Title = new LocalizableResourceString(
            nameof(Resources.CO0002Title), Resources.ResourceManager, typeof(Resources)
        );

        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            DiagnosticId,
            Title,
            Title,
            Constants.Category,
            DiagnosticSeverity.Error,
            true
        );

        private static readonly Action<SyntaxTreeAnalysisContext> SyntaxTreeAction = HandleSyntaxTree;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(Descriptor);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxTreeAction(SyntaxTreeAction);
        }

        private static void HandleSyntaxTree(SyntaxTreeAnalysisContext context)
        {
            var endOfFileToken = context.Tree.GetRoot().GetLastToken(includeZeroWidth: true);

            //NOTE: Account for empty files
            if (endOfFileToken.FullSpan.End == 0)
            {
                return;
            }

            var previousToken = endOfFileToken.GetPreviousToken(includeZeroWidth: true);

            var endOfFileSpan = new TextSpan(previousToken.FullSpan.Start, endOfFileToken.FullSpan.End - previousToken.FullSpan.Start);

            var sourceText = context.Tree.GetText(context.CancellationToken);
            var endOfFileText = sourceText.ToString(endOfFileSpan);

            if (endOfFileText.IndexOf('\n') >= 0)
            {
                return;
            }

            context.ReportDiagnostic(Diagnostic.Create(Descriptor, endOfFileToken.GetLocation()));
        }
    }
}
