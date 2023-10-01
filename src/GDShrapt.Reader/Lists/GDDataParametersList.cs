﻿namespace GDShrapt.Reader
{
    public sealed class GDDataParametersList : GDCommaSeparatedList<GDDataToken>,
        ITokenReceiver<GDDataToken>
    {
        internal override bool IsStopChar(char c)
        {
            return !c.IsDataStartCharToken();
        }

        internal override GDReader ResolveNode()
        {
            return new GDDataTokensResolver(this);
        }

        internal override void HandleNewLineChar(GDReadingState state)
        {
            ListForm.AddToEnd(new GDNewLine());
        }

        public override GDNode CreateEmptyInstance()
        {
            return new GDDataParametersList();
        }

        internal override void Visit(IGDVisitor visitor)
        {
            visitor.Visit(this);
        }

        internal override void Left(IGDVisitor visitor)
        {
            visitor.Left(this);
        }

        void ITokenReceiver<GDDataToken>.HandleReceivedToken(GDDataToken token)
        {
            ListForm.AddToEnd(token);
        }
    }
}
