﻿using GDShrapt.Reader.Declarations;
using GDShrapt.Reader.Types;

namespace GDShrapt.Reader
{

    public interface IGDVisitor : IGDBaseVisitor
    {
        void WillVisit(GDNode node);
        void DidLeft(GDNode node);

        void Visit(GDClassDeclaration d);
        void Visit(GDDictionaryKeyValueDeclaration d);
        void Visit(GDEnumDeclaration d);
        void Visit(GDEnumValueDeclaration d);
        void Visit(GDInnerClassDeclaration d);
        void Visit(GDMatchCaseDeclaration d);
        void Visit(GDParameterDeclaration d);
        void Visit(GDSignalDeclaration d);
        void Visit(GDVariableDeclaration d);

        void Visit(GDIfBranch b);
        void Visit(GDElseBranch b);
        void Visit(GDElifBranch b);

        void Visit(GDClassAtributesList list);
        void Visit(GDClassMembersList list);
        void Visit(GDDictionaryKeyValueDeclarationList list);
        void Visit(GDElifBranchesList list);
        void Visit(GDEnumValuesList list);
        void Visit(GDDataParametersList list);
        void Visit(GDExpressionsList list);
        void Visit(GDMatchCasesList list);
        void Visit(GDParametersList list);
        void Visit(GDPathList list);
        void Visit(GDLayersList list);
        void Visit(GDStatementsList list);

        void Visit(GDMethodDeclaration d);
        void Visit(GDToolAtribute a);
        void Visit(GDClassNameAtribute a);
        void Visit(GDExtendsAtribute a);
        void Visit(GDExpressionStatement s);
        void Visit(GDIfStatement s);
        void Visit(GDForStatement s);
        void Visit(GDMatchStatement s);
        void Visit(GDVariableDeclarationStatement s);
        void Visit(GDWhileStatement s);

        void Visit(GDClassMemberAttributeDeclaration d);
        void Visit(GDGetAccessorBodyDeclaration d);
        void Visit(GDAttribute attr);
        void Visit(GDSetAccessorBodyDeclaration d);
        void Visit(GDSetAccessorMethodDeclaration d);
        void Visit(GDSingleTypeNode type);
        void Visit(GDArrayTypeNode type);
        void Visit(GDGetAccessorMethodDeclaration d);

        void Left(GDWhileStatement s);
        void Left(GDVariableDeclarationStatement s);
        void Left(GDMatchStatement s);
        void Left(GDForStatement s);
        void Left(GDIfStatement s);
        void Left(GDExpressionStatement s);
        void Left(GDToolAtribute a);
        void Left(GDClassNameAtribute a);
        void Left(GDExtendsAtribute a);
        void Left(GDVariableDeclaration d);
        void Left(GDMethodDeclaration d);
        void Left(GDInnerClassDeclaration d);
        void Left(GDParameterDeclaration d);
        void Left(GDClassDeclaration d);
        void Left(GDDictionaryKeyValueDeclaration decl);
        void Left(GDEnumDeclaration decl);
        void Left(GDEnumValueDeclaration decl);
        void Left(GDMatchCaseDeclaration decl);
        void Left(GDSignalDeclaration decl);
        void Left(GDClassAtributesList list);
        void Left(GDClassMembersList list);
        void Left(GDDictionaryKeyValueDeclarationList list);
        void Left(GDElifBranchesList list);
        void Left(GDEnumValuesList list);
        void Left(GDDataParametersList list);
        void Left(GDExpressionsList list);
        void Left(GDMatchCasesList list);
        void Left(GDParametersList list);
        void Left(GDPathList list);
        void Left(GDLayersList list);
        void Left(GDStatementsList list);
        void Left(GDIfBranch branch);
        void Left(GDElseBranch branch);
        void Left(GDElifBranch branch);
        void Left(GDClassMemberAttributeDeclaration d);
        void Left(GDAttribute attr);
        void Left(GDGetAccessorBodyDeclaration d);
        void Left(GDSetAccessorBodyDeclaration d);
        void Left(GDSetAccessorMethodDeclaration d);
        void Left(GDSingleTypeNode type);
        void Left(GDArrayTypeNode type);
        void Left(GDGetAccessorMethodDeclaration d);

        void EnterListChild(GDNode node);
        void LeftListChild(GDNode node);
        void WillVisitExpression(GDExpression e);
        void DidLeftExpression(GDExpression e);

        void Visit(GDArrayInitializerExpression e);
        void Visit(GDBoolExpression e);
        void Visit(GDBracketExpression e);
        void Visit(GDBreakExpression e);
        void Visit(GDBreakPointExpression e);
        void Visit(GDCallExpression e);
        void Visit(GDContinueExpression e);
        void Visit(GDDictionaryInitializerExpression e);
        void Visit(GDDualOperatorExpression e);
        void Visit(GDGetNodeExpression e);
        void Visit(GDIdentifierExpression e);
        void Visit(GDIfExpression e);
        void Visit(GDIndexerExpression e);
        void Visit(GDMatchCaseVariableExpression e);
        void Left(GDArrayInitializerExpression e);
        void Visit(GDMatchDefaultOperatorExpression e);
        void Visit(GDNodePathExpression e);
        void Visit(GDNumberExpression e);
        void Visit(GDMemberOperatorExpression e);
        void Visit(GDPassExpression e);
        void Left(GDBoolExpression e);
        void Visit(GDReturnExpression e);
        void Visit(GDSingleOperatorExpression e);
        void Visit(GDStringExpression e);
        void Visit(GDYieldExpression e);
        void Visit(GDAsyncExpression e);
        void Visit(GDMethodExpression e);
        void Left(GDBracketExpression e);
        void Left(GDYieldExpression e);
        void Left(GDStringExpression e);
        void Left(GDSingleOperatorExpression e);
        void Left(GDReturnExpression e);
        void Left(GDPassExpression e);
        void Left(GDNumberExpression e);
        void Left(GDBreakExpression e);
        void Left(GDNodePathExpression e);
        void Left(GDMemberOperatorExpression e);
        void Left(GDMatchDefaultOperatorExpression e);
        void Left(GDMatchCaseVariableExpression e);
        void Left(GDIndexerExpression e);
        void Left(GDBreakPointExpression e);
        void Left(GDIfExpression e);
        void Left(GDIdentifierExpression e);
        void Left(GDGetNodeExpression e);
        void Left(GDDictionaryInitializerExpression e);
        void Left(GDContinueExpression e);
        void Left(GDCallExpression e);
        void Left(GDDualOperatorExpression e);
        void Left(GDAsyncExpression e);
        void Left(GDMethodExpression e);
    }
}