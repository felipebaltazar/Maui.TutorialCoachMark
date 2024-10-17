using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Maui.TutorialCoachMark.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class UseTutorialCoachMarkInitializationAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "TCM001";

    const string category = "Initialization";
    const string useMauiAppMethodName = "UseMauiApp";
    const string useMauiTutorialCoachMarkMethodName = "UseTutorialCoachMark";

    private static readonly LocalizableString title =
        new LocalizableResourceString(nameof(Resources.InitializationErrorTitle), Resources.ResourceManager, typeof(Resources));

    private static readonly LocalizableString messageFormat =
        new LocalizableResourceString(nameof(Resources.InitalizationMessageFormat), Resources.ResourceManager, typeof(Resources));

    private static readonly LocalizableString description =
        new LocalizableResourceString(nameof(Resources.InitializationErrorMessage), Resources.ResourceManager, typeof(Resources));

    private static readonly DiagnosticDescriptor rule =
        new(DiagnosticId, title, messageFormat, category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [rule];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is InvocationExpressionSyntax invocationExpression
            && invocationExpression.Expression is MemberAccessExpressionSyntax memberAccessExpression
            && memberAccessExpression.Name.Identifier.ValueText == useMauiAppMethodName)
        {
            var root = invocationExpression.SyntaxTree.GetRoot();
            var methodDeclaration = root.FindNode(invocationExpression.FullSpan)
                .Ancestors()
                .OfType<MethodDeclarationSyntax>()
                .FirstOrDefault();

            if (methodDeclaration is not null
                && !methodDeclaration.DescendantNodes().OfType<InvocationExpressionSyntax>().Any(static n =>
                    n.Expression is MemberAccessExpressionSyntax m &&
                    m.Name.Identifier.ValueText == useMauiTutorialCoachMarkMethodName))
            {
                var diagnostic = Diagnostic.Create(rule, invocationExpression.GetLocation());
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
