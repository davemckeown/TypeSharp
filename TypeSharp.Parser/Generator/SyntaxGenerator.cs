// <copyright file="SyntaxGenerator.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>


namespace TypeSharp.Parser.Generator
{
    using System;
    using System.Text;

    using Roslyn.Compilers.CSharp;

    /// <summary>
    /// SyntaxGenerator parses and generates TypeScript syntax from CSharp source code
    /// </summary>
    public class SyntaxGenerator
    {
        public string ConvertMethodSyntax(BlockSyntax syntax)
        {
            StringBuilder output = new StringBuilder();


            foreach (var node in syntax.ChildNodes())
            {
                if (node is ExpressionSyntax)
                {
                    this.ConvertExpressionSyntax(output, node as ExpressionSyntax);
                }
                else if (node is StatementSyntax)
                {
                    this.ConvertStatementSyntax(output, node as StatementSyntax);
                }
            }

            foreach (StatementSyntax statement in syntax.Statements)
            {

            }

            return output.ToString();
        }

        private void ConvertExpressionSyntax(StringBuilder output, ExpressionSyntax expression)
        {
            switch (expression.Kind)
            {
                case SyntaxKind.AddExpression:
                    ConvertAddExpression(expression);
                    break;
                case SyntaxKind.SubtractExpression:
                    ConvertSubtractExpression(expression);
                    break;
                
            }
        }

        private void ConvertSubtractExpression(ExpressionSyntax expression)
        {
            throw new NotImplementedException();
        }

        private void ConvertAddExpression(ExpressionSyntax expression)
        {
            throw new NotImplementedException();
        }

        private void ConvertStatementSyntax(StringBuilder output, StatementSyntax statement)
        {
            switch (statement.Kind)
            {
                case SyntaxKind.IfStatement:
                    ConvertIfStatement(output, statement as IfStatementSyntax);
                    break;
                case SyntaxKind.ForStatement:
                    ConvertForStatement(output, statement as ForStatementSyntax);
                    break;
                case SyntaxKind.LocalDeclarationStatement:
                    ConvertVariableDeclaration(output, statement as LocalDeclarationStatementSyntax);
                    break;
            }
        }

        private void ConvertVariableDeclaration(StringBuilder output, LocalDeclarationStatementSyntax localDeclarationStatementSyntax)
        {
            throw new NotImplementedException();
        }

        private void ConvertForStatement(StringBuilder output, ForStatementSyntax forStatementSyntax)
        {
            output.Append("for (");

            foreach (var initializer in forStatementSyntax.Initializers)
            {
                
            }


            foreach (var step in forStatementSyntax.Incrementors)
            {
                
            }

        }

        private void ConvertIfStatement(StringBuilder output, IfStatementSyntax ifStatementSyntax)
        {
            output.Append("if (");
        }
    }
}
