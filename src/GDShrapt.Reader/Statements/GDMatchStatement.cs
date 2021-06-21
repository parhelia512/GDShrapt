﻿namespace GDShrapt.Reader
{
    public sealed class GDMatchStatement : GDStatement,
        IKeywordReceiver<GDMatchKeyword>,
        ITokenReceiver<GDColon>,
        IExpressionsReceiver
    {
        internal GDMatchKeyword MatchKeyword
        {
            get => _form.Token0;
            set => _form.Token0 = value;
        }
        public GDExpression Value
        {
            get => _form.Token1;
            set => _form.Token1 = value;
        }

        internal GDColon Colon
        {
            get => _form.Token2;
            set => _form.Token2 = value;
        }

        public GDMatchCasesList Cases { get => _form.Token3 ?? (_form.Token3 = new GDMatchCasesList(LineIntendation + 1)); }

        enum State
        {
            Match,
            Value,
            Colon,
            Cases,
            Completed
        }

        readonly GDTokensForm<State, GDMatchKeyword, GDExpression, GDColon, GDMatchCasesList> _form;
        internal override GDTokensForm Form => _form;

        internal GDMatchStatement(int lineIntendation)
            : base(lineIntendation)
        {
            _form = new GDTokensForm<State, GDMatchKeyword, GDExpression, GDColon, GDMatchCasesList>(this);
        }

        public GDMatchStatement()
        {
            _form = new GDTokensForm<State, GDMatchKeyword, GDExpression, GDColon, GDMatchCasesList>(this);
        }

        internal override void HandleChar(char c, GDReadingState state)
        {
            if (IsSpace(c))
            {
                _form.AddBeforeActiveToken(state.Push(new GDSpace()));
                state.PassChar(c);
                return;
            }

            switch (_form.State)
            {
                case State.Match:
                    state.PushAndPass(new GDKeywordResolver<GDMatchKeyword>(this), c);
                    break;
                case State.Value:
                    state.PushAndPass(new GDExpressionResolver(this), c);
                    break;
                case State.Colon:
                    state.PushAndPass(new GDSingleCharTokenResolver<GDColon>(this), c);
                    break;
                case State.Cases:
                    _form.State = State.Completed;
                    state.PushAndPass(Cases, c);
                    break;
                default:
                    state.PopAndPass(c);
                    break;
            }
        }

        internal override void HandleNewLineChar(GDReadingState state)
        {
            switch (_form.State)
            {
                case State.Value:
                case State.Colon:
                case State.Cases:
                    _form.State = State.Completed;
                    state.PushAndPassNewLine(Cases);
                    break;
                default:
                    state.PopAndPassNewLine(); 
                    break;
            }
        }

        void IKeywordReceiver<GDMatchKeyword>.HandleReceivedToken(GDMatchKeyword token)
        {
            if (_form.State == State.Match)
            {
                MatchKeyword = token;
                _form.State = State.Value;
                return;
            }

            throw new GDInvalidReadingStateException();
        }

        void IKeywordReceiver<GDMatchKeyword>.HandleReceivedKeywordSkip()
        {
            if (_form.State == State.Match)
            {
                _form.State = State.Value;
                return;
            }

            throw new GDInvalidReadingStateException();
        }

        void ITokenReceiver<GDColon>.HandleReceivedToken(GDColon token)
        {
            if (_form.State == State.Colon)
            {
                Colon = token;
                _form.State = State.Cases;
                return;
            }

            throw new GDInvalidReadingStateException();
        }

        void ITokenReceiver<GDColon>.HandleReceivedTokenSkip()
        {
            if (_form.State == State.Colon)
            {
                _form.State = State.Cases;
                return;
            }

            throw new GDInvalidReadingStateException();
        }

        void IExpressionsReceiver.HandleReceivedToken(GDExpression token)
        {
            if (_form.State == State.Value)
            {
                Value = token;
                _form.State = State.Colon;
                return;
            }

            throw new GDInvalidReadingStateException();
        }

        void IExpressionsReceiver.HandleReceivedExpressionSkip()
        {
            if (_form.State == State.Value)
            {
                _form.State = State.Colon;
                return;
            }

            throw new GDInvalidReadingStateException();
        }
    }
}