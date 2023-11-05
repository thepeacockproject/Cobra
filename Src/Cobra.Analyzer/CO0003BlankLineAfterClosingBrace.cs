using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

//Based on: StyleCop SA1513
namespace Cobra.Analyzer
{
    //ReSharper disable once InconsistentNaming
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    internal class CO0003BlankLineAfterClosingBrace : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "CO0003";

        private const SyntaxKind InitKeyword = (SyntaxKind)8443;

        private static readonly LocalizableString _title = new LocalizableResourceString(
            nameof(Resources.CO0003Title), Resources.ResourceManager, typeof(Resources)
        );

        internal static readonly DiagnosticDescriptor Descriptor = new DiagnosticDescriptor(
            DiagnosticId,
            _title,
            _title,
            Constants.Category,
            DiagnosticSeverity.Error,
            true
        );

        private static readonly Action<SyntaxTreeAnalysisContext> _syntaxTreeAction = HandleSyntaxTree;

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(Descriptor);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxTreeAction(_syntaxTreeAction);
        }

        private static void HandleSyntaxTree(SyntaxTreeAnalysisContext context)
        {
            var syntaxRoot = context.Tree.GetRoot(context.CancellationToken);

            var visitor = new BracesVisitor(context);
            visitor.Visit(syntaxRoot);
        }

        private sealed class BracesVisitor : CSharpSyntaxWalker
        {
            private readonly SyntaxTreeAnalysisContext _context;
            private readonly Stack<SyntaxToken> _bracesStack = new Stack<SyntaxToken>();

            public BracesVisitor(SyntaxTreeAnalysisContext context)
                : base(SyntaxWalkerDepth.Token)
            {
                _context = context;
            }

            public override void VisitToken(SyntaxToken token)
            {
                if (token.IsKind(SyntaxKind.OpenBraceToken))
                {
                    _bracesStack.Push(token);
                }
                else if (token.IsKind(SyntaxKind.CloseBraceToken))
                {
                    AnalyzeCloseBrace(token);

                    _bracesStack.Pop();
                }

                base.VisitToken(token);
            }

            private static bool HasLeadingBlankLine(SyntaxTriviaList triviaList)
            {
                foreach (var trivia in triviaList)
                {
                    switch (trivia.Kind())
                    {
                        case SyntaxKind.WhitespaceTrivia:
                            // ignore
                            break;

                        case SyntaxKind.EndOfLineTrivia:
                            return true;

                        default:
                            return false;
                    }
                }

                return false;
            }

            private static bool StartsWithComment(SyntaxTriviaList triviaList)
            {
                foreach (var trivia in triviaList)
                {
                    switch (trivia.Kind())
                    {
                        case SyntaxKind.WhitespaceTrivia:
                            // ignore
                            break;

                        case SyntaxKind.SingleLineCommentTrivia:
                            return true;

                        default:
                            return false;
                    }
                }

                return false;
            }

            private static bool StartsWithDirectiveTrivia(SyntaxTriviaList triviaList)
            {
                foreach (var trivia in triviaList)
                {
                    switch (trivia.Kind())
                    {
                        case SyntaxKind.WhitespaceTrivia:
                            // ignore
                            break;

                        default:
                            return trivia.IsDirective;
                    }
                }

                return false;
            }

            private static bool IsPartOf<T>(SyntaxToken token)
            {
                var result = false;

                for (var current = token.Parent; !result && (current != null); current = current.Parent)
                {
                    result = current is T;
                }

                return result;
            }

            private void AnalyzeCloseBrace(SyntaxToken token)
            {
                if (token.Parent.IsKind(SyntaxKind.Interpolation))
                {
                    // The text after an interpolation is part of a string literal, and therefore does not require a
                    // blank line in source.
                    return;
                }

                var nextToken = token.GetNextToken(true, true);

                if (nextToken.HasLeadingTrivia
                    && (HasLeadingBlankLine(nextToken.LeadingTrivia) || StartsWithComment(nextToken.LeadingTrivia)))
                {
                    // the close brace has a trailing blank line or is followed by a single line comment that starts with 4 slashes.
                    return;
                }

                if (IsOnSameLineAsOpeningBrace(token))
                {
                    // the close brace is on the same line as the corresponding opening token
                    return;
                }

                if ((token.Parent is BlockSyntax) && (token.Parent.Parent is DoStatementSyntax))
                {
                    // the close brace is part of do ... while statement
                    return;
                }

                // check if the next token is not preceded by significant trivia.
                if (nextToken.LeadingTrivia.All(trivia => trivia.IsKind(SyntaxKind.WhitespaceTrivia)))
                {
                    if (nextToken.IsKind(SyntaxKind.DotToken))
                    {
                        // the close brace is followed by a member accessor on the next line
                        return;
                    }

                    if (nextToken.IsKind(SyntaxKind.CloseBraceToken))
                    {
                        // the close brace is followed by another close brace on the next line
                        return;
                    }

                    if (nextToken.IsKind(SyntaxKind.CatchKeyword) || nextToken.IsKind(SyntaxKind.FinallyKeyword))
                    {
                        // the close brace is followed by catch or finally statement
                        return;
                    }

                    if (nextToken.IsKind(SyntaxKind.ElseKeyword))
                    {
                        // the close brace is followed by else (no need to check for if -> the compiler will handle that)
                        return;
                    }

                    if (IsPartOf<QueryExpressionSyntax>(token))
                    {
                        if (nextToken.Parent is QueryClauseSyntax
                            || nextToken.Parent is SelectOrGroupClauseSyntax
                            || nextToken.Parent is QueryContinuationSyntax)
                        {
                            // the close brace is part of a query expression
                            return;
                        }
                    }

                    if (nextToken.IsKind(SyntaxKind.SemicolonToken) &&
                        (IsPartOf<VariableDeclaratorSyntax>(token) ||
                         IsPartOf<YieldStatementSyntax>(token) ||
                         IsPartOf<ArrowExpressionClauseSyntax>(token) ||
                         IsPartOf<EqualsValueClauseSyntax>(token) ||
                         IsPartOf<AssignmentExpressionSyntax>(token) ||
                         IsPartOf<ReturnStatementSyntax>(token) ||
                         IsPartOf<ThrowStatementSyntax>(token) ||
                         IsPartOf<ObjectCreationExpressionSyntax>(token)))
                    {
                        // the close brace is part of a variable initialization statement or a return/throw statement
                        return;
                    }

                    if (nextToken.IsKind(SyntaxKind.CommaToken) || nextToken.IsKind(SyntaxKind.CloseParenToken))
                    {
                        // The close brace is the end of an object initializer, anonymous function, lambda expression, etc.
                        // Comma and close parenthesis never requires a preceeding blank line.
                        return;
                    }

                    if (nextToken.IsKind(SyntaxKind.ColonToken))
                    {
                        // the close brace is in the first part of a conditional expression.
                        return;
                    }

                    if (nextToken.IsKind(SyntaxKind.AddKeyword)
                        || nextToken.IsKind(SyntaxKind.RemoveKeyword)
                    || nextToken.IsKind(SyntaxKind.GetKeyword)
                        || nextToken.IsKind(SyntaxKind.SetKeyword)
                        || nextToken.IsKind(InitKeyword))
                    {
                        // the close brace is followed by an accessor (SA1516 will handle that)
                        return;
                    }

                    if ((nextToken.IsKind(SyntaxKind.PrivateKeyword)
                        || nextToken.IsKind(SyntaxKind.ProtectedKeyword)
                        || nextToken.IsKind(SyntaxKind.InternalKeyword))
                        && (nextToken.Parent is AccessorDeclarationSyntax))
                    {
                        // the close brace is followed by an accessor with an accessibility restriction.
                        return;
                    }

                    if (nextToken.IsKind(SyntaxKind.EndOfFileToken))
                    {
                        // this is the last close brace in the file
                        return;
                    }
                }

                if (StartsWithDirectiveTrivia(nextToken.LeadingTrivia))
                {
                    // the close brace is followed by directive trivia.
                    return;
                }

                var location = Location.Create(_context.Tree, TextSpan.FromBounds(token.Span.End, nextToken.FullSpan.Start));

                _context.ReportDiagnostic(Diagnostic.Create(Descriptor, location));
            }

            private bool IsOnSameLineAsOpeningBrace(SyntaxToken closeBrace)
            {
                var matchingOpenBrace = _bracesStack.Peek();
                return matchingOpenBrace.SyntaxTree.GetLineSpan(matchingOpenBrace.Span).EndLinePosition.Line == closeBrace.SyntaxTree.GetLineSpan(closeBrace.Span).StartLinePosition.Line;
            }
        }
    }
}
