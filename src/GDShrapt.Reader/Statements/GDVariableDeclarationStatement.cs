﻿namespace GDShrapt.Reader
{
    public sealed class GDVariableDeclarationStatement : GDStatement, 
        ITokenOrSkipReceiver<GDVarKeyword>,
        ITokenOrSkipReceiver<GDIdentifier>,
        ITokenOrSkipReceiver<GDColon>,
        ITokenOrSkipReceiver<GDType>,
        ITokenOrSkipReceiver<GDAssign>,
        ITokenOrSkipReceiver<GDExpression>
    {
        public GDVarKeyword VarKeyword
        {
            get => _form.Token0;
            set => _form.Token0 = value;
        }
        public GDIdentifier Identifier
        {
            get => _form.Token1;
            set => _form.Token1 = value;
        }
        public GDColon Colon
        {
            get => _form.Token2;
            set => _form.Token2 = value;
        }
        public GDType Type
        {
            get => _form.Token3;
            set => _form.Token3 = value;
        }
        public GDAssign Assign
        {
            get => _form.Token4;
            set => _form.Token4 = value;
        }
        public GDExpression Initializer
        {
            get => _form.Token5;
            set => _form.Token5 = value;
        }

        enum State
        {
            Var,
            Identifier,
            Colon,
            Type,
            Assign,
            Initializer,
            Completed
        }

        readonly GDTokensForm<State, GDVarKeyword, GDIdentifier, GDColon, GDType, GDAssign, GDExpression> _form;
        public override GDTokensForm Form => _form;
        internal GDVariableDeclarationStatement(int lineIntendation)
            : base(lineIntendation)
        {
            _form = new GDTokensForm<State, GDVarKeyword, GDIdentifier, GDColon, GDType, GDAssign, GDExpression>(this);
        }

        public GDVariableDeclarationStatement()
        {
            _form = new GDTokensForm<State, GDVarKeyword, GDIdentifier, GDColon, GDType, GDAssign, GDExpression>(this);
        }

        internal override void HandleChar(char c, GDReadingState state)
        {
            if (this.ResolveSpaceToken(c, state))
                return;

            switch (_form.State)
            {
                case State.Var:
                    this.ResolveKeyword<GDVarKeyword>(c, state);
                    break;
                case State.Identifier:
                    this.ResolveIdentifier(c, state);
                    break;
                case State.Colon:
                    this.ResolveColon(c, state);
                    break;
                case State.Type:
                    this.ResolveType(c, state);
                    break;
                case State.Assign:
                    this.ResolveAssign(c, state);
                    break;
                case State.Initializer:
                    this.ResolveExpression(c, state);
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

        public override GDNode CreateEmptyInstance()
        {
            return new GDVariableDeclarationStatement();
        }

        void ITokenReceiver<GDVarKeyword>.HandleReceivedToken(GDVarKeyword token)
        {
            if (_form.State == State.Var)
            {
                _form.State = State.Identifier;
                VarKeyword = token;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenSkipReceiver<GDVarKeyword>.HandleReceivedTokenSkip()
        {
            if (_form.State == State.Var)
            {
                _form.State = State.Identifier;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenReceiver<GDIdentifier>.HandleReceivedToken(GDIdentifier token)
        {
            if (_form.State == State.Identifier)
            {
                _form.State = State.Colon;
                Identifier = token;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenSkipReceiver<GDIdentifier>.HandleReceivedTokenSkip()
        {
            if (_form.State == State.Identifier)
            {
                _form.State = State.Colon;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenReceiver<GDColon>.HandleReceivedToken(GDColon token)
        {
            if (_form.State == State.Colon)
            {
                _form.State = State.Type;
                Colon = token;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenSkipReceiver<GDColon>.HandleReceivedTokenSkip()
        {
            if (_form.State == State.Colon)
            {
                _form.State = State.Type;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenReceiver<GDType>.HandleReceivedToken(GDType token)
        {
            if (_form.State == State.Type)
            {
                _form.State = State.Assign;
                Type = token;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenSkipReceiver<GDType>.HandleReceivedTokenSkip()
        {
            if (_form.State == State.Type)
            {
                _form.State = State.Assign;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenReceiver<GDAssign>.HandleReceivedToken(GDAssign token)
        {
            if (_form.State == State.Assign)
            {
                _form.State = State.Initializer;
                Assign = token;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenSkipReceiver<GDAssign>.HandleReceivedTokenSkip()
        {
            if (_form.State == State.Assign)
            {
                _form.State = State.Initializer;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenReceiver<GDExpression>.HandleReceivedToken(GDExpression token)
        {
            if (_form.State == State.Initializer)
            {
                _form.State = State.Completed;
                Initializer = token;
                return;
            }

            throw new GDInvalidStateException();
        }

        void ITokenSkipReceiver<GDExpression>.HandleReceivedTokenSkip()
        {
            if (_form.State == State.Initializer)
            {
                _form.State = State.Completed;
                return;
            }

            throw new GDInvalidStateException();
        }
    }
}