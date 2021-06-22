﻿namespace GDShrapt.Reader
{
    internal class GDMatchCasesResolver : GDIntendedResolver
    {
        GDSpace _lastSpace;

        new IMatchCaseReceiver Owner { get; }

        public GDMatchCasesResolver(IMatchCaseReceiver owner, int lineIntendation)
            : base(owner, lineIntendation)
        {
            Owner = owner;
        }

        internal override void HandleCharAfterIntendation(char c, GDReadingState state)
        {
            if (IsSpace(c))
            {
                state.Push(_lastSpace = new GDSpace());
                state.PassChar(c);
                return;
            }

            SendIntendationTokensToOwner();

            if (_lastSpace != null)
            {
                Owner.HandleReceivedToken(_lastSpace);
                _lastSpace = null;
            }

            Owner.HandleReceivedToken(state.Push(new GDMatchCaseDeclaration(LineIntendationThreshold)));
            state.PassChar(c);
        }

        internal override void HandleNewLineAfterIntendation(GDReadingState state)
        {
            ResetIntendation();
            state.PassNewLine();
        }

        internal override void HandleSharpCharAfterIntendation(GDReadingState state)
        {
            Owner.HandleReceivedToken(state.Push(new GDComment()));
            state.PassSharpChar();
        }

        internal override void ForceComplete(GDReadingState state)
        {
            if (_lastSpace != null)
            {
                Owner.HandleReceivedToken(_lastSpace);
                _lastSpace = null;
            }

            base.ForceComplete(state);
            PassIntendationSequence(state);
        }
    }
}