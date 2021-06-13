﻿namespace GDShrapt.Reader
{
    public sealed class GDMatchDefaultOperatorExpression : GDExpression,
        ITokenReceiver<GDDefaultToken>
    {
        public override int Priority => GDHelper.GetOperationPriority(GDOperationType.DefaultOperator);

        internal GDDefaultToken DefaultToken
        {
            get => _form.Token0;
            set => _form.Token0 = value;
        }

        enum State
        {
            Default,
            Completed
        }

        readonly GDTokensForm<State, GDDefaultToken> _form = new GDTokensForm<State, GDDefaultToken>();
        internal override GDTokensForm Form => _form;

        internal override void HandleChar(char c, GDReadingState state)
        {
            if (_form.State == State.Default)
                state.Push(new GDSingleCharTokenResolver<GDDefaultToken>(this));
            else
                state.Pop();

            state.PassChar(c);
        }

        internal override void HandleLineFinish(GDReadingState state)
        {
            state.Pop();
            state.PassLineFinish();
        }

        void ITokenReceiver<GDDefaultToken>.HandleReceivedToken(GDDefaultToken token)
        {
            if (_form.State == State.Default)
            {
                _form.State = State.Completed;
                DefaultToken = token;
                return;
            }

            throw new GDInvalidReadingStateException();
        }

        void ITokenReceiver<GDDefaultToken>.HandleReceivedTokenSkip()
        {
            if (_form.State == State.Default)
            {
                _form.State = State.Completed;
                return;
            }

            throw new GDInvalidReadingStateException();
        }
    }
}
