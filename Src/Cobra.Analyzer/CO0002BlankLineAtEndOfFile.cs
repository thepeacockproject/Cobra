﻿using System;
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
        private const string DiagnosticId = "CO0002";

        private static readonly LocalizableString _title = new LocalizableResourceString(
            nameof(Resources.CO0002Title), Resources.ResourceManager, typeof(Resources)
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

            context.ReportDiagnostic(Diagnostic.Create(_descriptor, endOfFileToken.GetLocation()));
        }
    }
}
