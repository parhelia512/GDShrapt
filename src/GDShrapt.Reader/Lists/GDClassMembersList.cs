﻿namespace GDShrapt.Reader
{
    public sealed class GDClassMembersList : GDIntendedTokensList<GDClassMember>,
        IIntendedTokenReceiver<GDClassMember>
    {
        bool _completed;

        internal GDClassMembersList(int lineIntendation) 
            : base(lineIntendation)
        {
        }

        public GDClassMembersList()
        {
        }

        internal override void HandleChar(char c, GDReadingState state)
        {
            if (!_completed)
            {
                _completed = true;
                state.PushAndPass(new GDClassMembersResolver(this, LineIntendationThreshold), c);
                return;
            }

            state.PopAndPass(c);
        }

        internal override void HandleNewLineChar(GDReadingState state)
        {
            if (!_completed)
            {
                _completed = true;
                state.Push(new GDClassMembersResolver(this, LineIntendationThreshold));
                state.PassNewLine();
                return;
            }

            state.PopAndPassNewLine();
        }

        public override GDNode CreateEmptyInstance()
        {
            return new GDClassMembersList();
        }

        void ITokenReceiver<GDClassMember>.HandleReceivedToken(GDClassMember token)
        {
            ListForm.Add(token);
        }
    }
}
