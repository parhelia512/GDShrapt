﻿namespace GDShrapt.Reader
{
    internal sealed class GDElseResolver : GDIntendedSequenceResolver
    {
        new IElseBranchReceiver Owner { get; }

        public override string Sequence => "else";

        public GDElseResolver(IElseBranchReceiver owner, int lineIntendation)
            : base(owner, lineIntendation)
        {
            Owner = owner;
        }

        protected override void OnFail(GDReadingState state)
        {
            Owner.HandleReceivedElseBranchSkip();
        }

        protected override void OnMatch(GDReadingState state)
        {
            var branch = new GDElseBranch();

            branch.SendKeyword(new GDElseKeyword());

            SendIntendationToOwner();
            Owner.HandleReceivedToken(branch);
            state.Push(branch);
        }
    }
}
