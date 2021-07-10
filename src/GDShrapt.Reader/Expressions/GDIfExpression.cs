﻿namespace GDShrapt.Reader
{
    public sealed class GDIfExpression : GDExpression,
        ITokenOrSkipReceiver<GDExpression>,
        ITokenOrSkipReceiver<GDIfKeyword>,
        ITokenOrSkipReceiver<GDElseKeyword>
    {
        public override int Priority => GDHelper.GetOperationPriority(GDOperationType.If);

        public GDExpression TrueExpression
        {
            get => _form.Token0;
            set => _form.Token0 = value;
        }
        public GDIfKeyword IfKeyword
        {
            get => _form.Token1;
            set => _form.Token1 = value;
        }
        public GDExpression Condition
        {
            get => _form.Token2;
            set => _form.Token2 = value;
        }
        public GDElseKeyword ElseKeyword
        {
            get => _form.Token3;
            set => _form.Token3 = value;
        }

        public GDExpression FalseExpression
        {
            get => _form.Token4;
            set => _form.Token4 = value;
        }

        enum State
        {
            True,
            If,
            Condition,
            Else,
            False,
            Completed
        }

        readonly GDTokensForm<State, GDExpression, GDIfKeyword, GDExpression, GDElseKeyword, GDExpression> _form;
        public override GDTokensForm Form => _form;
        public GDIfExpression()
        {
            _form = new GDTokensForm<State, GDExpression, GDIfKeyword, GDExpression, GDElseKeyword, GDExpression>(this);
        }

        internal override void HandleChar(char c, GDReadingState state)
        {
            switch (_form.State)
            {
                case State.True:
                    if (!this.ResolveSpaceToken(c, state))
                        state.PushAndPass(new GDExpressionResolver(this), c);
                    break;
                case State.If:
                    if (!this.ResolveSpaceToken(c, state))
                        state.PushAndPass(new GDKeywordResolver<GDIfKeyword>(this), c);
                    break;
                case State.Condition:
                    if (!this.ResolveSpaceToken(c, state))
                        state.PushAndPass(new GDExpressionResolver(this), c);
                    break;
                case State.Else:
                    if (!this.ResolveSpaceToken(c, state))
                        state.PushAndPass(new GDKeywordResolver<GDElseKeyword>(this), c);
                    break;
                case State.False:
                    if (!this.ResolveSpaceToken(c, state))
                        state.PushAndPass(new GDExpressionResolver(this), c);
                    break;
                default:
                    state.PopAndPass(c);
                    break;
            }
        }

        internal override void HandleNewLineChar(GDReadingState state)
        {
            state.PopAndPassNewLine();
        }

        protected override GDExpression PriorityRebuildingPass()
        {
            if (IsHigherPriorityThan(TrueExpression, GDSideType.Left))
            {
                var previous = TrueExpression;
                // Remove expression to break cycle
                TrueExpression = null;
                TrueExpression = previous.SwapRight(this).RebuildRootOfPriorityIfNeeded();
                return previous;
            }

            if (IsHigherPriorityThan(FalseExpression, GDSideType.Right))
            {
                var previous = FalseExpression;

                // Remove expression to break cycle
                FalseExpression = null;
                FalseExpression = previous.SwapLeft(this).RebuildRootOfPriorityIfNeeded();
                return previous;
            }

            return this;
        }

        public override GDExpression SwapLeft(GDExpression expression)
        {
            var left = TrueExpression;
            TrueExpression = expression;
            return left;
        }

        public override GDExpression SwapRight(GDExpression expression)
        {
            var right = FalseExpression;
            FalseExpression = expression;
            return right;
        }

        public override void RebuildBranchesOfPriorityIfNeeded()
        {
            TrueExpression = TrueExpression.RebuildRootOfPriorityIfNeeded();
            FalseExpression = FalseExpression.RebuildRootOfPriorityIfNeeded();
        }

        public override GDNode CreateEmptyInstance()
        {
            return new GDIfExpression();
        }

        void ITokenReceiver<GDExpression>.HandleReceivedToken(GDExpression token)
        {
            if (_form.State == State.True)
            {
                _form.State = State.If;
                TrueExpression = token;
                return;
            }

            if (_form.State == State.Condition)
            {
                _form.State = State.Else;
                Condition = token;
                return;
            }

            if (_form.State == State.False)
            {
                _form.State = State.Completed;
                FalseExpression = token;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenSkipReceiver<GDExpression>.HandleReceivedTokenSkip()
        {
            if (_form.State == State.True)
            {
                _form.State = State.If;
                return;
            }

            if (_form.State == State.Condition)
            {
                _form.State = State.Else;
                return;
            }

            if (_form.State == State.False)
            {
                _form.State = State.Completed;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenReceiver<GDIfKeyword>.HandleReceivedToken(GDIfKeyword token)
        {
            if (_form.State == State.If)
            {
                _form.State = State.Condition;
                IfKeyword = token;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenSkipReceiver<GDIfKeyword>.HandleReceivedTokenSkip()
        {
            if (_form.State == State.If)
            {
                _form.State = State.Condition;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenReceiver<GDElseKeyword>.HandleReceivedToken(GDElseKeyword token)
        {
            if (_form.State == State.Else)
            {
                _form.State = State.False;
                ElseKeyword = token;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenSkipReceiver<GDElseKeyword>.HandleReceivedTokenSkip()
        {
            if (_form.State == State.Else)
            {
                _form.State = State.False;
                return;
            }

            throw new GDInvalidStateException();
        }
    }
}
